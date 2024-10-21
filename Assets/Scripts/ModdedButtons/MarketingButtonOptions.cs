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
        dataManager = ScriptableObjectDataManager.Instance; // Singleton �zerinden eri�im
        // Butonun interaktif olup olmad���n� kontrol et
        GetComponent<Button>().interactable = !ButtonSettingObject.isTaken;

        // Arka plan ve metinleri olu�tur
        ReCreate();
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
            // �r�n al�nd�
            ButtonSettingObject.isTaken = true;
            GameTotalCoin.Amount -= ButtonSettingObject.price;
            GetComponent<Button>().interactable = false;
            ItemHolder.EquippedData.Add(ButtonSettingObject);

            // Veriyi kaydet
            dataManager.SaveData(ButtonSettingObject, GameTotalCoin, ItemHolder);
        }
    }

    private void Update()
    {
        // E�er R tu�una bas�l�rsa t�m veriyi s�f�rla
        if (Input.GetKeyDown(KeyCode.R))
        {
            dataManager.RestartData(ButtonSettingObject, GameTotalCoin, ItemHolder);
        }
    }
}
