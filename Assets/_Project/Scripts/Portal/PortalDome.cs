 using UnityEngine;

public class PortalDome : MonoBehaviour
{
    public GameObject _dome;
    private bool _isActive = false;
     
    private void Update()
    {
        ActionDome(); 
        if (_isActive == true)
        {
            _dome.transform.localScale = Vector3.Lerp(_dome.transform.localScale, new Vector3(7, 7, 7),   Time.deltaTime);
        }
        
    }

    private void ActionDome()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            _isActive = true; 
        }
    }
}
