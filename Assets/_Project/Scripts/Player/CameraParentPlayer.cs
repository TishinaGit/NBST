using Cinemachine; 
using Controller; 
using UnityEngine;
using Zenject;

public class CameraParentPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _cameraSystem;
    public PlayerController Controller;
    public CinemachineFreeLook CinemachineFreeLook; 
     
    [Inject]
    public void Consruct(PlayerController controller, CinemachineFreeLook cinemachineFreeLook)
    {
        Controller = controller;
        CinemachineFreeLook = cinemachineFreeLook;  
    }

    private void Start()
    { 
        CamerasSettings();
    }

    private void CamerasSettings()
    { 
        CinemachineFreeLook.Follow = Controller.gameObject.transform;
        CinemachineFreeLook.LookAt = Controller.gameObject.transform; 
        _cameraSystem.transform.parent = Controller.transform;
    }  
} 

