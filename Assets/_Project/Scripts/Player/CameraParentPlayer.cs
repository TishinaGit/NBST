using Cinemachine; 
using Controller; 
using UnityEngine;
using Zenject;

public class CameraParentPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _cameraSystem;

    private CinemachineFreeLook _cinemachineFreeLook;
    private PlayerController _controller;
   
    [Inject]
    public void Construct( CinemachineFreeLook cinemachineFreeLook, PlayerController controller)
    {
        _cinemachineFreeLook = cinemachineFreeLook;
        _controller = controller;
          
    }

    private void Start()
    {  
        CamerasSettings();
    }

    private void CamerasSettings()
    { 
        _cinemachineFreeLook.Follow = _controller.gameObject.transform;
        _cinemachineFreeLook.LookAt = _controller.gameObject.transform; 
        _cameraSystem.transform.parent = _controller.transform;
    }  
} 

