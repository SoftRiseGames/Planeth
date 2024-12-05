using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuButtonEvents : MonoBehaviour
{
    //Ana oyun sahnesi 0 
    // store için 1
    // alýnanlar için 2

    //0 for helmet
    //1 for armor
    //2 for shoes
    //3 for sword
    //4 for glove

    string ActivatedAsyncSceneByName;
    public void StoreMenu()
    {
        StartCoroutine(LoadAsyncScene(4,"Store"));
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("SceneName"))
            ActivatedAsyncSceneByName = PlayerPrefs.GetString("SceneName");
        else
            ActivatedAsyncSceneByName = null;
    }
    public void EquippedMenu()
    {
        StartCoroutine(LoadAsyncScene(5, "EquippedItem"));
    }
    public void MainGame()
    {
        SceneManager.LoadScene(3);
    }
    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void CharacterCustomizationScene()
    {
        SceneManager.LoadScene(0);
    }
    public void CharacterCustomizationHelmet()
    {
        int selectedButton = 0;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadAsyncScene(5, "EquippedItem"));
    }
    public void CharacterCustomizationArmor()
    {
        int selectedButton = 1;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadAsyncScene(5, "EquippedItem"));
    }
    public void CharacterCustomizationShoes()
    {
        int selectedButton = 2;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadAsyncScene(5, "EquippedItem"));
    }
    public void CharacterCustomizationSword()
    {
        int selectedButton = 3;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadAsyncScene(5, "EquippedItem"));
    }
    public void CharacterCustomizationGlove()
    {
        int selectedButton = 4;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadAsyncScene(5,"EquippedItem"));
        
    }
    private IEnumerator LoadAsyncScene(int SceneIndex, string SceneName)
    {
        PlayerPrefs.SetString("SceneName", SceneName);
        var progress = SceneManager.LoadSceneAsync(SceneIndex, LoadSceneMode.Additive);
        while (!progress.isDone)
        {
            yield return null;
        }

    }

    public void MapPanel()
    {
        SceneManager.LoadScene(1);
    }

  
    public void ClosePage()
    {
        SceneManager.UnloadSceneAsync(ActivatedAsyncSceneByName);
    }
    public void FirstVillageMenu()
    {
        
        SceneManager.LoadScene(2);
    }

   
    

}
