using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScriptableObjectDataManager : MonoBehaviour
{


    public ButtonDataList buttonDataList = new ButtonDataList();
    CurrencyData currencyData = new CurrencyData();
    EquippedLister equippedLister = new EquippedLister();

    public static ScriptableObjectDataManager Instance { get; private set; }
    private string savePath;
    private string CoinSavePath;
    private string EquippedItemDataPath;
    public SO_ValueMaker CoinValue;
    public EquippedItem ItemHolder;

    private void Awake()
    {
        savePath = Application.dataPath + "/Datas.json";
        CoinSavePath = Application.dataPath + "/Amounts.json";
        EquippedItemDataPath = Application.dataPath + "/EquippedItemDataPath.json";
        
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
        LoadEquippedData();

    }
    private void Start()
    {
        
    }

    [System.Serializable]
    public class ButtonData
    {
        public string isName;
        public bool isTaken;
        public bool isWear;
    }

    public class EquippedLister
    {
        public List<string> EquippedDataNames = new List<string>(); // Objelerin isimlerini sakla
    }

    public class CurrencyData
    {
        public int Amount;
    }

    [System.Serializable]
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
        equippedLister.EquippedDataNames.Clear();
        foreach (var item in ItemHolder.EquippedData)
        {
            equippedLister.EquippedDataNames.Add(item.name); // Objelerin isimlerini kaydet
        }
    }

    void Outputjson()
    {
        string ItemDatas = JsonUtility.ToJson(buttonDataList, true);
        string CurrencyDatas = JsonUtility.ToJson(currencyData);
        string EquippedDatas = JsonUtility.ToJson(equippedLister);
        File.WriteAllText(savePath, ItemDatas);
        File.WriteAllText(CoinSavePath, CurrencyDatas);
        File.WriteAllText(EquippedItemDataPath, EquippedDatas);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            buttonDataList.buttonDatas.Clear();
    }

    // Objelerin isimleriyle verileri yükle
    void LoadEquippedData()
    {
        if (File.Exists(EquippedItemDataPath))
        {
            string equippedDataStr = File.ReadAllText(EquippedItemDataPath);
            equippedLister = JsonUtility.FromJson<EquippedLister>(equippedDataStr);
            ItemHolder.EquippedData.Clear();

            foreach (string itemName in equippedLister.EquippedDataNames)
            {
                // Ýsme göre ScriptableObject'i bul
                So_Clothe_Settings item = Resources.Load<So_Clothe_Settings>("Wearables/" + itemName);
                if (item != null)
                {
                    item.isTaken = true;
                    ItemHolder.EquippedData.Add(item); // Geri yüklenen objeyi listeye ekle
                }
                else
                {
                    Debug.LogWarning($"Item {itemName} not found in Resources.");
                }
            }
        }
        else
        {
            Debug.Log("No existing data file found, starting fresh.");
        }
    }

    public void UpdateSavedData(So_Clothe_Settings so_Clothe)
    {
        LoadData();
        for (int i = 0; i < buttonDataList.buttonDatas.Count; i++)
        {
            if (buttonDataList.buttonDatas[i].isName == so_Clothe.name)
            {
                buttonDataList.buttonDatas[i].isTaken = so_Clothe.isTaken;
                buttonDataList.buttonDatas[i].isWear = so_Clothe.isWear;
                break;
            }
        }

        Outputjson();
    }
}
