using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text readyText;
    public TMP_Text countText;
    public TMP_Text gameEndText;
    public GameObject gameEndUI;
    public MatchUI matchUI;

    void Start()
    {
        GameManager.Instance.OnTimerUpdated += UpdateTimerUI;
        GameManager.Instance.OnReadyUpdated += UpdateReadyUI;
        GameManager.Instance.OnCountUpdated  += UpdateCountUI;
        GameManager.Instance.OnGameEnded += ActivateGameEndUI;
        GameManager.Instance.OnMatchCard += Match;
        
        
        gameEndUI.SetActive(false);
        countText.gameObject.SetActive(false);
    }

    void UpdateTimerUI(float seconds)
    {
        timerText.text = seconds.ToString("F2");
        timerText.color = GameManager.Instance.IsHurryUp() ? Color.red : Color.white;
    }
    
    void UpdateReadyUI(float seconds)
    {
        readyText.text = seconds.ToString("0");
        if (seconds <= 0)
        {
            readyText.gameObject.SetActive(false);
        }
    }

    void UpdateCountUI(int score)
    {
        countText.text = $"match 횟수 : {score}";
    }

    void ActivateGameEndUI(bool isWin)
    {
        gameEndUI.SetActive(true);
        gameEndText.text = isWin ? "You Win!" : "Game Over...";
        countText.gameObject.SetActive(true);
    }

    void Match(bool isCollect)
    {
        matchUI.gameObject.SetActive(true);
        matchUI.PlayMatchAnimation(isCollect);

    }
}
