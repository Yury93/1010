using UnityEngine;

public class CellItem : DragItemSprite
{
    [field: SerializeField] public SpriteRenderer SpriteRender { get; protected set; }
    [field: SerializeField] public bool isBan { get; protected set; }
    [field: SerializeField] public Collider2D CellCollider { get; protected set; } 
    [field: SerializeField] public LayerMask layerMask { get; protected set; }
    [field : SerializeField] public GroupItem groupItem { get; protected set; }
    public DropParentSprite DropParentSprite => CellCollider != null ? CellCollider.gameObject.GetComponent<DropParentSprite>() : null;
    public void SetGroup(GroupItem groupItem)
    {
        this.groupItem = groupItem;
    }
     
    public override void OnBeginDrag<T>(EventData2D<T> eventData)
    {
        if (groupItem) return;
        if(isBan) { return; }
        base.OnBeginDrag<T>(eventData);
    }
    public override void OnDrag<T>(EventData2D<T> eventData)
    {
        if (groupItem) return;
        if (isBan) { return; }
        base.OnDrag(eventData);
    }
    public override void OnEndDrag<T>(EventData2D<T> eventData)
    {
        if (groupItem) return;
        if (isBan) { return; }
        base.OnEndDrag(eventData);
    }
    public void Ban()
    {
        isBan = true;
    }
    public override void Updated()
    {
        if (isBan) { return; }
        Vector2 direction = Vector2.right;
        Vector2 origin = SpriteRender.bounds.center;
        float distance = 0f;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, layerMask.value);
        if (hit.collider != null)
        {
            CellCollider = hit.collider;
        }
    } 
}
