using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [Header("Bottle Enemy")]
    [SerializeField] private int _bottlesIterations;
    [SerializeField] private float _waitBetweenSec;

    private void Start()
    {
        if(BottlesManager.Instance != null)
        {
            BottlesManager.Instance.StartCoroutine(BottlesManager.Instance.BottleCourutine(_waitBetweenSec, _bottlesIterations));
        }
    }
}
