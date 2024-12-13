using Controller;
using UnityEngine;
public class PlayerShooter : MonoBehaviour
{ 
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private PlayerActionsInput _playerActionsInput;

    private void Start()
    {
        _playerActionsInput = GetComponent<PlayerActionsInput>();
    }

    public void Shoot()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(_rectTransform.position);
         
        if (Physics.Raycast(ray, out hit, 1000) == true)
        {
            _weapon.FirePointLookAt(hit.point);
        }
        if (_playerActionsInput.AttackPressed == true)
        {
            _weapon.Fire();
        }
    }
}
