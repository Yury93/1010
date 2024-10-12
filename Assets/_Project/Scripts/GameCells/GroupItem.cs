using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class GroupItem : DragItemSprite
{
    [SerializeField] private List<CellItem> cellItem;

    public void Init()
    {
        cellItem.ForEach(c=>c.SetGroup(this));
    }
    private void Update()
    {

        cellItem.ForEach(c => c.Updated());
        if (Input.GetMouseButtonUp(0)) 
        {
            bool allColliders = true;
            for (int i = 0; i < cellItem.Count; i++)
            {
                if(cellItem[i].CellCollider == null)
                {
                    allColliders = false;
                    break;
                }
            }
            if ((allColliders == false))
            {
                return;
            }
            else
            {
                bool allowDrop = true;
                for (int i = 0; i < cellItem.Count; i++)
                {
                    if (cellItem[i].DropParentSprite.DragItems.Count > 0)
                    {
                        allowDrop = false;
                        break;
                    }
                }
                if (allowDrop) 
                {
                    for (int i = 0; i < cellItem.Count; i++)
                    {
                        cellItem[i].DropParentSprite.OnDrop(cellItem[i].transform);
                    }
                }
            }
        }
    }
}
