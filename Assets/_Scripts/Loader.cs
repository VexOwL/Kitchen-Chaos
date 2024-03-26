using UnityEngine.SceneManagement;

public static class Loader
{
    private static Scene _targetScene;
    public enum Scene { MainMenu, GameScene, LoadingScreen }

    public static void Load(Scene targetScene)
    {
        _targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScreen.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(_targetScene.ToString());
    }
}