
using System;
using UnityEngine;


public class Cell : DropParentSprite
{
    [field: SerializeField] public SpriteRenderer sprite { get; set; }
    [field: SerializeField] public int column { get; set; }
    [field: SerializeField] public int line { get; set; }

    public Action<Cell> onAddDragItem;
    public override void AddDragItem(IDragItemSprite dragItem)
    { 
        base.AddDragItem(dragItem);
    
        var cellItem = dragItem.MyTransform.GetComponent<CellItem>(); 
            cellItem.Ban();
        onAddDragItem?.Invoke(this);
    } 
}
 
