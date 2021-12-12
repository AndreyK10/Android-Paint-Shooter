using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameState GameState;
    [SerializeField]
    private CinemachineVirtualCamera vcam1, vcam2;


    private void OnEnable()
    {
        GameState.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameState.GameMode gameMode)
    {
        if (gameMode == GameState.GameMode.Menu || gameMode == GameState.GameMode.Lose)
        {
            ChangeCamera(vcam1, vcam2);
        }
        if (gameMode == GameState.GameMode.ScoreMode || gameMode == GameState.GameMode.FreeMode)
        {
            ChangeCamera(vcam2, vcam1);
        }
    }

    private void ChangeCamera(CinemachineVirtualCamera cameraTo, CinemachineVirtualCamera cameraFrom)
    {
        cameraTo.Priority = 10;
        cameraFrom.Priority = 9;
    }    

    private void OnDisable()
    {
        GameState.OnStateChanged -= OnStateChanged;
    }

}
