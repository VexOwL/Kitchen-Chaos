using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter _clearCounter; 
    [SerializeField] private GameObject _visualGameObject; 
    
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged; //подписка на ивент из Player
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs eventArgs)
    {
        if(eventArgs.selectedCounter == _clearCounter)
        {
            _visualGameObject.SetActive(true);
        } else {
            _visualGameObject.SetActive(false);
        }
    }
}
