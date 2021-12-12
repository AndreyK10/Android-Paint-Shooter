using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private SpawnPoint[] _spawnPoints;

    [SerializeField]
    private Target[] _targets;

    private Target[] _tempTargets;
    [SerializeField]
    private Transform _tempPosition;

    [SerializeField]
    private float _timeToHitMin, _timeToHitMax, _timeToSpawnMin, _timeToSpawnMax;

    [SerializeField]
    private GameState GameState;
    private bool isPaused;

    private void OnEnable()
    {

        GameState.OnStateChanged += OnStateChanged;

        _tempTargets = new Target[_targets.Length];
        for (int i = 0; i < _targets.Length; i++)
        {
            _tempTargets[i] = Instantiate(_targets[i], _tempPosition);
            _tempTargets[i].gameObject.SetActive(false);
        }
    }

    private void OnStateChanged(GameState.GameMode gameMode)
    {
        if (gameMode == GameState.GameMode.ScoreMode)
        {
            StartCoroutine("StartSpawning");

        }
        else
        {
            StopCoroutine("StartSpawning");
            for (int i = 0; i < _tempTargets.Length; i++)
            {
                _tempTargets[i].gameObject.SetActive(false);
            }
        }
    }


    private IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            Target target = _tempTargets[(Random.Range(0, _tempTargets.Length))];
            target.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;
            target.gameObject.SetActive(true);
            yield return new WaitForSeconds(Random.Range(_timeToHitMin, _timeToHitMax));
            target.gameObject.SetActive(false);
            yield return new WaitForSeconds(Random.Range(_timeToSpawnMin, _timeToSpawnMax));
        }
    }

    private void OnDisable()
    {
        GameState.OnStateChanged -= OnStateChanged;
    }

}
