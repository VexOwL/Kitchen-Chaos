using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    public bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;

            Loader.LoaderCallback();
        }
    }
}
