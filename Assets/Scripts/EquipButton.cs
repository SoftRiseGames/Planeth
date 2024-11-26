using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour
{
    public So_Clothe_Settings clothes;
    public EquippedItem equipList;

    private void Awake()
    {
        if (clothes.isWear == true)
            gameObject.GetComponent<Button>().interactable = false;
        else
            gameObject.GetComponent<Button>().interactable = true;
        //ScriptableObjectDataManager.Instance.LoadData(gameObject);
    }
    private void Start()
    {

       
    }

    private void Update()
    {
        RefreshButton();
    }
    public void isEquip()
    {
        Debug.Log("clicked");
       
        foreach (So_Clothe_Settings equippedItem in equipList.EquippedData)
        {
            if(equippedItem.WearableType == clothes.WearableType)
                equippedItem.isWear = false;

            ScriptableObjectDataManager.Instance.UpdateSavedData(equippedItem);
        }
            
            
        clothes.isWear = true;
        gameObject.GetComponent<Button>().interactable = false;
        ScriptableObjectDataManager.Instance.UpdateSavedData(clothes);

    }

    void RefreshButton()
    {
        if (clothes.isWear == false)
            gameObject.GetComponent<Button>().interactable = true;
    }
}