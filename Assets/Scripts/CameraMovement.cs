using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameState GameState;
    [SerializeField]
    private CinemachineVirtualCamera _vcam1, _vcam2;


    private void OnEnable()
    {
        GameState.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameState.GameMode gameMode)
    {
        if (gameMode == GameState.GameMode.Menu || gameMode == GameState.GameMode.Lose)
        {
            ChangeCamera(_vcam1, _vcam2);
        }
        if (gameMode == GameState.GameMode.ScoreMode || gameMode == GameState.GameMode.FreeMode)
        {
            ChangeCamera(_vcam2, _vcam1);
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
