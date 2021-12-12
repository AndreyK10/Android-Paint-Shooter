using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsController : MonoBehaviour
{
    public GameState GameState;
    public Launcher Launcher;



    private void OnEnable()
    {
        GameState.OnStateChanged += OnStateChanged;
    }


    private void OnStateChanged(GameState.GameMode gameMode)
    {
        if (gameMode == GameState.GameMode.Menu || gameMode == GameState.GameMode.Lose)
        {
            SwithcLauncher(false);
        }
        else if (gameMode == GameState.GameMode.FreeMode || gameMode == GameState.GameMode.ScoreMode)
        {
            SwithcLauncher(true);
        }
    }

    private void OnDisable()
    {
        GameState.OnStateChanged -= OnStateChanged;
    }


    private void SwithcLauncher(bool launcher)
    {
        Launcher.enabled = launcher;
    }

}
