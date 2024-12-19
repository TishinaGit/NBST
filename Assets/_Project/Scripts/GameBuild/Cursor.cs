 using UnityEngine;

public class CursorLoced : MonoBehaviour
{ 
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    } 
}
