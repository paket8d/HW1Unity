using System;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [Header("Game Speed Settings")]
    [SerializeField] private float currentGameSpeed = 1f;
    [SerializeField] private float speedIncreaseRate = 0.05f;
    [SerializeField] private float maxGameSpeed = 3f;
    [Header("Score Settings")]
    [SerializeField] private int currentScore = 0;
    [SerializeField] private float scoreIncreaseRate = 10;
    [SerializeField] private int bestScore = 0; 
    
    private float _scoreAccumulator = 0f;
    private const string BestScoreKey = "BestScore";
    
    public float CurrentGameSpeed { get => currentGameSpeed; private set => currentGameSpeed = value; }
    public int CurrentScore { get => currentScore; private set => currentScore = value; }
    public int BestScore { get => bestScore; private set => bestScore = value; }

    private void Awake()
    {
        bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
    }

    private void Start()
    {
        ResetRun();
    }

    void Update()
    {
        CurrentGameSpeed = Mathf.Min(maxGameSpeed, CurrentGameSpeed + speedIncreaseRate * Time.deltaTime);
        
        _scoreAccumulator += scoreIncreaseRate * currentGameSpeed * Time.deltaTime;
        int newScore = Mathf.FloorToInt(_scoreAccumulator);
        if (newScore > CurrentScore)
        {
            currentScore = newScore;
            TryUpdateBestScore();
        }
    }

    private void TryUpdateBestScore()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
            PlayerPrefs.Save();
        }
    }

    public void ResetRun()
    {
        currentGameSpeed = 1f;
        currentScore = 0;
        _scoreAccumulator = 0f;
    }
}
