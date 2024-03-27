using UnityEngine;

public class BurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter _stove;

    private void Start()
    {
        _stove.OnProgressChanged += Stove_OnProgressChanged;

        gameObject.SetActive(false);
    }

    private void Stove_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs eventArgs)
    {
        float burntSoon = 0.5f;
        bool show = _stove.IsFried() && eventArgs.progressNormalized >= burntSoon;

        if(show)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
