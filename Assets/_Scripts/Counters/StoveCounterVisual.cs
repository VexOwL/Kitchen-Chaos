using System;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject _stoveOn;
    [SerializeField] private GameObject _stoveParticles;
    [SerializeField] private StoveCounter _stoveCounter;

    private void Start()
    {
        _stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs eventArgs)
    {
        bool showStoveOn = eventArgs.State != StoveCounter.State.Idle;
        bool showParticles = eventArgs.State == StoveCounter.State.Frying || eventArgs.State == StoveCounter.State.Fried;
        
        _stoveOn.SetActive(showStoveOn);
        _stoveParticles.SetActive(showParticles);
    }
}
