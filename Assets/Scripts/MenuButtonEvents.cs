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
    public void StoreMenu()
    {
        SceneManager.LoadSceneAsync(3,LoadSceneMode.Additive);
    }

    public void EquippedMenu()
    {
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
    }
    public void MainGame()
    {
        SceneManager.LoadScene(0);
    }   

    public void CharacterCustomizationHelmet()
    {
        int selectedButton = 0;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadEquippedItems());
    }
    public void CharacterCustomizationArmor()
    {
        int selectedButton = 1;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadEquippedItems());
    }
    public void CharacterCustomizationShoes()
    {
        int selectedButton = 2;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadEquippedItems());
    }
    public void CharacterCustomizationSword()
    {
        int selectedButton = 3;
        PlayerPrefs.SetInt("SelectedButton", selectedButton); 
        StartCoroutine(LoadEquippedItems());
    }
    public void CharacterCustomizationGlove()
    {
        int selectedButton = 4;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadEquippedItems());
        
    }

    public void MapPanel()
    {
        SceneManager.LoadScene(2);
    }

    public void FirstVillageMenu()
    {
        SceneManager.LoadScene(5);
    }

    private IEnumerator LoadEquippedItems()
    {
        var progress = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        while (!progress.isDone)
            yield return null;
    }
    

}
