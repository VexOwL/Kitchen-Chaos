using UnityEngine;

public class Counter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform _kitchenObjectPosition;
    private KitchenObject _kitchenObject;

    public virtual void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
                player.GetKitchenObject().SetKitchenObjectParent(this);
        }
        else
        {
            if (!player.HasKitchenObject())
                GetKitchenObject().SetKitchenObjectParent(player);
        }
    }

    public virtual void InteractAlternate(Player player)
    {
        
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
