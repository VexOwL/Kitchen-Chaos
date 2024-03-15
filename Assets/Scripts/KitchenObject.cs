using System;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private SO_KitchenObject _kitchenObjectSO;

    private IKitchenObjectParent KitchenObjectParent;

    public SO_KitchenObject GetKitchenObject()
    {
        return _kitchenObjectSO;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return KitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if(KitchenObjectParent != null)
            KitchenObjectParent.ClearKitchenObject();
        
        KitchenObjectParent = kitchenObjectParent;

        if(KitchenObjectParent.HasKitchenObject())
            Debug.LogError("Counter already has an object!");

        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = KitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
}
