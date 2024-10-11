using UnityEngine;

public class CellItem : DragItemSprite
{ 
    public override void OnBeginDrag<T>(EventData2D<T> eventData)
    {
        base.OnBeginDrag<T>(eventData);
    }
    public override void OnDrag<T>(EventData2D<T> eventData)
    {
        base.OnDrag(eventData);
    }
    public override void OnEndDrag<T>(EventData2D<T> eventData)
    {
        base.OnEndDrag(eventData);
    }
    public override void Updated()
    {
        base.Updated();
    }
}
