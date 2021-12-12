using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private HitTarget HitTarget;
    [SerializeField]
    private GameState GameState;
    [SerializeField]
    private TextMeshProUGUI _scoreText, _highscoreText, _highscoreMenuText;

    public int Score { get; private set; }

    private void OnEnable()
    {
        HitTarget.OnTargetHit += OnTargetHit;
        GameState.OnStateChanged += OnStateChanged;
    }
    private void Start()
    {
        UpdateHighscore(_highscoreMenuText, PlayerPrefs.GetInt("HS"));
    }
    private void OnStateChanged(GameState.GameMode gameMode)
    {
        if (gameMode == GameState.GameMode.ScoreMode)
        {
            ResetCounter();
            UpdateCounter(_scoreText, Score);
        }
        else if (gameMode == GameState.GameMode.Lose)
        {
            
            if (Score > PlayerPrefs.GetInt("HS"))
            {
                PlayerPrefs.SetInt("HS", Score);
                UpdateHighscore(_highscoreText, Score);
                UpdateHighscore(_highscoreMenuText, Score);
            }
            else
            {
                UpdateHighscore(_highscoreText, PlayerPrefs.GetInt("HS"));
            }
        } 
        else
        {
            ResetCounter();
        }
    }


    private void OnTargetHit(Target.TargetType targetType, int value)
    {
        if (targetType == Target.TargetType.ScoreTarget)
        {
            Score += value;
            UpdateCounter(_scoreText, Score);
        }
    }
    private void ResetCounter()
    {
        Score = 0;
    }

    private void UpdateCounter(TextMeshProUGUI text, int value)
    {
        text.text = value.ToString();
    }

    private void UpdateHighscore(TextMeshProUGUI text, int value)
    {
        text.text = "HIGHSCORE: " + value.ToString();
    }

    private void OnDisable()
    {
        HitTarget.OnTargetHit -= OnTargetHit;
        GameState.OnStateChanged -= OnStateChanged;
    }



}
