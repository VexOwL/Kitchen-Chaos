using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float _footstepTimer, _footstepTimerMax = 0.1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        _footstepTimer -= Time.deltaTime;
        if (_footstepTimer <= 0)
        {
            _footstepTimer = _footstepTimerMax;

            if (player.IsWalking)
            {
                float volume = 1;
                SoundManager.Instance.PlayFootstepSound(player.transform.position, volume);
            }
        }
    }
}
