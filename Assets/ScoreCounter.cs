using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    private HitTarget HitTarget;
    [SerializeField]
    private GameState GameState;
    [SerializeField]
    private Text text, hs;

    public int Score { get; private set; }

    private void OnEnable()
    {
        HitTarget.OnTargetHit += OnTargetHit;
        GameState.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameState.GameMode gameMode)
    {
        if (gameMode == GameState.GameMode.ScoreMode)
        {
            ResetCounter();
            UpdateCounter(text, Score);
        }
        else if (gameMode == GameState.GameMode.Lose)
        {
            
            if (Score > PlayerPrefs.GetInt("HS"))
            {
                PlayerPrefs.SetInt("HS", Score);
                UpdateCounter(hs, Score);
            }
            else
            {
                UpdateCounter(hs, PlayerPrefs.GetInt("HS"));
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
            UpdateCounter(text, Score);
        }
    }
    private void ResetCounter()
    {
        Score = 0;
    }

    private void UpdateCounter(Text text, int value)
    {
        text.text = value.ToString();
    }

    private void OnDisable()
    {
        HitTarget.OnTargetHit -= OnTargetHit;
        GameState.OnStateChanged -= OnStateChanged;
    }



}
