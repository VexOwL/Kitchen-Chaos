using System;
using UnityEngine;

public class StoveSound : MonoBehaviour
{
    [SerializeField] private StoveCounter _stove;
    private AudioSource _audioSource;
    private float _warningSoundTimer, _warningSoundTimerMax = 0.3f;
    private bool _playWarningSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _stove.OnStateChanged += Stove_OnStateChanged;
        _stove.OnProgressChanged += Stove_OnProgressChanged;
    }

    private void Update()
    {
        if (_playWarningSound)
        {
            _warningSoundTimer -= Time.deltaTime;
            if (_warningSoundTimer <= 0)
            {
                _warningSoundTimer = _warningSoundTimerMax;
                SoundManager.Instance.PlayWarningSound(_stove.transform.position);
            }
        }
    }

    private void Stove_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs eventArgs)
    {
        float burntSoon = 0.5f;
        _playWarningSound = _stove.IsFried() && eventArgs.progressNormalized >= burntSoon;
    }

    private void Stove_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs eventArgs)
    {
        bool playSound = eventArgs.State != StoveCounter.State.Idle;

        if (playSound)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }
}
