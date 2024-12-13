 using UnityEngine;
public class PlayerShooter : MonoBehaviour
{ 
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _rectTransform; 

    public void Shoot()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(_rectTransform.position);

        if (Physics.Raycast(ray, out hit, 1000) == true)
        {
            _weapon.FirePointLookAt(hit.point);
        }

        if (_weapon.CanFire == true)
        {
            _weapon.Fire(); 
        }
    }
}
