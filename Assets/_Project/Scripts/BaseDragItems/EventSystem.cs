using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [field: SerializeField] public EventData2D<DragItemSprite> eventDataItem; 
    RaycastHit2D raycast;
    Collider2D colliderItem;
    private void Start()
    {
         //eventDataDrop = new EventData2D<DropParentSprite>();
    }
    void Update()
    {

        //GameController.instance.GridBlocks.cells.ForEach(c=>c.sprite.color = Color.gray);
        //GameController.instance.GridBlocks.FreeCells.ForEach(c => c.sprite.color = Color.green);
        //GameController.instance.GridBlocks.BanCells.ForEach(c => c.sprite.color = Color.red);
        ApplyRaycasts();
        OnMouseButtonDown();
        SetEventPosition();
        OnMouseButton();
        OnMouseButtonUp();
    }

    private void SetEventPosition()
    {
        if (eventDataItem != null)   eventDataItem.eventPosition = GetMouseWorldPosition(eventDataItem.eventItem.transform); 
    }

    private void OnMouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //if (raycasts != null)
            //{
            //    RaycastHit2D raycastHit2D = raycasts.FirstOrDefault(r => r.collider != null && r.collider.GetComponent<DropParentSprite>());
            //    if (raycastHit2D.collider != null)
            //    {
            //        eventDataDrop.eventItem = raycastHit2D.collider.GetComponent<DropParentSprite>();
            //        if (eventDataDrop != null)
            //        {
            //            if (eventDataDrop.eventItem)
            //            {
            //                if (eventDataItem != null && eventDataDrop.eventItem.DragItems.Count == 0)
            //                {
            //                   var cellItem = eventDataItem.eventItem as CellItem;

            //                    if (eventDataDrop.eventItem.DragItems.Count == 0 && cellItem.isBan == false)
            //                    {
            //                        eventDataDrop.eventItem.OnDrop(eventDataItem.eventItem.transform);
                                
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            if (eventDataItem != null)
            {
                var cell = eventDataItem.eventItem;
                cell.OnEndDrag(eventDataItem);
                eventDataItem = null;
            } 
        }
    }

    private void OnMouseButton()
    {
        if (Input.GetMouseButton(0))
        {
            if (eventDataItem != null)
            {
                var cell = eventDataItem.eventItem;
                cell.OnDrag(eventDataItem);
            }
            
        }
    }

    private void OnMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0) && eventDataItem == null)
        {
            if (raycast.collider != null)
            { 
                var raycastHit2D = raycast.collider.GetComponent<DragItemSprite>(); 
                if (raycastHit2D )
                {
                    if (eventDataItem == null) eventDataItem = new EventData2D<DragItemSprite>();
                     
                    eventDataItem.Collider2d = raycastHit2D.GetComponent<Collider2D>(); 
                    eventDataItem.eventItem = raycastHit2D;
                    eventDataItem.eventItem.OnBeginDrag(eventDataItem); 
                }
            }
        }
    }

    private void ApplyRaycasts()
    {
        var mouse = Input.mousePosition;
        mouse.z = _camera.nearClipPlane;
        Ray ray = _camera.ScreenPointToRay(mouse); 
        RaycastHit2D  hit  = Physics2D.Raycast (ray.origin, ray.direction); 
        raycast  = hit;
    }

    public Vector3 GetMouseWorldPosition( Transform target)
    {
        float plainPositionZ = _camera.WorldToScreenPoint(target.position).z;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, plainPositionZ));
        return worldPosition;
    }
}
[SerializeField]
public class EventData2D <T>
{
    public Collider2D Collider2d;
    public Vector3 eventPosition;
    public T eventItem {  get;  set; }
     
}