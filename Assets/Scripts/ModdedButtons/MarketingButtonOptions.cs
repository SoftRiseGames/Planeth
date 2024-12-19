using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MarketingButtonOptions : MonoBehaviour
{
    public So_Clothe_Settings ButtonSettingObject;
    [SerializeField] EquippedItem ItemHolder;
    [SerializeField] TextMeshProUGUI ObjectName;
    [SerializeField] Image background;
    [SerializeField] Image Skin;
    [SerializeField] SO_ValueMaker GameTotalCoin;
    public bool isTaken;

    private void Awake()
    {
        //ScriptableObjectDataManager.Instance.LoadData(gameObject);
    }
    private void Start()
    {
       
        ifTake();
        // Arka plan ve metinleri oluþtur
        ReCreate();
    }
    private void Update()
    {
       
    }
    void ifTake()
    {
        if (ButtonSettingObject.isTaken == true)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        
    }
    public void ReCreate()
    {
        background.sprite = ButtonSettingObject.Background;
        Skin.sprite = ButtonSettingObject.Skin;
        ObjectName.text = ButtonSettingObject.ObjectName;
    }

    public void TakeItem()
    {
        
        int CheckerInt = 0;
        foreach(int i in GameTotalCoin.Amount)
        {
            if (GameTotalCoin.Amount[i] >= ButtonSettingObject.MaterialRequirement[i])
            {
                CheckerInt = CheckerInt + 1;
            }
            else
                return;

        }
        if (GameTotalCoin.Amount.Count == CheckerInt)
        {
            this.ButtonSettingObject.isTaken = true;
            foreach (int i in GameTotalCoin.Amount)
            {
                GameTotalCoin.Amount[i] -= ButtonSettingObject.MaterialRequirement[i];
            }
            GetComponent<Button>().interactable = false;
            ItemHolder.EquippedData.Add(this.ButtonSettingObject);
            ScriptableObjectDataManager.Instance.SaveData(ButtonSettingObject);
         
        }
    }

    
}
