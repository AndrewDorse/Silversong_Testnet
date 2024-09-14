using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _oneSecondTimer;
    private float _threeTimesPerSecondTimer;
    private float _tenTimesPerSecondTimer;
    private float _everyFiveSecondsTimer;


    void Update()
    {
        _oneSecondTimer += Time.deltaTime;
        _threeTimesPerSecondTimer += Time.deltaTime;
        _tenTimesPerSecondTimer += Time.deltaTime;
        _everyFiveSecondsTimer += Time.deltaTime; 

        if (_oneSecondTimer >= 1)
        {
            EventsProvider.EverySecond?.Invoke();
            _oneSecondTimer = 0;
        }

        if (_threeTimesPerSecondTimer >= 0.33f)
        {
            EventsProvider.ThreeTimesPerSecond?.Invoke();
            _threeTimesPerSecondTimer = 0;
        }

        if (_tenTimesPerSecondTimer >= 0.1f)
        {
            EventsProvider.TenTimesPerSecond?.Invoke();
            _tenTimesPerSecondTimer = 0;
        }

        if (_everyFiveSecondsTimer >= 5f)
        {
            EventsProvider.EveryFiveSeconds?.Invoke();
            _everyFiveSecondsTimer = 0;
        }

    }
}
