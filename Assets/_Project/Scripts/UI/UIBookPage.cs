using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBookPage : MonoBehaviour
{
    [SerializeField] private GameObject _pageOne;
    [SerializeField] private GameObject _pageTwo;

    public void BTM_GOPageOne()
    {
        _pageOne.SetActive(true);
        _pageTwo.SetActive(false);
    }

    public void BTM_GOPageTwo()
    {
        _pageOne.SetActive(false);
        _pageTwo.SetActive(true);
    }
}
