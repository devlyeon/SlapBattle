using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private SoundType _soundType;
    [SerializeField] private SoundName _soundName;

    public void PlaySound()
    {
        if (_soundType == SoundType.SFX)
        {
            AudioClip clip = SoundLibrary.Instance.AUDIOCLIP[(int)_soundName];
            SoundLibrary.Instance.SFX_AUDIO_SOURCE.PlayOneShot(clip);
        }
        else
        {
            AudioClip clip = SoundLibrary.Instance.AUDIOCLIP[(int)_soundName];
            SoundLibrary.Instance.BGM_AUDIO_SOURCE.PlayOneShot(clip);
        }
    }
}

public enum SoundType
{
    SFX = 0,
    BGM = 1,
}