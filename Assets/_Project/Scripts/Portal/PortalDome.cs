using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalDome : MonoBehaviour
{
    [SerializeField] private GameObject _dome;
    private bool _isActive = false;
    private void Update()
    {
        Kostil(); 
        if (_isActive == true)
        {
            _dome.transform.localScale = Vector3.Lerp(_dome.transform.localScale, new Vector3(8, 8, 8),   Time.deltaTime);
        }
        
    }

    private void Kostil()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            _isActive = true;
        }
    }
}
