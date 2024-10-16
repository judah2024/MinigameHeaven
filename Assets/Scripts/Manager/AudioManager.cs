using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // public AudioClip bgmClip;    
    AudioSource _audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        //audioSource.clip = bgmClip;
        RestartBGM();
    }

    public void HurryUp()
    {
        _audioSource.pitch = 1.1f;
    }

    public void RestartBGM()
    {
        _audioSource.pitch = 1;
        _audioSource.Stop();
        _audioSource.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
