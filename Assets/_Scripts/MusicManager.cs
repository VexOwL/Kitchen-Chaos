using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _volume = 0.2f;
    public static MusicManager Instance;
    private const string PLAYERPREFS_MUSIC_VOLUME = "MusicVolume";
    
    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();

        _volume = PlayerPrefs.GetFloat(PLAYERPREFS_MUSIC_VOLUME);
    }
    
    public void ChangeVolume()
    {
        _volume += 0.1f;

        if(_volume > 1)
            _volume = 0;

        _audioSource.volume = _volume;

        PlayerPrefs.SetFloat(PLAYERPREFS_MUSIC_VOLUME, _volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return _volume;
    }
}
