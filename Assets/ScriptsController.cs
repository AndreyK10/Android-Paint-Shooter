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
            SwitchLauncher();
        }
        else if (gameMode == GameState.GameMode.FreeMode || gameMode == GameState.GameMode.ScoreMode)
        {
            StartCoroutine(SwitchLauncher(true));
        }
    }

    private void OnDisable()
    {
        GameState.OnStateChanged -= OnStateChanged;
    }

    private void SwitchLauncher()
    {
        Launcher.enabled = false;
    }

    private IEnumerator SwitchLauncher(bool launcher)
    {
        yield return new WaitForSeconds(2f);
        Launcher.enabled = launcher;
    }

}
