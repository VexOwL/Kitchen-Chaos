using System;
using UnityEngine;

public class StoveSound : MonoBehaviour
{
    [SerializeField] private StoveCounter _stove;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _stove.OnStateChanged += Stove_OnStateChanged;
    }

    private void Stove_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs eventArgs)
    {
        bool playSound = eventArgs.State != StoveCounter.State.Idle;

        if(playSound)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }
}
