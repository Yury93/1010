using System.Collections.Generic;
using UnityEngine;

 
    public interface IDropParentSprite : IDropHandlerSprite
    {
        public Transform MyTransform { get; }

        /// <summary>
        /// ������ ������� ������� ���� �� �������
        /// </summary>
        public List<IDragItemSprite> DragItems { get; }

        /// <summary>
        /// ��������� ����� � ������ DragItems 
        /// </summary>
        /// <param name="dragItem"></param>
        void AddDragItem(IDragItemSprite dragItem);

        /// <summary>
        /// ������� ����� �� ������ DragItems, ���� �� ��� ���
        /// </summary>
        /// <param name="dragItem"></param>
        public void RemoveFromListItem(IDragItemSprite dragItem);
    }
    public interface IDropHandlerSprite
    {
        public void OnDrop(Transform data);
    } 