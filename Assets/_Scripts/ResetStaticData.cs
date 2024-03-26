using UnityEngine;

public class ResetStaticData : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        Counter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}
