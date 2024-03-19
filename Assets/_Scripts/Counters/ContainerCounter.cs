using System;
using UnityEngine;

public class ContainerCounter : Counter
{
    [SerializeField] private SO_KitchenObject _kitchenObjectSO;
    public event EventHandler OnPlayerTookObject;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(_kitchenObjectSO, player);

            OnPlayerTookObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
