using Cinemachine;
using UnityEngine;

public class ActionsKeyE : MonoBehaviour
{
    [SerializeField] private GameObject _uiObject;
    [SerializeField] private GameObject _uiActionEText;
    [SerializeField] private CinemachineFreeLook _cinemachineFreeLook;


    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            _uiActionEText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                _cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0f;
                _cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;

                _uiObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            _cinemachineFreeLook.m_XAxis.m_MaxSpeed = 250f;
            _cinemachineFreeLook.m_YAxis.m_MaxSpeed = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _uiActionEText.SetActive(false);
            _uiObject.SetActive(false);
        }
    }
}
