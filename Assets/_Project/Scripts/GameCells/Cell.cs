
using System;
using UnityEngine;


public class Cell : DropParentSprite
{
    [field: SerializeField] public SpriteRenderer sprite { get; set; }
    [field: SerializeField] public int number { get; set; }
    [field: SerializeField] public int line { get; set; }
    bool IsHasItem;
    public override void AddDragItem(IDragItemSprite dragItem)
    { 
        base.AddDragItem(dragItem);
        IsHasItem = true;
        var cellItem = dragItem.MyTransform.GetComponent<CellItem>(); 
            cellItem.Ban(); 
    } 
}
 
