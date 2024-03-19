using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<SO_KitchenObject> _validKitchenObjects;
    private List<SO_KitchenObject> _KitchenObjectsOnPlate = new List<SO_KitchenObject>();
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {public SO_KitchenObject KitchenObject;}

    public bool TryAddIngredient(SO_KitchenObject _SOKitchenObject)
    {
        if (_validKitchenObjects.Contains(_SOKitchenObject))
        {
            if (_KitchenObjectsOnPlate.Contains(_SOKitchenObject))
            {
                return false;
            }
            else
            {
                _KitchenObjectsOnPlate.Add(_SOKitchenObject);

                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {KitchenObject = _SOKitchenObject});
                
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}
