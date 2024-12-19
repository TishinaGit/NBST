using UnityEngine;

public class SoundPlayUIButton : MonoBehaviour
{
    [SerializeField] private AudioSource _uiButtonSound;

    public void BTM_PlaySound()
    {
        _uiButtonSound.Play();
    }
}
