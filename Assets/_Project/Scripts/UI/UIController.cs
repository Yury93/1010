
using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI tapToStartText, gameoverText;
    [SerializeField] private Image fade;
    [SerializeField] private float animationDuration ;
    [SerializeField] private Color[] colors;
    public string GAME_OVER = "GAME OVER";
    public string TAP_TO_START = "TAP TO START";
    public void Init()
    {
        GameController.instance.onStart += OnStart;
        startButton.onClick.AddListener(() => GameController.instance.IsStartGame = true);
        GameController.instance.onEnd += OnEndGame;
        StartCoroutine(AnimateTextCoroutine(TAP_TO_START, tapToStartText));
        gameoverText.gameObject.SetActive(false);



     
    }
    private void OnStart()
    {
        fade.gameObject.SetActive(false);
        tapToStartText.gameObject.SetActive(false);
    }
    private void OnEndGame()
    {
        startButton.onClick.RemoveAllListeners();
        fade.gameObject.SetActive(true);
        gameoverText.gameObject.SetActive(true);
        StartCoroutine(AnimateTextCoroutine(GAME_OVER, gameoverText));
        //startButton.onClick.AddListener(CloseGame);
        //StartCoroutine(AnimateTextCoroutine());
    }

    IEnumerator AnimateTextCoroutine(string text,TextMeshProUGUI textMeshProUGUI)
    {
        textMeshProUGUI.text = "";
       
        foreach (char letter in text)
        {
            textMeshProUGUI.text += letter;
            textMeshProUGUI.ForceMeshUpdate();
              
            yield return new WaitForSeconds(animationDuration);
        }
 

        textMeshProUGUI.transform.DOShakeScale(6, 0.1f, 1);
    
    }


    //    private void CloseGame()
    //{
    //    Debug.LogError("Игра завершена");
    //} 
}
