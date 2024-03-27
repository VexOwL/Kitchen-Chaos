using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Mode _mode;
    private enum Mode { Inverted, Normal }

    private void LateUpdate()
    {
        switch (_mode)
        {
            case Mode.Inverted:
                Vector3 directionFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + directionFromCamera);
                break;

            case Mode.Normal:
                transform.LookAt(Camera.main.transform.position);
                break;
        }
    }
}
