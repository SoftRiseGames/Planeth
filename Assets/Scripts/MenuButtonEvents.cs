using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonEvents : MonoBehaviour
{
    //Ana oyun sahnesi 0 
    // store için 1
    // alýnanlar için 2
    public void StoreMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void MainGame()
    {
        SceneManager.LoadScene(0);
    }
    
}
