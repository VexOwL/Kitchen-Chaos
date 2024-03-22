using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private SO_AudioClipsRefs _audioClipsRefs;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_Success;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_Failed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_PickedSomething;
        Counter.OnAnyPlacedObject += Counter_OnAnyPlacedObject;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnObjectTrashed;
    }
    
    private void PlaySound(AudioClip sound, Vector3 position, float volume = 0.5f)
    {
        AudioSource.PlayClipAtPoint(sound, position, volume);
    }

    private void PlaySound(AudioClip[] soundArray, Vector3 position, float volume = 0.5f)
    {

        PlaySound(soundArray[Random.Range(0, soundArray.Length)], position, volume);
    }

    public void PlayFootstepSound(Vector3 position, float volume = 0.5f)
    {

        PlaySound(_audioClipsRefs.Footstep, position, volume);
    }

    private void TrashCounter_OnObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(_audioClipsRefs.Trashed, trashCounter.transform.position);
    }

    private void Counter_OnAnyPlacedObject(object sender, EventArgs e)
    {
        Counter counter = sender as Counter;
        PlaySound(_audioClipsRefs.ObjectDrop, counter.transform.position);
    }

    private void Player_PickedSomething(object sender, EventArgs e)
    {
        PlaySound(_audioClipsRefs.ObjectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(_audioClipsRefs.Chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_Success(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioClipsRefs.DeliverySuccess, deliveryCounter.transform.position);
    }

    private void DeliveryManager_Failed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioClipsRefs.DeliveryFailed, deliveryCounter.transform.position);
    }

}
