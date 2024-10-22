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
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }

        
        savePath = Application.persistentDataPath + "/buttonData.json";
    }

    
    public void SaveObjectTakeData(So_Clothe_Settings buttonSettingObject, SO_ValueMaker gameTotalCoin, EquippedItem itemHolder)
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

    // ScriptableObject verilerini JSON dosyas�ndan y�kle
    public void LoadData(So_Clothe_Settings buttonSettingObject, SO_ValueMaker gameTotalCoin, EquippedItem itemHolder)
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            ButtonData data = JsonUtility.FromJson<ButtonData>(jsonData);

            // Veriyi geri y�kle
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

            for (int i = 0; i < itemHolder.EquippedData.Count; i++)
                itemHolder.EquippedData[i].isTaken = data.isTaken;

            gameTotalCoin.Amount = data.totalCoins;
            itemHolder.EquippedData = new List<So_Clothe_Settings>(data.equippedItems); // Clone the list to avoid reference issues
        }
        else
        {
            Debug.Log("Save file not found, loading defaults.");
        }
    }
    public void SaveWearData(So_Clothe_Settings buttonSettingObject)
    {
        ButtonData data = new ButtonData
        {
            isWear = buttonSettingObject.isWear,
        };

        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, jsonData);
        
    }
    public void LoadWearData(So_Clothe_Settings buttonSettingObject)
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            ButtonData data = JsonUtility.FromJson<ButtonData>(jsonData);

            buttonSettingObject.isWear = data.isWear;
        }
        else
        {
            Debug.Log("Save file not found, loading defaults.");
        }
    }

    // T�m verileri s�f�rlama
    public void RestartData(So_Clothe_Settings buttonSettingObject, SO_ValueMaker gameTotalCoin, EquippedItem itemHolder)
    {
        buttonSettingObject.isTaken = false;
        gameTotalCoin.Amount = 100; // �stedi�iniz ba�lang�� de�eri
        itemHolder.EquippedData.Clear();

        // S�f�rland�ktan sonra JSON dosyas�n� g�ncelle
        SaveObjectTakeData(buttonSettingObject, gameTotalCoin, itemHolder);
    }

    // ScriptableObject verileri i�in kullan�lan veri s�n�f�
    [System.Serializable]
    public class ButtonData
    {
        public bool isTaken;
        public bool isWear;
        public int totalCoins;
        public List<So_Clothe_Settings> equippedItems;
    }
}
