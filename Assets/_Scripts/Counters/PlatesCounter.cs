using System;
using UnityEngine;

public class PlatesCounter : Counter
{
    [SerializeField] private SO_KitchenObject _plateKitchenObjectSO;
    private float _plateSpawnTimer, _plateSpawnTimerMax = 4;
    private int _platesAmount, _platesAmountMax = 5;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    private void Update()
    {
        _plateSpawnTimer += Time.deltaTime;

        if (_plateSpawnTimer > _plateSpawnTimerMax)
        {
            _plateSpawnTimer = 0;

            if (GameManager.Instance.IsGamePlaying() && _platesAmount < _platesAmountMax)
            {
                _platesAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (_platesAmount > 0)
            {
                _platesAmount--;
                KitchenObject.SpawnKitchenObject(_plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
