
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
    [SerializeField] private TextMeshProUGUI scoreText,scoreText2, recordText,recordText2;
    [SerializeField] private Image fade;
    [SerializeField] private float animationDuration ;
    [SerializeField] private GameClose gameClose;
    [SerializeField ] private PopupClose popupClose;
    public string GAME_OVER = "GAME OVER";
    public string TAP_TO_START = "TAP TO START";
 
   // public string RECORD = "RECORDPREFS";
  
    int score, record;  
    public void Init()
    {
        GameController.instance.onAddScore += AddScore;
        GameController.instance.onStart += OnStart;
        startButton.onClick.AddListener(() => GameController.instance.IsStartGame = true);
        GameController.instance.onEnd += OnEndGame;
        StartCoroutine(AnimateTextCoroutine(TAP_TO_START, tapToStartText));
        gameoverText.gameObject.SetActive(false);
        recordText.gameObject.SetActive(false);
        recordText2.gameObject.SetActive(false);
        popupClose.Init(this);
      //  var recordstr = Jammer.PlayerPrefs.GetString(RECORD);
      //  if (string.IsNullOrEmpty(recordstr) == false) record = Int32.Parse(recordstr);
      // ShowRecord();
    }
    public void OpenPopupClose()
    {
        popupClose.Open();
    }
    private void ShowRecord()
    {
        recordText.text = record.ToString();
        recordText2.text = record.ToString();
    }
    public void AddScore(int score)
    {
        this.score+= score;
        if(this.score > record)
        {
            record = this.score;
          //  Jammer.PlayerPrefs.SetString(RECORD,record.ToString());
           // ShowRecord();
        }
        ShowScore();
        gameClose.CallAddScore(this. score);
    }

    private void ShowScore()
    {
        scoreText.text = score.ToString();
        scoreText2.text = score.ToString();
    }

    private void OnStart()
    {
        fade.gameObject.SetActive(false);
        tapToStartText.gameObject.SetActive(false);
    }
    private void OnEndGame()
    {
        ShowScore();
      //  ShowRecord();
        startButton.onClick.RemoveAllListeners();
        fade.gameObject.SetActive(true);
        gameoverText.gameObject.SetActive(true);
        StartCoroutine(AnimateTextCoroutine(GAME_OVER, gameoverText));
        startButton.onClick.AddListener(CloseGame);
        //StartCoroutine(AnimateTextCoroutine());
    }

    public void CloseGame()
    {
        Debug.Log("game over");
        gameClose.CloseCurrentFrame();
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
