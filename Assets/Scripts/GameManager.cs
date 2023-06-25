using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float gameSpeed { get; private set; }

    [SerializeField] private float gameSpeedIncrease = 0.1f;
    [SerializeField] private float initialGameSpeed = 5f;

    private Player Player;
    private Spanwer Spanwer;

    private float score;

  [SerializeField] private TextMeshProUGUI gameOverText;
  [SerializeField] private TextMeshProUGUI scoreText;
  [SerializeField] private TextMeshProUGUI hiscoreText;
  [SerializeField] private Button retryButton;

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Player = FindObjectOfType<Player>();
        Spanwer = FindObjectOfType<Spanwer>();
        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles) {
            Destroy(obstacle.gameObject);
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;
        
        Player.gameObject.SetActive(true);
        Spanwer.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;
        
        Player.gameObject.SetActive(false);
        Spanwer.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        
        UpdateHiscore();
        
     }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.RoundToInt(score).ToString("D5");
    }

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore",hiscore);
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
}
