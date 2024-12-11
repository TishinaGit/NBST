using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadScene : MonoBehaviour
{
    public void BTM_LoadMainMenuScene()
    {
        SceneManager.LoadScene(1);
    }

    public void BTM_LoadLobbyGameScene()
    {
        SceneManager.LoadScene(2);
    }

    public void BTM_LoadGameScene()
    {
        SceneManager.LoadScene(3);
    }
   
}
