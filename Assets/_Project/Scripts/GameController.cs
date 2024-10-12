using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform freeContent;
    public static GameController instance;
    [field:SerializeField] public GridBlocks GridBlocks {  get; private set; }

    [Space(20)] 
    [SerializeField] private List<GroupItem> variantItems;
    [SerializeField] private List<StartDropParent> startDropParents;
 

    private void Awake()
    {
        instance = this;
        GridBlocks.Init();
       
        //groupItem.Init();
        //groupItem.SetDropParent(generalDropParent);
        //groupItem.SetupParentPosition();
        //groupItem.SetFreeContent(freeContent);

    }
}
