using UnityEngine;

public class DestroyPortal : MonoBehaviour
{
    [SerializeField] private GameObject _portal;
    private float _time = 12;
    private void Update()
    {
        TimeDestroy();
    }

    public void TimeDestroy()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
        }
        else if (_time <= 0)
        {
            Destroy(_portal);
        }
    }
}
