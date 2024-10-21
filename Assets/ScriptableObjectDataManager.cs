using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScriptableObjectDataManager : MonoBehaviour
{
    private string savePath;

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/scriptableObjectData.json";
    }
    
    public void SaveData(So_Clothe_Settings buttonSettings, SO_ValueMaker gameTotalCoin, EquippedItem itemHolder)
    {
        ScriptableObjectData data = new ScriptableObjectData
        {
            isTaken = buttonSettings.isTaken,
            totalCoins = gameTotalCoin.Amount,
            equippedItems = itemHolder.EquippedData 
        };

        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, jsonData);
    }

    
    public void LoadData(So_Clothe_Settings buttonSettings, SO_ValueMaker gameTotalCoin, EquippedItem itemHolder)
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            ScriptableObjectData data = JsonUtility.FromJson<ScriptableObjectData>(jsonData);

            
            buttonSettings.isTaken = data.isTaken;
            gameTotalCoin.Amount = data.totalCoins;
            itemHolder.EquippedData = data.equippedItems; 
        }
    }

    
    [System.Serializable]
    public class ScriptableObjectData
    {
        public bool isTaken;
        public int totalCoins;
        public List<So_Clothe_Settings> equippedItems; 
    }
}
