using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonEvents : MonoBehaviour
{
    //Ana oyun sahnesi 0 
    // store i�in 1
    // al�nanlar i�in 2
    public void StoreMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void MainGame()
    {
        SceneManager.LoadScene(0);
    }
    
}
