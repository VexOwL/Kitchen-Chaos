using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private SO_KitchenObject _kitchenObjectSO;
    [SerializeField] private Transform _kitchenObjectPosition;
    private KitchenObject _kitchenObject;

    public void Interact(Player player)
    {
        if (_kitchenObject == null)
        {
            Transform kitchenObject = Instantiate(_kitchenObjectSO.Prefab, _kitchenObjectPosition);
            kitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            _kitchenObject.SetKitchenObjectParent(player); 
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _kitchenObjectPosition;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null; 
    }
}
