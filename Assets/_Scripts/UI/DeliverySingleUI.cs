using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipeName;
    [SerializeField] private Transform _iconContainer;
    [SerializeField] private Transform _iconTemplate;

    public void Awake()
    {
        _iconTemplate.gameObject.SetActive(false);
    }
    
    public void SetRecipeSO(SO_Recipe recipe)
    {
        _recipeName.text = recipe.RecipeName;

        foreach(Transform child in _iconContainer)
        {
            if(child == _iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(SO_KitchenObject ingredient in recipe.Ingredients)
        {
            Transform iconTransform = Instantiate(_iconTemplate, _iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = ingredient.Sprite;
        }
    }
}
