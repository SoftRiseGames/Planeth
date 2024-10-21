using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
public class MarketingButtonOptions : MonoBehaviour
{

    public So_Clothe_Settings ButtonSettingObject;
    [SerializeField] EquippedItem ItemHolder;
    [SerializeField] TextMeshProUGUI ObjectName;
    [SerializeField] Image background;
    [SerializeField] Image Skin;
    [SerializeField] SO_ValueMaker GameTotalCoin;

    private void Start()
    {
        if (!ButtonSettingObject.isTaken)
            GetComponent<Button>().interactable = true;
        else
            GetComponent<Button>().interactable = false;
    }

    public void ReCreate()
    {
        background.sprite = ButtonSettingObject.Background;
        Skin.sprite = ButtonSettingObject.Skin;
        ObjectName.text = ButtonSettingObject.ObjectName;
    }
    public void TakeItem()
    {
        if (GameTotalCoin.Amount >= ButtonSettingObject.price)
        {
            ButtonSettingObject.isTaken = true;
            GameTotalCoin.Amount = GameTotalCoin.Amount - ButtonSettingObject.price;
            GetComponent<Button>().interactable = false;
            ItemHolder.EquippedData.Add(ButtonSettingObject);
            EditorUtility.SetDirty(GameTotalCoin);
            EditorUtility.SetDirty(ButtonSettingObject);
            EditorUtility.SetDirty(ItemHolder);
        }

    }

}