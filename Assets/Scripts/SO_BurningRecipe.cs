using UnityEngine;

[CreateAssetMenu()]
public class SO_BurningRecipe : ScriptableObject
{
    public SO_KitchenObject Input;
    public SO_KitchenObject Output;
    public float BurningTimerMax;
}
