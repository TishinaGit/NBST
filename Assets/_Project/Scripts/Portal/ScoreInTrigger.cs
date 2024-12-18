 using UnityEngine;

public class ScoreInTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _portal;
    private int _score;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        { 
            Score();
        }
    }

    private void Score()
    {
        _score++;
        Debug.Log(_score);
        if (_score >= 100)
        {
            _portal.SetActive(true);
        }
    }
}
