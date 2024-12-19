using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameLobbyScene : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other != null)
            {
                SceneManager.LoadScene(2);
            }
        }

    }

}
