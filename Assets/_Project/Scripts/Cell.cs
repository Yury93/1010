 
using UnityEngine;


public class Cell : DropParentSprite
{
    [SerializeField] private SpriteRenderer sprite;
    [field: SerializeField] public int number { get; set; }
    [field: SerializeField] public int line { get; set; }
    public override void AddDragItem(IDragItemSprite dragItem)
    {
        base.AddDragItem(dragItem);
    } 
}
 
