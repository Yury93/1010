using DG.Tweening;
using System; 
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Lines
{
  public  List<int> Line;
}
 
public class GroupItem : DragItemSprite
{
    [SerializeField] private List<CellItem> cellItem;
    [field: SerializeField] public List<Lines> ShapeLines {  get; set; }
    [field: SerializeField] public List<Lines> ShapeLines90 { get; set; }  
    [field: SerializeField] public bool isFliped { get; set; }
    public Action<GroupItem,List<CellItem>> onDrop;
    public List<CellItem> CellItems => cellItem;
    public float Rotation { get; set; }
    List<Tween> tweens = new List<Tween>();
    public void Init()
    {
        cellItem.ForEach(c=>c.SetGroup(this));
        transform.localScale = new Vector3(  transform.localScale.x / 1.1f, transform.localScale.y / 1.1f, 1f) ;
    }
    public override void SetupParentPosition()
    {
        base.SetupParentPosition();
        if(Parent.MyTransform.GetComponent<StartDropParent>())
        {
            SetSmallScale();
        }
    }
    private void Update()
    {
        cellItem.ForEach(c => c.Updated());
        OnInputMouseButtonUp();
    }
    private void LateUpdate()
    {
        cellItem.ForEach(c => c.lateUpdated());
    }

    private void OnInputMouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
         //   tweens.ForEach(t=>t?.Kill());
          //  SetNormalScale();
            bool allColliders = IsAllColliders();
            if ((allColliders == false))
            {
                return;
            }
            else
            {
                bool allowDrop = IsAllowDrop();
                if (allowDrop) DropItems( );
            }
        }
    }
    public override void OnBeginDrag<T>(EventData2D<T> eventData)
    {
        base.OnBeginDrag(eventData);
        SetNormalScale();

    }
    public void SetSmallScale()
    {
        
          tweens.Add(  transform.DOScale(new Vector3(transform.localScale.x/1.1f, transform.localScale.y / 1.1f, 1f),0.001f));
       
    }
    public void SetNormalScale()
    {

        tweens.Add(transform.DOScale(new Vector3(3f, 3f, 3f), 0.001f));
        
    }
    private void DropItems()
    {
        for (int i = 0; i < cellItem.Count; i++)
        {
            cellItem[i].DropParentSprite.OnDrop(cellItem[i].transform);
         //   cellItem[i].transform.localScale = Vector3.one;
        }
        onDrop?.Invoke(this,cellItem);
    }

    private bool IsAllowDrop()
    {
        bool allowDrop = true;
        for (int i = 0; i < cellItem.Count; i++)
        {
            if (cellItem[i].DropParentSprite.DragItems.Count > 0)
            {
                allowDrop = false;
                break;
            }
            //if (cellItem[i].SpriteRender.color != Color.white)
            //cellItem[i].SpriteRender.color = Color.white;
            //else
            //    cellItem[i].SpriteRender.color = Color.white;
        }

        return allowDrop;
    }

    private bool IsAllColliders()
    {
        bool allColliders = true;
        for (int i = 0; i < cellItem.Count; i++)
        {

            if (cellItem[i].CellCollider == null)
            {
                allColliders = false;
                break;
            }
            else
            {
                int countEquelCollider = 0;
                for (int k = 0; k < cellItem.Count; k++)
                {
                    if (cellItem[i].CellCollider == cellItem[k].CellCollider)
                    {
                        countEquelCollider++;
                    }
                }
                if(countEquelCollider > 1)
                {
                    //Debug.LogError("Ѕолее одного одинакового коллайдера");
                    allColliders = false;
                    break;
                }
            }
        }

        return allColliders;
    }
}
