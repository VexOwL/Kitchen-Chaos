using System;
using UnityEngine;

public class StoveCounter : Counter, IHasProgress
{
    [SerializeField] private SO_FryingRecipe[] _fryingRecipes;
    [SerializeField] private SO_BurningRecipe[] _burningRecipes;
    private SO_FryingRecipe _fryingRecipeSO;
    private SO_BurningRecipe _burningRecipeSO;
    private float _fryingTimer;
    private float _burningTimer;
    public enum State { Idle, Frying, Fried, Burnt }
    private State _state;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs { public State State; }

    private void Start()
    {
        _state = State.Idle;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.Idle:
                break;

            case State.Frying:
                _fryingTimer += Time.deltaTime;

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = _fryingTimer / _fryingRecipeSO.FryingTimerMax });

                if (_fryingTimer > _fryingRecipeSO.FryingTimerMax)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_fryingRecipeSO.Output, this);

                    _state = State.Fried;
                    _burningTimer = 0;
                    _burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { State = _state });
                }
                break;

            case State.Fried:
                _burningTimer += Time.deltaTime;

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = _burningTimer / _burningRecipeSO.BurningTimerMax });

                if (_burningTimer > _burningRecipeSO.BurningTimerMax)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_burningRecipeSO.Output, this);

                    _state = State.Burnt;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { State = _state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0 });
                }
                break;

            case State.Burnt:
                break;
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    _fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    _state = State.Frying;
                    _fryingTimer = 0;

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { State = _state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = _fryingTimer / _fryingRecipeSO.FryingTimerMax });
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        _state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { State = _state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0 });
                    }
                }
            }
            else
            {
                if (!player.HasKitchenObject())
                {
                    GetKitchenObject().SetKitchenObjectParent(player);

                    _state = State.Idle;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { State = _state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0 });
                }
            }
        }
    }

    private bool HasRecipeWithInput(SO_KitchenObject inputKitchenObject)
    {
        SO_FryingRecipe fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObject);

        return fryingRecipeSO != null;
    }

    private SO_KitchenObject GetOutputForInput(SO_KitchenObject inputKitchenObject)
    {
        SO_FryingRecipe fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObject);

        if (fryingRecipeSO != null)
            return fryingRecipeSO.Output;
        else
            return null;
    }

    private SO_FryingRecipe GetFryingRecipeSOWithInput(SO_KitchenObject inputKitchenObjectSO)
    {
        foreach (SO_FryingRecipe recipe in _fryingRecipes)
        {
            if (recipe.Input == inputKitchenObjectSO)
                return recipe;
        }
        return null;
    }

    private SO_BurningRecipe GetBurningRecipeSOWithInput(SO_KitchenObject inputKitchenObjectSO)
    {
        foreach (SO_BurningRecipe recipe in _burningRecipes)
        {
            if (recipe.Input == inputKitchenObjectSO)
                return recipe;
        }
        return null;
    }
}