using System;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter _cuttingCounter;
    private Animator _animator;
    private const string CUT = "Cut";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, EventArgs eventArgs)
    {
        _animator.SetTrigger(CUT);
    }
}
