using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject priateCat;
    public GameObject fatCat;
    public GameObject normalCat;
    public GameObject restartButton;

    [Header("UI")]
    public Text levelText;
    public GameObject levelFront;

    int level = 0;
    int score = 0;
    bool isPlaying;

    public bool IsPlaying
    {
        get { return isPlaying; }
    }

    void Start()
    {
        isPlaying = true;
        Time.timeScale = 1.0f;
        restartButton.SetActive(!isPlaying);
        levelFront.transform.localScale  = new Vector3(0.0f, 1.0f, 1.0f);
        InvokeRepeating("CreateCat", 0.0f, 1.0f);
    }

    private void CreateCat()
    {
        Instantiate(normalCat);

        float p = Random.Range(0.0f, 10.0f);
        if (level == 1)
        {
            if (p < 2) Instantiate(normalCat);
        }
        else if (level == 2)
        {
            if (p < 5) Instantiate(normalCat);
        }
        else if (level >= 3)
        {
            if (level >= 4) Instantiate(priateCat);
            Instantiate(fatCat);
        }
    }

    public void OnGameOver()
    {
        isPlaying = false;
        restartButton.SetActive(!isPlaying);
        Time.timeScale = 0.0f;
    }

    public void AddScore()
    {
        score++;
        level = score / 3;

        levelText.text = level.ToString();
        levelFront.transform.localScale = new Vector3((score % 3) / 3.0f, 1.0f, 1.0f);
    }
}
