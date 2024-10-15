using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform freeContent;
    public static GameController instance;
    [field:SerializeField] public GridBlocks GridBlocks {  get; private set; }
    [Space(20)] 
    [SerializeField] private List<GroupItem> variantItems;
    [SerializeField] private List<float> variantRotations;
    [SerializeField] private List<StartDropParent> startDropParents;
    [SerializeField] private List<StartDropParent> bounds;
    [SerializeField] private UIController uiController;
    private List<GroupItem> groupItems = new List<GroupItem>();
    int dropGoupCount;
    public bool IsStartGame { get;set;}
    public Action onStart;
    public Action onEnd;
    private void Awake()
    {
        instance = this;
        GridBlocks.Init();
        uiController.Init();
 
    }
    private IEnumerator Start()
    {
        while (IsStartGame == false) 
        { 
          yield return null;
        }
  
        onStart?.Invoke();
        CreateVariantBlocks();
        CreateBounds();
        GridBlocks.onAddDragItem += OnAddDragItem;
    }
      
    private void OnAddDragItem(Cell cell)
    {
        var lineCells = GridBlocks.cells.Where(c => c.line == cell.line).ToList();
        var numberCells = GridBlocks.cells.Where(c => c.column == cell.column).ToList();
         
        bool isLineFilled = lineCells.All(c => c.DragItems.Count > 0);
        bool isNumberFilled = numberCells.All(c => c.DragItems.Count > 0); 
        if (isLineFilled )
        {
            foreach (var item in lineCells)
            {
                var dragItem = item.DragItems[0];
                item.RemoveFromListItem(dragItem);
                DestroyItem(dragItem);

            }
        }
        if( isNumberFilled)
        {
            foreach (var item in numberCells)
            {
                if (item.DragItems.Count > 0)
                {
                    var dragItem = item.DragItems[0];
                    item.RemoveFromListItem(dragItem);
                    DestroyItem(dragItem);
                }
            }
        }
    }

    private static void DestroyItem(IDragItemSprite dragItem)
    {
        var cellItem = dragItem.MyTransform.GetComponent<CellItem>();
        cellItem.SpriteRender.transform.DOScale(new Vector3(0, 0, 0), 0.7f);
        cellItem.SpriteRender.transform.DOLocalRotate(new Vector3(0, 0, -180), 1.2f).OnComplete(() =>
        {
            Destroy(dragItem.MyTransform.gameObject);
        });
    }

    private void CreateVariantBlocks()
    {  
        foreach (var parent in startDropParents)
        {
            var rnd =UnityEngine. Random.Range(0, variantItems.Count);
            var dragItem = Instantiate(variantItems[rnd], null);
            var rndRotation = UnityEngine.Random.Range(0, variantRotations.Count);
             dragItem.transform.rotation = Quaternion.Euler(new Vector3(0, 0, variantRotations[rndRotation]));
            dragItem.Rotation = variantRotations[rndRotation];
            dragItem.Init();
            dragItem.SetDropParent(parent);
            dragItem.SetupParentPosition();
            dragItem.SetFreeContent(freeContent);
            dragItem.onDrop += OnDropDragItem; 
            groupItems.Add(dragItem);
          
        }
      
    }

    private bool CheckCanPlace(bool canPlace, GroupItem dragItem, int rotation)
    {
        foreach (var cell in GridBlocks.cells)
        {
            if (canPlace) { break; }
            canPlace = CanPlaceShape(dragItem, cell.line, cell.column, rotation);
        }

        return canPlace;
    }

    public bool CanPlaceShape(GroupItem groupItem, int x, int y,int rotation)
    {
        List<Lines> shape = new List<Lines>();

        if(rotation == 0)
          shape = groupItem.ShapeLines; 
        else if( rotation == 90)
            shape = groupItem.ShapeLines90;
        
        int line = 0;
        foreach (var lineItem in shape)
        {
            line = shape.IndexOf(lineItem);
            foreach (var coord in lineItem.Line)
            { 
                int blockX = x + coord; 
                int blockY = y + line;
                 
                Cell cell = GetCell(blockX, blockY);
                if (cell == null)
                { 
                    return false;
                }

                if (  cell.DragItems.Count > 0)
                { 
                    return false;  
                }
            }
        }
         
        return true;
    }

    public Cell GetCell(int x, int y)
    { 
        foreach (var cell in GridBlocks.cells)
        {
            if (cell.column == x && cell.line == y)
            {
                return cell;
            }
        }
        return null;  
    }
    private (int, int) RotatePoint(int x, int y, int rotation)
    {
        switch (rotation)
        {
            case 90:
                return (-y, x);
            case 180:
                return (-x, -y);
            case 270:
                return (y, -x);
            default:
                return (x, y);
        }
    }

    private void OnDropDragItem(GroupItem item, List<CellItem> list)
    {
         dropGoupCount++;
        groupItems.Remove(item);

        Destroy(item.gameObject);
        if(dropGoupCount == startDropParents.Count)
        {
            dropGoupCount = 0;
            CreateVariantBlocks();
        }
        bool canPlace = false;
         foreach (var group in groupItems)
        {
          canPlace =  CheckCanPlace(canPlace,group,(int)group.Rotation);
            if(canPlace == true)
            {
                break;
            }
        }
        if (!canPlace)
        { 
            Debug.LogError("Нет места");
            onEnd?.Invoke();
        }

    }

   

    private void CreateBounds()
    {
        foreach (var bound in bounds)
        {
            var dragItem = Instantiate(variantItems[0], null);
            dragItem.Init();
            dragItem.SetDropParent(bound);
            dragItem.SetupParentPosition();
            dragItem.SetFreeContent(freeContent);
        }
    }
}
