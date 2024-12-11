using UnityEngine;

public class RotateHeart : MonoBehaviour
{
    [SerializeField] private GameObject _heartRotate;
    public Vector3 xAngle;

    private void Update()
    {
        _heartRotate.transform.Rotate(xAngle, Space.Self);
    }
    private void Awake()
    {
        
    }
}