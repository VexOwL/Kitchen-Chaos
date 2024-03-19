using UnityEngine;

[CreateAssetMenu()]
public class SO_CuttingRecipe : ScriptableObject
{
    public SO_KitchenObject Input;
    public SO_KitchenObject Output;
    public int CuttingProgressMax;
}
