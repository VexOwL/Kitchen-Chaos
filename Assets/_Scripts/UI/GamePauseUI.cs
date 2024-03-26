using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _optionsButton;

    private void Awake()
    {
        _mainMenuButton.onClick.AddListener(() => {Loader.Load(Loader.Scene.MainMenu);});
        _optionsButton.onClick.AddListener(() => {OptionsUI.Instance.Show();});
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

