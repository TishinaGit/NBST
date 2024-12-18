using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class OpenTheDoor : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _sourceOpen;
    [SerializeField] private AudioSource _sourceClose;

    protected static readonly int openDoorHash = Animator.StringToHash("OpenDoorAnim");
    protected static readonly int closeDoorHash = Animator.StringToHash("CloseDoorAnim");

    private void OnTriggerEnter(Collider other)
    {
        _sourceOpen.Play();
    }
    private void OnTriggerStay(Collider other)
    { 
        _animator.Play(openDoorHash); 
    }

    private void OnTriggerExit(Collider other)
    {
        _animator.Play(closeDoorHash);
        _sourceClose.Play();
    }
        
}
