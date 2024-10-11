using UnityEngine;

public class EventSystem : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [field: SerializeField] public EventData2D<CellItem> eventData2D;
    Collider2D hitCollider;
    void Update()
    {
        var mouse = Input.mousePosition;
        mouse.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mouse);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        hitCollider = hit.collider; 
   
        if (Input.GetMouseButtonDown(0) && eventData2D == null)
        {
            if (hitCollider != null)
            {
                var cellItem = hitCollider.GetComponent<CellItem>();
                var s = hitCollider.GetComponent<SpriteRenderer>();
             
                if (cellItem)
                {


                    if (eventData2D == null) eventData2D = new EventData2D<CellItem>();
                    eventData2D.Collider2d = hitCollider;

                    eventData2D.eventItem = cellItem;
                    cellItem.OnBeginDrag(eventData2D); 
                }
            }
        }
        if (eventData2D != null)
        {
            eventData2D.eventPosition = GetMouseWorldPosition(Camera.main, eventData2D.eventItem.transform);
        }
        if (Input.GetMouseButton(0))
        {
            if (eventData2D != null)
            {
                var cell = eventData2D.eventItem;  
                cell.OnDrag(eventData2D); 
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (eventData2D != null)
            {
                var cell = eventData2D.eventItem;  
                cell.OnEndDrag(eventData2D);
                eventData2D = null; 
            }
        }
    }
    public static Vector3 GetMouseWorldPosition(Camera mainCamera, Transform target)
    {
        float plainPositionZ = mainCamera.WorldToScreenPoint(target.position).z;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, plainPositionZ));
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