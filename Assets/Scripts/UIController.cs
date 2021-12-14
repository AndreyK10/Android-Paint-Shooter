using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameState GameState;

    public TextMeshProUGUI ScoreText, HighscoreText, HealthText, HighscoreMenuText;
    public Button FreeModeButton, ScoreModeButton, MenuButton, RestartButton, MenuLoseButton, ClearParticlesButton;

    private void OnEnable()
    {
        GameState.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameState.GameMode gameMode)
    {
        if (gameMode == GameState.GameMode.Menu)
        {
            SwitchCountersState(false, false, false, true);
            SwitchButtonsState(true, true, false, false, false, false);
        }
        if (gameMode == GameState.GameMode.ScoreMode)
        {
            SwitchCountersState(true, false, true, false);
            SwitchButtonsState(false, false, true, false, false, true);
        }
        if (gameMode == GameState.GameMode.FreeMode)
        {
            SwitchCountersState(false, false, false, false);
            SwitchButtonsState(false, false, true, false, false, true);
        }
        if (gameMode == GameState.GameMode.Lose)
        {
            SwitchCountersState(true, true, false, false);
            SwitchButtonsState(false, false, false, true, true, false);
        }
    }

    private void SwitchCountersState(bool score, bool highscore, bool health, bool highscoreMenu)
    {
        ScoreText.gameObject.SetActive(score);
        HighscoreText.gameObject.SetActive(highscore);
        HealthText.gameObject.SetActive(health);
        HighscoreMenuText.gameObject.SetActive(highscoreMenu);
    }

    private void SwitchButtonsState(bool scoreMode, bool freeMode, bool menu, bool restart, bool menuLose, bool clearParticles)
    {
        FreeModeButton.gameObject.SetActive(freeMode);
        ScoreModeButton.gameObject.SetActive(scoreMode);
        MenuButton.gameObject.SetActive(menu);
        RestartButton.gameObject.SetActive(restart);
        MenuLoseButton.gameObject.SetActive(menuLose);
        ClearParticlesButton.gameObject.SetActive(clearParticles);
    }

    private void OnDisable()
    {
        GameState.OnStateChanged -= OnStateChanged;
    }

}
