using System.Collections.Generic;
using UnityEngine;

 
    public interface IDropParentSprite : IDropHandlerSprite
    {
        public Transform MyTransform { get; }

        /// <summary>
        /// список айтемов которые есть на объекте
        /// </summary>
        public List<IDragItemSprite> DragItems { get; }

        /// <summary>
        /// Добавляет айтем в список DragItems 
        /// </summary>
        /// <param name="dragItem"></param>
        void AddDragItem(IDragItemSprite dragItem);

        /// <summary>
        /// Удаляет айтем из списка DragItems, если он там был
        /// </summary>
        /// <param name="dragItem"></param>
        public void RemoveFromListItem(IDragItemSprite dragItem);
    }
    public interface IDropHandlerSprite
    {
        public void OnDrop(Transform data);
    } 