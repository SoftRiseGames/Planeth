using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScriptableObjectDataManager : MonoBehaviour
{
    public static ScriptableObjectDataManager Instance { get; private set; }
    private string savePath;
    private string CoinSavePath;
    private string EquippedItemDataPath;
    public SO_ValueMaker CoinValue;
    public EquippedItem ItemHolder;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        savePath = Application.dataPath + "/Datas.json";
        CoinSavePath = Application.dataPath + "/Amounts.json";
        EquippedItemDataPath = Application.dataPath + "/EquippedItemDataPath.json";

        //
    }

    ButtonDataList buttonDataList = new ButtonDataList();
    CurrencyData currencyData = new CurrencyData();
    EquippedLister equippedLister = new EquippedLister();


   

    public class ButtonData
    {
        public string isName;
        public bool isTaken;
        public bool isWear;
    }

    public class EquippedLister
    {
        public List<So_Clothe_Settings> EquippedData = new List<So_Clothe_Settings>();
    }

    public class CurrencyData
    {
        public int Amount;
    }


    public class ButtonDataList
    {
        public List<ButtonData> buttonDatas = new List<ButtonData>(); // Diziyi List'e çevirdik
    }

   

    // Veriyi kaydetme fonksiyonu
    public void SaveDatas(So_Clothe_Settings so_Clothe)
    {
        LoadData();
        // Yeni ButtonData oluþtur
        ButtonData buttonData = new ButtonData
        {
            isName = so_Clothe.name,
            isTaken = so_Clothe.isTaken,
            isWear = so_Clothe.isWear
        };
        buttonDataList.buttonDatas.Add(buttonData);
        SaveCoinDatas();
        EquippedDatas();
        Outputjson();
    }
    void SaveCoinDatas()
    {
        currencyData.Amount = CoinValue.Amount;
    }
    void EquippedDatas()
    {
        for(int i = 0; i<ItemHolder.EquippedData.Count; i++)
        {
            equippedLister.EquippedData.Add(ItemHolder.EquippedData[i]);
        }
    }
   
    void Outputjson()
    {
        
        string ItemDatas = JsonUtility.ToJson(buttonDataList,true);
        string CurrencyDatas = JsonUtility.ToJson(currencyData);
        string EquippedDatas = JsonUtility.ToJson(equippedLister,true);
        File.WriteAllText(savePath, ItemDatas);
        File.WriteAllText(CoinSavePath, CurrencyDatas);
        File.WriteAllText(EquippedItemDataPath, EquippedDatas);
        // File.WriteAllText(CoinSavePath, CurrencyDatas);
    }

    // JSON dosyasýný okuma fonksiyonu
    void LoadData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            buttonDataList = JsonUtility.FromJson<ButtonDataList>(json); 
        }
        else
        {
            Debug.Log("No existing data file found, starting fresh.");
        }
    }
}