using UnityEngine;
using UnityEngine.UI;

public class PlayTimeLeftUI : MonoBehaviour
{
    [SerializeField] private Image _timer;

    private void Update()
    {
        _timer.fillAmount = GameManager.Instance.GetGameTimerNormalized();
    }
}
