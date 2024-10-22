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

    private ScriptableObjectDataManager dataManager;

    private void Start()
    {
        dataManager = ScriptableObjectDataManager.Instance; // Singleton üzerinden eriþim
        ifTake();
        // Arka plan ve metinleri oluþtur
        ReCreate();
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
            // Ürün alýndý
            ButtonSettingObject.isTaken = true;
            GameTotalCoin.Amount -= ButtonSettingObject.price;
            GetComponent<Button>().interactable = false;
            ItemHolder.EquippedData.Add(ButtonSettingObject);
            // Veriyi kaydet
            dataManager.SaveObjectTakeData(ButtonSettingObject, GameTotalCoin, ItemHolder);
        }
    }

    private void Update()
    {
        // Eðer R tuþuna basýlýrsa tüm veriyi sýfýrla
        if (Input.GetKeyDown(KeyCode.R))
        {
            dataManager.RestartData(ButtonSettingObject, GameTotalCoin, ItemHolder);
        }
    }
}
