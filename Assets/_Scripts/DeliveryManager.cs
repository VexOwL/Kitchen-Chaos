using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private SO_RecipeList _recipesList;
    public static DeliveryManager Instance;
    private List<SO_Recipe> _waitingRecipes = new List<SO_Recipe>();
    private float _spawnRecipeTimer, _spawnRecipeTimerMax = 5;
    private int _maxRecipes = 3, _successfulDeliveriesAmount;
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFailed;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;

        if (_spawnRecipeTimer <= 0)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;

            if (GameManager.Instance.IsGamePlaying() && _waitingRecipes.Count < _maxRecipes)
            {
                SO_Recipe waitingRecipe = _recipesList.Recipes[Random.Range(0, _recipesList.Recipes.Count)];
                _waitingRecipes.Add(waitingRecipe);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverPlate(PlateKitchenObject plate)
    {
        for (int i = 0; i < _waitingRecipes.Count; i++)
        {
            SO_Recipe waitingRecipe = _waitingRecipes[i];

            if (waitingRecipe.Ingredients.Count == plate.GetKitchenObjectsOnPlate().Count)
            {
                bool plateMatchesRecipe = true;

                foreach (SO_KitchenObject waitingRecipeIngredient in waitingRecipe.Ingredients)
                {
                    bool ingredientFound = false;

                    foreach (SO_KitchenObject plateIngredient in plate.GetKitchenObjectsOnPlate())
                    {
                        if (plateIngredient == waitingRecipeIngredient)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                        plateMatchesRecipe = false;
                }

                if (plateMatchesRecipe)
                {
                    _waitingRecipes.RemoveAt(i);

                    _successfulDeliveriesAmount++;
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        OnDeliveryFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<SO_Recipe> GetWaitingRecipes()
    {
        return _waitingRecipes;
    }

    public int GetSuccesfulDeliveriesAmount()
    {
        return _successfulDeliveriesAmount;
    }
}
