using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTopButtonScripts : MonoBehaviour
{
    [SerializeField] GameObject WhichMainMenu;
    public void GameStoreMenuActivator()
    {

        if(WhichMainMenu.tag == "Store")
        {
            foreach (GameObject menuListObjects in WhichMainMenu.GetComponent<StoreLister>().StoreMenus)
                menuListObjects.SetActive(false);

            WhichMainMenu.GetComponent<StoreLister>().StoreMenus[int.Parse(gameObject.name)].SetActive(true);
            Debug.Log(int.Parse(gameObject.name));
        }
        else if(WhichMainMenu.tag == "EquippedItem")
        {
            foreach (GameObject menuListObjects in WhichMainMenu.GetComponent<EquippedItemHolderScript>().EquippedItemMenu)
                menuListObjects.SetActive(false);

            WhichMainMenu.GetComponent<EquippedItemHolderScript>().EquippedItemMenu[int.Parse(gameObject.name)].SetActive(true);
        }
        
    }
}
