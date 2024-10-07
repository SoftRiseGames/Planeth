using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void GameStoreMenuActivator()
    {
       GameObject menuLists = GameObject.Find("menuScreens");
        foreach (GameObject menuListObjects in menuLists.GetComponent<StoreLister>().StoreMenus)
            menuListObjects.SetActive(false);

        menuLists.GetComponent<StoreLister>().StoreMenus[int.Parse(gameObject.name)].SetActive(true);
        Debug.Log(int.Parse(gameObject.name));
    }
}
