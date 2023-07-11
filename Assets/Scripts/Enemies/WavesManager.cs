using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Wave[] waves;

    private int _waveIndex;
    private Wave _currentWave;
    private State _state;

    private enum State
    {
        SpawningWave,
        WaitingWaveSpawn,
        WaitingWaveEnd,
        Idle
    }

    private void Update()
    {
        switch (_state)
        {
            case State.SpawningWave:
                StartCoroutine(SpawnWaveCoroutine());
                break;

            case State.WaitingWaveSpawn:
                break;

            case State.WaitingWaveEnd:
                if (_currentWave.IsFinished())
                {
                    _state = _waveIndex == waves.Length ? State.Idle : State.SpawningWave;
                    _currentWave.Destroy();
                }
                break;

            case State.Idle:
                break;

            default:
                break;
        }
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        _state = State.WaitingWaveSpawn;
        yield return new WaitForSeconds(waves[_waveIndex].timeAfterPrevWave);

        waves[_waveIndex].InitEnemies(transform);
        _currentWave = waves[_waveIndex];
        _waveIndex++;
        _state = State.WaitingWaveEnd;
    }

}

[Serializable]
public class Wave
{
    [SerializeField] private GameObject enemiesPrefab;
    [HideInInspector] public GameObject enemies;
    public float timeAfterPrevWave;

    public void InitEnemies(Transform transform) => enemies = MonoBehaviour.Instantiate(enemiesPrefab, transform);
    public bool IsFinished() => enemies.transform.childCount == 0;
    public void Destroy() => MonoBehaviour.Destroy(enemies.gameObject);
}
