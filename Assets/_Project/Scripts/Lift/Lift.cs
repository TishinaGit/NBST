using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private Transform _targetLift;
    [SerializeField] private AudioSource _sourceLift;
    private float speed = 0.5f;
   
    private Vector3 _floor1;
    private Vector3 _floor2;

    public bool _isMoveLift1 = false;
    public bool _isMoveLift2 = false;

    private void Awake()
    {
        _floor1.y = 0f;
        _floor2.y = 1.05f;
    }

   
    private void Update()
    {
        BTM_MoveLiftFloor1();
        BTM_MoveLiftFloor2();
    }

    #region BTM
    public void BTM_One()
    {
        _sourceLift.Play();

        _isMoveLift1 = true;

        _isMoveLift2 = false;
    }

    public void BTM_Two()
    {
        _sourceLift.Play();

        _isMoveLift2 = true;

        _isMoveLift1 = false;
    }
#endregion

    #region MoveLift
    private void BTM_MoveLiftFloor1()
    {
        
        if (_isMoveLift1 == true)
        {
            _targetLift.transform.localPosition = Vector3.MoveTowards(_targetLift.transform.localPosition, _floor1, speed * Time.deltaTime);
        }
    }

    private void BTM_MoveLiftFloor2()
    {
        
        if (_isMoveLift2 == true)
        {
            _targetLift.transform.localPosition = Vector3.MoveTowards(_targetLift.transform.localPosition, _floor2, speed * Time.deltaTime);
        }
    }
    #endregion
}