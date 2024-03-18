using UnityEngine;

public class TrashCounter : Counter
{
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
            player.GetKitchenObject().DestroySelf();
    }
    
}