using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScriptableObjectDataManager : MonoBehaviour
{
    public static ScriptableObjectDataManager Instance { get; private set; }

    private string savePath;

    private void Awake()
    {
        // Singleton kontrolü
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Oyun sahneleri arasýnda nesneyi koru
        }
        else
        {
            Destroy(gameObject); // Zaten var olan bir instance varsa bu nesneyi yok et
        }

        // Persistent data yolunu ayarla
        savePath = Application.persistentDataPath + "/buttonData.json";
    }

    // ScriptableObject verilerini JSON dosyasýna kaydet
    public void SaveData(So_Clothe_Settings buttonSettingObject, SO_ValueMaker gameTotalCoin, EquippedItem itemHolder)
    {
        ButtonData data = new ButtonData
        {
            isTaken = buttonSettingObject.isTaken,
            totalCoins = gameTotalCoin.Amount,
            equippedItems = new List<So_Clothe_Settings>(itemHolder.EquippedData) // Clone the list to avoid reference issues
        };

        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, jsonData);
    }

    // ScriptableObject verilerini JSON dosyasýndan yükle
    public void LoadData(So_Clothe_Settings buttonSettingObject, SO_ValueMaker gameTotalCoin, EquippedItem itemHolder)
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            ButtonData data = JsonUtility.FromJson<ButtonData>(jsonData);

            // Veriyi geri yükle
            buttonSettingObject.isTaken = data.isTaken;
            gameTotalCoin.Amount = data.totalCoins;
            itemHolder.EquippedData = new List<So_Clothe_Settings>(data.equippedItems); // Clone the list to avoid reference issues
        }
        else
        {
            Debug.Log("Save file not found, loading defaults.");
        }
    }

    public void StartLoadData(SO_ValueMaker gameTotalCoin, EquippedItem itemHolder)
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            ButtonData data = JsonUtility.FromJson<ButtonData>(jsonData);

            gameTotalCoin.Amount = data.totalCoins;
            itemHolder.EquippedData = new List<So_Clothe_Settings>(data.equippedItems); // Clone the list to avoid reference issues
        }
        else
        {
            Debug.Log("Save file not found, loading defaults.");
        }
    }

    // Tüm verileri sýfýrlama
    public void RestartData(So_Clothe_Settings buttonSettingObject, SO_ValueMaker gameTotalCoin, EquippedItem itemHolder)
    {
        buttonSettingObject.isTaken = false;
        gameTotalCoin.Amount = 100; // Ýstediðiniz baþlangýç deðeri
        itemHolder.EquippedData.Clear();

        // Sýfýrlandýktan sonra JSON dosyasýný güncelle
        SaveData(buttonSettingObject, gameTotalCoin, itemHolder);
    }

    // ScriptableObject verileri için kullanýlan veri sýnýfý
    [System.Serializable]
    public class ButtonData
    {
        public bool isTaken;
        public int totalCoins;
        public List<So_Clothe_Settings> equippedItems;
    }
}
