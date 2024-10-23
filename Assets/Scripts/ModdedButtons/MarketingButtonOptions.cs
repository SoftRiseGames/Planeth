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
            Debug.Log("alýndý");
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
        if (GameTotalCoin.Amount >= ButtonSettingObject.price)
        {
            
            this.ButtonSettingObject.isTaken = true;
            GameTotalCoin.Amount -= ButtonSettingObject.price;
            GetComponent<Button>().interactable = false;
            ItemHolder.EquippedData.Add(this.ButtonSettingObject);
            ScriptableObjectDataManager.Instance.SaveDatas(ButtonSettingObject);
         
        }
    }

    
}
