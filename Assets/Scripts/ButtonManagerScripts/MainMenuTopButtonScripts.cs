using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTopButtonScripts : MonoBehaviour
{
    [SerializeField] string WhichMainMenu;
    public void GameStoreMenuActivator()
    {

       GameObject menuLists = GameObject.Find(WhichMainMenu);

        if(menuLists.tag == "Store")
        {
            foreach (GameObject menuListObjects in menuLists.GetComponent<StoreLister>().StoreMenus)
                menuListObjects.SetActive(false);

            menuLists.GetComponent<StoreLister>().StoreMenus[int.Parse(gameObject.name)].SetActive(true);
            Debug.Log(int.Parse(gameObject.name));
        }
        else if(menuLists.tag == "EquippedItem")
        {

        }
        
    }
}
