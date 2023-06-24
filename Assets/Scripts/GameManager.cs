using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float gameSpeed { get; private set; }

    [SerializeField] private float gameSpeedIncrease = 0.1f;
    [SerializeField] private float initialGameSpeed = 5f;

    private Player Player;
    private Spanwer Spanwer;

    

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

    private void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles) {
            Destroy(obstacle.gameObject);
        }
        
        gameSpeed = initialGameSpeed;
        enabled = true;
        
        Player.gameObject.SetActive(true);
        Spanwer.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;
        
        Player.gameObject.SetActive(false);
        Spanwer.gameObject.SetActive(false);
        
     }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
    }
}
