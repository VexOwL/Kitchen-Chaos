using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_Recipe : ScriptableObject
{
    public string RecipeName;
    public List<SO_KitchenObject> Ingredients;
}
