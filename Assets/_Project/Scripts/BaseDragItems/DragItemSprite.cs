 
using UnityEngine; 

[RequireComponent(typeof(BoxCollider2D))]
public class DragItemSprite : MonoBehaviour, IDragItemSprite
{

    [SerializeField] private IDropParentSprite parent; 
    float largestOverlapArea = 0f;
    private bool endDrag = true;
    private Transform freeContent;
    public Transform MyTransform => transform;
    public Transform FreeContent => freeContent;
    public IDropParentSprite Parent => parent;
   
    public void SetDropParent(IDropParentSprite dropParent) => parent = dropParent;
    public void SetFreeContent(Transform freeContent) => this.freeContent = freeContent;

     
    public void SetupParentPosition()
    { 
        MyTransform.SetParent(parent.MyTransform);
        transform.localPosition = Vector3.zero;
    }

    public virtual void Updated()
    {

    }

    public virtual void OnDrag<T>(EventData2D<T> eventData)
    {
        
        MyTransform.position = eventData.eventPosition;
        MyTransform.SetParent(FreeContent);
    }

    public virtual void OnBeginDrag<T>(EventData2D<T> eventData)
    {
            parent = MyTransform.parent.GetComponent<IDropParentSprite>(); 
            endDrag = false;
    }

    public virtual void OnEndDrag<T>(EventData2D<T> eventData)
    {
        endDrag = true;
        SetupParentPosition();
    }
   
}