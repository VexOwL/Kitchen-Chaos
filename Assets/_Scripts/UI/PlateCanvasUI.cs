using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCanvasUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject _plate;
    [SerializeField] private Transform _iconTemplate;

    private void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);
    }
    
    private void Start()
    {
        _plate.OnIngredientAdded += Plate_OnIngredientAdded;
    }

    private void Plate_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs eventArgs)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if(child == _iconTemplate) continue;
            Destroy(child.gameObject);
        }
        
        foreach(SO_KitchenObject kitchenObject in _plate.GetKitchenObjectsOnPlate())
        {
            Transform iconTransform = Instantiate(_iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconUI>().SetKitchenObject(kitchenObject);
        }
    }
}