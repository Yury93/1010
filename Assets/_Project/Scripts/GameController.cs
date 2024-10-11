using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CellItem cellItem;
    public Transform freeContent;

    private void Awake()
    {
        cellItem.SetFreeContent(freeContent);
    }
}
