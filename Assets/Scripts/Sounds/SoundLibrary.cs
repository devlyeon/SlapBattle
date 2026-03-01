using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;
    public AudioClip[] AUDIOCLIP => _audioClips;

    [SerializeField] private AudioSource _bgmAudioSource;
    public AudioSource BGM_AUDIO_SOURCE => _bgmAudioSource;

    [SerializeField] private AudioSource _sfxAudioSource;
    public AudioSource SFX_AUDIO_SOURCE => _sfxAudioSource;

    public static SoundLibrary Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
