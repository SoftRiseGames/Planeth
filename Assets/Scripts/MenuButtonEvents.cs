using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
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
    public static Action isTransition;
    public static Action isStorePopUp;
    public static Action isEquippedPopUp;
    public static Action isRawMaterialPopUp;

    public static Action isStorePopUpOff;
    public static Action isEquippedPopUpOff;
    public static Action isRawMaterialPopUpOff;
    public void StoreMenu()
    {
        //StartCoroutine(LoadAsyncScene("Store"));
        isStorePopUp?.Invoke();

    }

    public void StoreMenuOff()
    {
        isStorePopUpOff?.Invoke();
    }

    public void RawMaterialMenu()
    {
        isRawMaterialPopUp?.Invoke();
    }
    public void RawMaterialMenuOff()
    {
        isRawMaterialPopUpOff?.Invoke();
    }
    public void EquippedManuOff()
    {
        isEquippedPopUpOff?.Invoke();
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
        StartCoroutine(LoadAsyncScene("EquippedItem"));
        
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
        isEquippedPopUp?.Invoke();
    }
    public void CharacterCustomizationHelmet()
    {
        int selectedButton = 0;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadAsyncScene("EquippedItem"));
    }
    public void CharacterCustomizationArmor()
    {
        int selectedButton = 1;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadAsyncScene("EquippedItem"));
    }
    public void CharacterCustomizationShoes()
    {
        int selectedButton = 2;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadAsyncScene("EquippedItem"));
    }
    public void CharacterCustomizationSword()
    {
        int selectedButton = 3;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadAsyncScene("EquippedItem"));
    }
    public void CharacterCustomizationGlove()
    {
        int selectedButton = 4;
        PlayerPrefs.SetInt("SelectedButton", selectedButton);
        StartCoroutine(LoadAsyncScene("EquippedItem"));
        
    }
    private IEnumerator LoadAsyncScene(string SceneName)
    {
        PlayerPrefs.SetString("SceneName", SceneName);
        var progress = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
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
    public void TransitionEvent()
    {
        isTransition?.Invoke();
    }

   
    

}
