using UnityEngine;
using Cinemachine;
using System;
using System.Collections;

namespace Controller
{
    public class CameraManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputReader _input;
        [SerializeField] private CinemachineFreeLook _freeLookVCam;

        [Header("Settings")]
        [SerializeField, Range(0.5f, 3f)] private float _speedMultiplier = 1.0f;

        private bool isRMBPressed;
        private bool isCameraMovementLock;

        private void OnEnable()
        {
            _input.Look += OnLock;
            _input.EnableMouseControlCamera += OnEnableMouseControlCamera;
            _input.DisableMouseControlCamera += OnDisableMouseControlCamera;
        }
        
        private void OnDisable()
        {
            _input.Look -= OnLock;
            _input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
            _input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
        }

        private void OnLock(Vector2 cameraMovement, bool isDeviceMouse)
        {
            if (isCameraMovementLock) return;

            if (isDeviceMouse && !isRMBPressed) return;

            float deviceMultiplier = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;

            _freeLookVCam.m_XAxis.m_InputAxisValue = cameraMovement.x * _speedMultiplier * deviceMultiplier;
            _freeLookVCam.m_YAxis.m_InputAxisValue = cameraMovement.y * _speedMultiplier * deviceMultiplier;
        }
        private void OnEnableMouseControlCamera()
        {
            isRMBPressed = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartCoroutine(DisableMouseForFrame());
        }


        private void OnDisableMouseControlCamera()
        {
            isRMBPressed = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _freeLookVCam.m_XAxis.m_InputAxisValue = 0f;
            _freeLookVCam.m_YAxis.m_InputAxisValue = 0f;
        }

        private IEnumerator DisableMouseForFrame()
        {
            isCameraMovementLock = true;
            yield return new WaitForEndOfFrame();
            isCameraMovementLock = false;
        }
    }
}

