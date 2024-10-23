using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipButton : MonoBehaviour
{
    public So_Clothe_Settings clothes;
    public EquippedItem equipList;

    private void Awake()
    {
        //ScriptableObjectDataManager.Instance.LoadData(gameObject);
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
