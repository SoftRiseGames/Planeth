using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class MarketingButtonOptions : MonoBehaviour
{
    public So_Clothe_Settings ButtonSettingObject;
    [SerializeField] EquippedItem ItemHolder;
    [SerializeField] TextMeshProUGUI ObjectName;
    [SerializeField] Image background;
    [SerializeField] Image Skin;
    [SerializeField] SO_ValueMaker GameTotalCoin;

    private string savePath;

    private void Start()
    {
        
        savePath = Application.persistentDataPath + "/buttonData.json";

        
        LoadData();

        
        if (!ButtonSettingObject.isTaken)
            GetComponent<Button>().interactable = true;
        else
            GetComponent<Button>().interactable = false;

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
            
            ButtonSettingObject.isTaken = true;
            GameTotalCoin.Amount = GameTotalCoin.Amount - ButtonSettingObject.price;
            GetComponent<Button>().interactable = false;
            ItemHolder.EquippedData.Add(ButtonSettingObject);

            
            SaveData();
        }
    }

    
    private void SaveData()
    {
        ButtonData data = new ButtonData
        {
            isTaken = ButtonSettingObject.isTaken,
            totalCoins = GameTotalCoin.Amount,
            equippedItems = ItemHolder.EquippedData 
        };

        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, jsonData);
    }

    private void LoadData()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            ButtonData data = JsonUtility.FromJson<ButtonData>(jsonData);

            
            ButtonSettingObject.isTaken = data.isTaken;
            GameTotalCoin.Amount = data.totalCoins;
            ItemHolder.EquippedData = data.equippedItems; 
        }
    }

    
    [System.Serializable]
    public class ButtonData
    {
        public bool isTaken;
        public int totalCoins;
        public List<So_Clothe_Settings> equippedItems; 
    }
}
