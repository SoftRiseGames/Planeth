using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScriptableObjectDataManager : MonoBehaviour
{
    private string savePath;

    private void Awake()
    {
        // Persistent data yolunu ayarla
        savePath = Application.persistentDataPath + "/scriptableObjectData.json";
    }

    // ScriptableObject verilerini JSON dosyasýna kaydet
    public void SaveData(So_Clothe_Settings buttonSettings, SO_ValueMaker gameTotalCoin, EquippedItem itemHolder)
    {
        ScriptableObjectData data = new ScriptableObjectData
        {
            isTaken = buttonSettings.isTaken,
            totalCoins = gameTotalCoin.Amount,
            equippedItems = itemHolder.EquippedData // Adjust if necessary to a serializable format
        };

        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, jsonData);
    }

    // ScriptableObject verilerini JSON dosyasýndan yükle
    public void LoadData(So_Clothe_Settings buttonSettings, SO_ValueMaker gameTotalCoin, EquippedItem itemHolder)
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            ScriptableObjectData data = JsonUtility.FromJson<ScriptableObjectData>(jsonData);

            // Veriyi geri yükle
            buttonSettings.isTaken = data.isTaken;
            gameTotalCoin.Amount = data.totalCoins;
            itemHolder.EquippedData = data.equippedItems; // Adjust if necessary
        }
        else
        {
            // Eðer veri yoksa default deðerlerle devam edilir
            Debug.Log("Save file not found, loading defaults.");
        }
    }

    // JSON dosyasýný sil ve verileri sýfýrla
  
    // ScriptableObject verileri için kullanýlan veri sýnýfý
    [System.Serializable]
    public class ScriptableObjectData
    {
        public bool isTaken;
        public int totalCoins;
        public List<So_Clothe_Settings> equippedItems; // Assuming this is serializable
    }

   
}
