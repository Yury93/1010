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
            MyTransform.SetParent(parent.MyTransform);
            transform.localPosition = Vector3.zero;
        }

        public virtual void Updated()
        {
           
        }

        public virtual void OnDrag(Transform transform)
        {
            MyTransform.position = transform.position;
            MyTransform.SetParent(FreeContent);
        Debug.LogError("drag");
        }

        public virtual void OnBeginDrag(Transform transform)
        {
            parent = MyTransform.parent.GetComponent<IDropParentSprite>();
            endDrag = false;
        Debug.LogError("onbegin drag");
    }

        public virtual void OnEndDrag(Transform transform)
        {
            endDrag = true;
            SetupParentPosition();
        Debug.LogError("on end drag");
    }
    } 