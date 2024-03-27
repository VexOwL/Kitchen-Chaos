using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _massageText;
    [SerializeField] private Color _successColor;
    [SerializeField] private Color _failColor;
    [SerializeField] private Sprite _successSprite;
    [SerializeField] private Sprite _failSprite;
    private Animator _animator;
    private const string POPUP = "Popup";
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;

        gameObject.SetActive(false);
    }
    
    private void DeliveryManager_OnDeliverySuccess(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(POPUP);

        _backgroundImage.color = _successColor;
        _iconImage.sprite = _successSprite;
        _massageText.text = "Delivery\nsuccess";
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(POPUP);
        
        _backgroundImage.color = _failColor;
        _iconImage.sprite = _failSprite;
        _massageText.text = "Delivery\nfailed";
    }
}
