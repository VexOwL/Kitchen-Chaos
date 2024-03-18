using System;
using UnityEngine;

public class ContainerVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter _container;
    private Animator _animator;
    private const string OPEN_CLOSE = "OpenClose";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _container.OnPlayerTookObject += Container_OnPlayerTookObject;
    }

    private void Container_OnPlayerTookObject(object sender, EventArgs eventArgs)
    {
        _animator.SetTrigger(OPEN_CLOSE);
    }
}
