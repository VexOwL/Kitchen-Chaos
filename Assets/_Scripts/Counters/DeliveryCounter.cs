using UnityEngine;

public class DeliveryCounter : Counter
{
    public static DeliveryCounter Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
            {
                DeliveryManager.Instance.DeliverPlate(plate);
                
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
