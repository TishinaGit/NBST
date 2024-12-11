using UnityEngine;

public class SoundPlayUIButton : MonoBehaviour
{
    [SerializeField] private AudioSource _uiButton;

    public void BTM_PlaySound()
    {
        _uiButton.Play();
    }
}
