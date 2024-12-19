using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLoadGameScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other != null)
            {
                SceneManager.LoadScene(3);
            }
        }
       
    }
}
