using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipButton : MonoBehaviour
{
    public So_Clothe_Settings clothes;
    public EquippedItem equipList;
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
        clothes.isWear = true;
        ScriptableObjectDataManager.Instance.SaveWearData(clothes);
    }
}
