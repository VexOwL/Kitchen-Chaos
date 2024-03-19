using System;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private SO_KitchenObject _kitchenObjectSO;

    private IKitchenObjectParent KitchenObjectParent;

    public SO_KitchenObject GetKitchenObjectSO()
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

    public void DestroySelf()
    {
        KitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(SO_KitchenObject kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
}
