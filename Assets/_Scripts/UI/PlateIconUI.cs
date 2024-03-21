using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetKitchenObject(SO_KitchenObject _kitchenObjectSO)
    {
        _image.sprite = _kitchenObjectSO.Sprite;

    }
}
