using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameState GameState;

    public Text ScoreText, HighscoreText, HealthText;
    public Button FreeModeButton, ScoreModeButton, MenuButton;

    private void OnEnable()
    {
        GameState.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameState.GameMode gameMode)
    {
        if (gameMode == GameState.GameMode.Menu)
        {
            HideCounter(HealthText);
            HideCounter(HighscoreText);
            HideCounter(ScoreText);
            ShowButton(FreeModeButton);
            ShowButton(ScoreModeButton);
            HideButton(MenuButton);

        }
        if (gameMode == GameState.GameMode.ScoreMode)
        {
            ShowCounter(HealthText);
            HideCounter(HighscoreText);
            ShowCounter(ScoreText);
            HideButton(FreeModeButton);
            HideButton(ScoreModeButton);
            ShowButton(MenuButton);
        }
        if (gameMode == GameState.GameMode.FreeMode)
        {
            HideCounter(HealthText);
            HideCounter(HighscoreText);
            HideCounter(ScoreText);
            HideButton(FreeModeButton);
            HideButton(ScoreModeButton);
            ShowButton(MenuButton);

        }
        if (gameMode == GameState.GameMode.Lose)
        {
            HideCounter(HealthText);
            ShowCounter(HighscoreText);
            ShowCounter(ScoreText);
            HideButton(FreeModeButton);
            HideButton(ScoreModeButton);
            ShowButton(MenuButton);
        }
    }

    private void ShowCounter(Text text)
    {
        text.gameObject.SetActive(true);
    }
    private void HideCounter(Text text)
    {
        text.gameObject.SetActive(false);
    }
    private void ShowUIGroup()
    {

    }

    private void ShowButton(Button button)
    {
        button.gameObject.SetActive(true);
    }

    private void HideButton(Button button)
    {
        button.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GameState.OnStateChanged -= OnStateChanged;
    }

}
