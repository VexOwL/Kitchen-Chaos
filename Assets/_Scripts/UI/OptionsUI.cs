using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button _soundSfxButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TextMeshProUGUI _soundSfxText;
    [SerializeField] private TextMeshProUGUI _musicText;
    public static OptionsUI Instance {get; private set;}

    private void Awake()
    {
        Instance = this;

        _soundSfxButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        _musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        _exitButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        
        UpdateVisual();
        Hide();
    }

    private void UpdateVisual()
    {
        _soundSfxText.text = "Sound effects: " + Mathf.Ceil(SoundManager.Instance.GetVolume() * 10);
        _musicText.text = "Music: " + Mathf.Ceil(MusicManager.Instance.GetVolume() * 10);
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public   void Hide()
    {
        gameObject.SetActive(false);
    }
}
