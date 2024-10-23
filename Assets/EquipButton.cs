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
        //ScriptableObjectDataManager.Instance.LoadData(gameObject);
    }
    private void Start()
    {
        if (clothes.isWear == true)
            gameObject.GetComponent<Button>().interactable = false;
        else
            gameObject.GetComponent<Button>().interactable = true;
    }
    public void isEquip()
    {
        Debug.Log("clicked");
        /*
        foreach(So_Clothe_Settings takenitems in equipList.EquippedData)
        {
            takenitems.isWear = false;
            ScriptableObjectDataManager.Instance.SaveWearData(takenitems);
        }
        */
        foreach (So_Clothe_Settings equippedItem in equipList.EquippedData)
        {
            equippedItem.isWear = false;
            ScriptableObjectDataManager.Instance.UpdateSavedData(equippedItem);
        }
            
            
        clothes.isWear = true;
        ScriptableObjectDataManager.Instance.UpdateSavedData(clothes);

    }
}
