using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips; 
    [SerializeField] private Toggle _toggleMusic;
    [SerializeField] private Slider _sliderVolumeMusic;
    [SerializeField] private float _volume; 
    [SerializeField] private AudioSource _audioSource;
     
    public void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlayMusic());
    } 

    void Start()
    {
        Load();
        ValueMusic();
        StartCoroutine(Foo());
        StartCoroutine(Bar());
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
        _volume = _sliderVolumeMusic.value;
        Save();
        ValueMusic();
    }

    public void BTM_ToggleMusic()
    {
        if (_toggleMusic.isOn == true)
        {
            _volume = 0.1f;
        }
        else
        {
            _volume = 0;
        }
        Save();
        ValueMusic();
    }

    private void ValueMusic()
    {
        _audioSource.volume = _volume;
        _sliderVolumeMusic.value = _volume;
        if (_volume == 0) { _toggleMusic.isOn = false; } else { _toggleMusic.isOn = true; }
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volume", _volume);
    }
    private void Load()
    {
        _volume = PlayerPrefs.GetFloat("volume", _volume);
    }

}
