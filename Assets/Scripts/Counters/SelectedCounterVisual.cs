using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private Counter _counter; 
    [SerializeField] private GameObject[] _visualGameObjects; 
    
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged; //подписка на ивент из Player
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs eventArgs)
    {
        if(eventArgs.selectedCounter == _counter)
        {
            SetVisual(true);
        } else {
            SetVisual(false);
        }
    }

    private void SetVisual(bool truefalse)
    {
        foreach (GameObject visualObject in _visualGameObjects)
        {
            visualObject.SetActive(truefalse);
        }
    }
}
