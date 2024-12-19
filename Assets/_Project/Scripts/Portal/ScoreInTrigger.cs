 using UnityEngine;

public class ScoreInTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _portal;
    private float _score;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        { 
            Score();
        }
    }

    private void Score()
    {
        _score += 1 * Time.deltaTime; 
        if (_score >= 100)
        {
            _portal.SetActive(true);
        }
    }
}
