using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter _platesCounter;
    [SerializeField] private Transform _kitchenObjectPosition;
    [SerializeField] private Transform _plateVisualPrefab;
    private float _plateSpawnOffset = 0.1f;
    private List<GameObject> _plateVisuals = new List<GameObject>();

    private void Start()
    {
        _platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        _platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs eventArgs)
    {
        GameObject plateGameObject = _plateVisuals[_plateVisuals.Count - 1];
        _plateVisuals.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs eventArgs)
    {
        Transform plateVisualTransform = Instantiate(_plateVisualPrefab, _kitchenObjectPosition);
        plateVisualTransform.localPosition = new Vector3(0, _plateSpawnOffset * _plateVisuals.Count, 0);

        _plateVisuals.Add(plateVisualTransform.gameObject);
    }
}
