using System.Collections.Generic;
using UnityEngine;
 
    public class DropParentSprite : MonoBehaviour, IDropParentSprite
    {
        protected List<IDragItemSprite> dragItems = new List<IDragItemSprite>();
        public List<IDragItemSprite> DragItems => dragItems;
        Transform IDropParentSprite.MyTransform => transform;

        /// <summary>
        /// Чтобы метод работал - должен быть включен рейкаст на имадже
        /// </summary>
        /// <param name="data"></param>
        public void OnDrop(Transform data)
        {
            if (data  != null && dragItems.Count == 0)
            {
                var dragItem = data .GetComponent<IDragItemSprite>();
                AddDragItem(dragItem);
            }
        }
        public virtual void AddDragItem(IDragItemSprite dragItem)
        {
          if (dragItem.Parent != null) dragItem.Parent.RemoveFromListItem(dragItem);
            dragItem.SetDropParent(this);
    var dragItemSprite =    dragItem as DragItemSprite;
        dragItemSprite.SetupParentPosition();
            if (dragItems.Contains(dragItem) == false)
                dragItems.Add(dragItem);
        }
         
        public void RemoveFromListItem(IDragItemSprite dragItem)
        {
            if (dragItems.Count > 0 && dragItems.Contains(dragItem))
                dragItems.Remove(dragItem); 
        }
    } 