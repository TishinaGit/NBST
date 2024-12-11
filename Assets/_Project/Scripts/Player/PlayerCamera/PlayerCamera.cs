using UnityEngine;
using Cinemachine;

namespace Controller
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _freeLookVCam;
        [SerializeField] private InputReader _input;
        [SerializeField] public Transform _mainCam;

        private void Awake()
        {
            CameraGame();
        }

        private void CameraGame()
        {
            _freeLookVCam.Follow = transform;
            _freeLookVCam.LookAt = transform;
            _freeLookVCam.OnTargetObjectWarped(transform,
                transform.position - _freeLookVCam.transform.position - Vector3.forward);
            _mainCam = _freeLookVCam.transform;
            _input.EnablePlayerActions();
        }
    }
}

