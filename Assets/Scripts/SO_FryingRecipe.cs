using UnityEngine;

[CreateAssetMenu()]
public class SO_FryingRecipe : ScriptableObject
{
    public SO_KitchenObject Input;
    public SO_KitchenObject Output;
    public float FryingTimerMax;
}
