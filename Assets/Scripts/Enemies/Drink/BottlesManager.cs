using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlesManager : MonoBehaviour
{
    public static BottlesManager Instance;
    [SerializeField] private List<GameObject> _bottles = new List<GameObject>();
    private int _amountOfBottles;
    private int _currentBottle;
    private void Awake()
    {
        Instance = this;
        foreach( GameObject bottle in _bottles)
        {
            bottle.GetComponent<TestBottle>().enabled = false;
        }
        _currentBottle = 0;
    }
    private void Start()
    {
        _amountOfBottles = _bottles.Count;
    }

    private void StartBottleFall()
    {
        if (_currentBottle <= _amountOfBottles)
        {
            _bottles[_currentBottle].GetComponent<TestBottle>().enabled = true;
            _currentBottle++;
        }
    }

    public IEnumerator BottleCourutine(float howMuchToWaitBetween, int iterations)
    {
        int _currentIteration = 0;
        while(_currentIteration < iterations)
        {
            StartBottleFall();
            _currentIteration++;
            yield return new WaitForSeconds(howMuchToWaitBetween);
        }
       
    }
}
