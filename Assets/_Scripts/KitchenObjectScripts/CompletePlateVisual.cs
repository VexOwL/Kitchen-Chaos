using System;
using System.Collections.Generic;
using UnityEngine;

public class CompletePlateVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject _plate;
    [SerializeField] private List<KitchenObject_GameObject> KitchenObjects_GameObjects;

    [Serializable]
    public struct KitchenObject_GameObject
    {
        public SO_KitchenObject KitchenObject;
        public GameObject GameObject;
    }

    private void Start()
    {
        _plate.OnIngredientAdded += Plate_OnIngredientAdded;

        foreach(KitchenObject_GameObject obj in KitchenObjects_GameObjects)
        {
            obj.GameObject.SetActive(false);
        }
    }

    private void Plate_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs eventArgs)
    {
        foreach(KitchenObject_GameObject obj in KitchenObjects_GameObjects)
        {
            if(obj.KitchenObject == eventArgs.KitchenObject)
            {
                obj.GameObject.SetActive(true);
            }
        }
        //eventArgs.KitchenObject;
    }
}
