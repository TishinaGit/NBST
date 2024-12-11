using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public AudioClip[] _audioClips;
    private AudioSource _audioSource;

    public Toggle toggleMusic;
    public Slider sliderVolumeMusic;
    public float volume;

    public void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlayMusic());
    }

    IEnumerator PlayMusic()
    {
        for (int i = 0; i < _audioClips.Length; i++)
        {
            _audioSource.PlayOneShot(_audioClips[i]);
            while (_audioSource.isPlaying)
                yield return null;
        }
    }

    void Start()
    {
        Load();
        ValueMusic();
        StartCoroutine(Foo());
        StartCoroutine(Bar());
    }
    IEnumerator Foo()
    {
        yield return null;
    }
    IEnumerator Bar()
    {
        yield return null;
    }
    public void BTM_SliderMusic()
    {
        volume = sliderVolumeMusic.value;
        Save();
        ValueMusic();
    }

    public void BTM_ToggleMusic()
    {
        if (toggleMusic.isOn == true)
        {
            volume = 0.1f;
        }
        else
        {
            volume = 0;
        }
        Save();
        ValueMusic();
    }

    private void ValueMusic()
    {
        _audioSource.volume = volume;
        sliderVolumeMusic.value = volume;
        if (volume == 0) { toggleMusic.isOn = false; } else { toggleMusic.isOn = true; }
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volume", volume);
    }
    private void Load()
    {
        volume = PlayerPrefs.GetFloat("volume", volume);
    }

}
