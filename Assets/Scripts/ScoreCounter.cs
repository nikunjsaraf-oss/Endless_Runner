using System;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText = null;
    [SerializeField] int maxDifficulty = 10;
    [SerializeField] int scoreToNextLevel = 20;

    float scoreCounter = 0;
    int difficulty = 1;

    private void Start()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = $"Score: {(int)scoreCounter}";
    }

    private void Update()
    {
       if (!PlayerManager.isGameStarted) return;

        CalculateScore();
        scoreCounter += Time.deltaTime;
        UpdateScore();

    }

    private void CalculateScore()
    {
        if (scoreCounter >= scoreToNextLevel)
        {
            IncreaseDifficulty();
        }
    }

    private void IncreaseDifficulty()
    {
        if (difficulty > maxDifficulty)
        {
            return;
        }

        scoreToNextLevel *= 2;
        difficulty++;
    }
}
