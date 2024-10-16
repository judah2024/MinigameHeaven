using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Obeject")]
    public Animator anim;
    public GameObject square;
    public GameObject gameoverPanel;

    [Header("Text")]
    public Text timeText;
    public Text scoreText;
    public Text bestScoreText;
    public string bestScoreKey = "bestScore";

    float timer;
    public bool isPlaying = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
        Time.timeScale = 1.0f;
        gameoverPanel.SetActive(false);
        InvokeRepeating("CreateSquare", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying) 
        {
            timer += Time.deltaTime;
            timeText.text = timer.ToString("F2");
        }

    }

    private void CreateSquare()
    {
        Instantiate(square);
    }

    public void GameOver()
    {
        isPlaying = false;
        anim.SetBool("isDied", !isPlaying);

        Invoke("EndPlaying", 0.5f);
    }

    private void EndPlaying()
    {
        Time.timeScale = 0.0f;
        UpdateScore();
        gameoverPanel.SetActive(true);
    }

    private void UpdateScore()
    {
        float bestScore = PlayerPrefs.HasKey(bestScoreKey) ? PlayerPrefs.GetFloat(bestScoreKey) : 0.0f;
        if (timer > bestScore) 
        {
            bestScore = timer;
            PlayerPrefs.SetFloat(bestScoreKey, timer);
        }

        scoreText.text = string.Format("\t\t현재점수\t\t {0}", timer.ToString("F2"));
        bestScoreText.text = string.Format("\t\t현재점수\t\t {0}", bestScore.ToString("F2"));
    }
}
