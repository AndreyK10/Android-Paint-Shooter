using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum GameMode { ScoreMode, FreeMode, Menu, Lose }
    private GameMode _gameMode;

    [SerializeField]
    private HealthCounter HealthCounter;

    public delegate void StateChanged(GameMode gameMode);
    public event StateChanged OnStateChanged;

    private void Awake()
    {
        _gameMode = GameMode.Menu;
        OnStateChanged?.Invoke(_gameMode);
    }

    private void OnEnable()
    {
        HealthCounter.OnDead += OnDead;
    }

    private void OnDead()
    {
        _gameMode = GameMode.Lose;
        OnStateChanged?.Invoke(_gameMode);
    }

    public void FreeMode()
    {
        _gameMode = GameMode.FreeMode;
        OnStateChanged?.Invoke(_gameMode);

    }

    public void ScoreMode()
    {
        _gameMode = GameMode.ScoreMode;
        OnStateChanged?.Invoke(_gameMode);

    }

    public void Menu()
    {
        _gameMode = GameMode.Menu;
        OnStateChanged?.Invoke(_gameMode);
    }

    private void OnDisable()
    {
        HealthCounter.OnDead -= OnDead;
    }

}
