using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private GameObject _hasProgressGameObj;

    private IHasProgress _hasProgress;

    private void Start()
    {
        _hasProgress = _hasProgressGameObj.GetComponent<IHasProgress>();
        if(_hasProgress == null)
            Debug.LogError("Game object doesn't have 'IHasProgress'");
        
        _hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        _barImage.fillAmount = 0;

        HideBar();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs eventArgs)
    {
        _barImage.fillAmount = eventArgs.progressNormalized;

        if(eventArgs.progressNormalized <= 0 || eventArgs.progressNormalized >= 1)
        {
            HideBar();
        }
        else
        {
            ShowBar();
        }
    }

    private void HideBar()
    {
        gameObject.SetActive(false);
    }

    private void ShowBar()
    {
        gameObject.SetActive(true);
    }
}
