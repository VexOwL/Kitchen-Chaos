using System;
using UnityEngine;

public class CuttingCounter : Counter, IHasProgress
{
    [SerializeField] private SO_CuttingRecipe[] _cuttingRecipes;
    private int _cuttingProgress;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    public static event EventHandler OnAnyCut;
    new public static void ResetStaticData()
    {
        OnAnyCut = null;
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

                    _cuttingProgress = 0;

                    SO_CuttingRecipe cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.CuttingProgressMax
                    });
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
                        GetKitchenObject().DestroySelf();
                }
            }
            else
            {
                if (!player.HasKitchenObject())
                    GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            _cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            SO_CuttingRecipe cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)_cuttingProgress / cuttingRecipeSO.CuttingProgressMax
            });

            if (_cuttingProgress >= cuttingRecipeSO.CuttingProgressMax)
            {
                SO_KitchenObject outputKitchenObject = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
            }
        }
    }

    private bool HasRecipeWithInput(SO_KitchenObject inputKitchenObject)
    {
        SO_CuttingRecipe cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObject);

        return cuttingRecipeSO != null;
    }

    private SO_KitchenObject GetOutputForInput(SO_KitchenObject inputKitchenObject)
    {
        SO_CuttingRecipe cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObject);

        if (cuttingRecipeSO != null)
            return cuttingRecipeSO.Output;
        else
            return null;
    }

    private SO_CuttingRecipe GetCuttingRecipeSOWithInput(SO_KitchenObject inputKitchenObjectSO)
    {
        foreach (SO_CuttingRecipe recipe in _cuttingRecipes)
        {
            if (recipe.Input == inputKitchenObjectSO)
                return recipe;
        }
        return null;
    }
}
