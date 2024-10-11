using UnityEngine;

public class DragItemSprite : MonoBehaviour, IDragItemSprite
{

    [SerializeField] private IDropParentSprite parent;
    private bool endDrag = true;
    private Transform freeContent;
    public Transform MyTransform => transform;
    public Transform FreeContent => freeContent;
    public IDropParentSprite Parent => parent;

    public void SetDropParent(IDropParentSprite dropParent) => parent = dropParent;
    public void SetFreeContent(Transform freeContent) => this.freeContent = freeContent;

    private void SetupParentPosition()
    {
        if (parent != null)
        {
            MyTransform.SetParent(parent.MyTransform);
            transform.localPosition = Vector3.zero;
        }
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
        if (MyTransform.parent)
        {
            parent = MyTransform.parent.GetComponent<IDropParentSprite>();
        }
            endDrag = false;
       
    }

    public virtual void OnEndDrag<T>(EventData2D<T> eventData)
    {
        endDrag = true;
        SetupParentPosition();
    }
}