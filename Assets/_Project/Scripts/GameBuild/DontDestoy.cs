using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoy : MonoBehaviour
{
    [SerializeField] private GameObject _prefabDontDestoy;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
