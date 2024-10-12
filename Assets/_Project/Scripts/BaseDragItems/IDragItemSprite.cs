using UnityEngine; 

 
    public interface IDragItemSprite  : IDragSpriteHandler, IBeginDragSpriteHandler, IEndDragSpriteHandler, IUpdater
    {
        public Transform MyTransform { get; }
        public Transform FreeContent { get; }
        public IDropParentSprite Parent { get; } 
        /// <summary>
        /// Задаётся контент по которому будут перемещаться Айтемы
        /// </summary>
        /// <param name="freeContent"></param>
        public void SetFreeContent(Transform freeContent);
        /// <summary>
        /// Задать объект в который дропнут Айтем
        /// </summary>
        /// <param name="dropParent"></param>
        void SetDropParent(IDropParentSprite dropParent); 
    }

    public interface IEndDragSpriteHandler
    {
        public void OnEndDrag<T>(EventData2D<T> transform);
    }

    public interface IDragSpriteHandler
    {
        public void OnDrag<T>(EventData2D<T> transform);
    }

    public interface IBeginDragSpriteHandler
    {
        public void OnBeginDrag<T>(EventData2D<T> transform);
 
    }

    public interface IUpdater
    { 
        public abstract void Updated();
    } 