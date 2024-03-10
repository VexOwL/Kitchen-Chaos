using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Player _player;
    private  Animator _animator;
    private const string IS_WALKING = "IsWalking";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(IS_WALKING, _player.IsWalking);
    }
}
