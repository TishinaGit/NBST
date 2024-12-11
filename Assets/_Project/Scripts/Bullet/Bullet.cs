using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _bullet;
    private float _bulletSpeed = 200f;
    private void Awake()
    {
        _bullet = GetComponent<Rigidbody>();
    }
    public void Start()
    {
        if ( _bullet != null )
        {
            _bullet.velocity = transform.forward * _bulletSpeed;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if( other.gameObject  != null )
        {
            Destroy(gameObject);
        }
        
    }
}
