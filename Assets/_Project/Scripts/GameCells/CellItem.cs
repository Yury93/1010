using System;
using System.Collections.Generic;
using UnityEngine;

public class CellItem : DragItemSprite
{
    [field: SerializeField] public SpriteRenderer SpriteRender { get; protected set; }
    [field: SerializeField] public bool isBan { get; protected set; }
    [field: SerializeField] public Collider2D CellCollider { get; protected set; } 
    [field: SerializeField] public LayerMask layerMask { get; protected set; }
    [field : SerializeField] public GroupItem groupItem { get; protected set; }
    [field: SerializeField] public List<Color> colors;
    public DropParentSprite DropParentSprite => CellCollider != null ? CellCollider.gameObject.GetComponent<DropParentSprite>() : null;
    private void Start()
    {
        var rnd = UnityEngine.Random.Range(0,colors.Count);
       SpriteRender.color = colors[rnd];
    }
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
       // CellCollider = null;    
        RaycastHit2D hit = new RaycastHit2D();
       
            Vector2 direction = Vector2.right;
            Vector2 origin = SpriteRender.bounds.center;
            float distance = 0f;
            hit = Physics2D.Raycast(origin, direction, distance, layerMask.value);


        if (hit.collider != null)
        {
            CellCollider = hit.collider;
       //  CellCollider.GetComponent<SpriteRenderer>().color = Color.white;
        } 
    }
    private float timerClear = 1;
    private float dynamicTimer;
    private void UpdateTimer()=>dynamicTimer = Time.time + 0.01f;
    public void lateUpdated()
    {
        if (Time.time > dynamicTimer)
        {
            CellCollider = null;
            UpdateTimer();
            Updated();
        }
    }
}
