using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupClose : MonoBehaviour
{
    public Button closePopupButton;
    public Button closeFrameButton;

    UIController UIController;
    public void Init(UIController uIController)
    {
        this.UIController = uIController;
        gameObject.SetActive(false);
        closePopupButton.onClick.AddListener(Close);
        closeFrameButton.onClick.AddListener(CloseFrame);
    }

    private void CloseFrame()
    {
        UIController.CloseGame();
    }

    public void Open()
    {
        gameObject.SetActive (true);
    }
    public void Close() 
    {
        gameObject.SetActive(false);
    }
     
}
