using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScriptableObjectDataManager : MonoBehaviour
{
    public ButtonDataList buttonDataList = new ButtonDataList();
    private CurrencyData currencyData = new CurrencyData();
    private EquippedLister equippedLister = new EquippedLister();

    public static ScriptableObjectDataManager Instance { get; private set; }
    private string savePath;
    private string coinSavePath;
    private string equippedDataPath;
    public SO_ValueMaker CoinValue;
    public EquippedItem ItemHolder;

    string equippedDataStr;
    string DataStr;
    private void Awake()
    {
        savePath = Path.Combine(Application.dataPath, "Datas.json");
        coinSavePath = Path.Combine(Application.dataPath, "Amounts.json");
        equippedDataPath = Path.Combine(Application.dataPath, "EquippedItemDataPath.json");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (File.Exists(equippedDataPath))
        {
            equippedDataStr = File.ReadAllText(equippedDataPath);
            equippedLister = JsonUtility.FromJson<EquippedLister>(equippedDataStr);
        }

        if (File.Exists(savePath))
        {
            DataStr = File.ReadAllText(savePath);
            buttonDataList = JsonUtility.FromJson<ButtonDataList>(DataStr);
        }
       

    }

    private void Start()
    {
        LoadEquippedData();
    }

    [System.Serializable]
    public class ButtonData
    {
        public string isName;
        public bool isTaken;
        public bool isWear;
    }

    [System.Serializable]
    public class ButtonDataList
    {
        public List<ButtonData> buttonDatas = new List<ButtonData>();
    }

    public class EquippedLister
    {
        public List<string> EquippedDataNames = new List<string>();
    }

    public class CurrencyData
    {
        public List<int> Amount;
    }

    public void SaveData(So_Clothe_Settings soClothe)
    {
        LoadButtonData();

        ButtonData buttonData = new ButtonData
        {
            isName = soClothe.name,
            isTaken = soClothe.isTaken,
            isWear = soClothe.isWear
        };
        buttonDataList.buttonDatas.Add(buttonData);
        currencyData.Amount = CoinValue.Amount;
        UpdateEquippedData();
        WriteToJson();
    }

    private void UpdateEquippedData()
    {
        equippedLister.EquippedDataNames.Clear();
        foreach (var item in ItemHolder.EquippedData)
        {
            equippedLister.EquippedDataNames.Add(item.name);
        }
    }

    public void WriteToJson()
    {
        File.WriteAllText(savePath, JsonUtility.ToJson(buttonDataList, true));
        File.WriteAllText(coinSavePath, JsonUtility.ToJson(currencyData));
        File.WriteAllText(equippedDataPath, JsonUtility.ToJson(equippedLister));
    }

    public void LoadButtonData()
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

    public void LoadEquippedData()
    {
        if (File.Exists(equippedDataPath))
        {
            
            ItemHolder.EquippedData.Clear();
            int i = 0;
            foreach (string itemName in equippedLister.EquippedDataNames)
            {
                So_Clothe_Settings item = Resources.Load<So_Clothe_Settings>("Wearables/" + itemName);
               
                if (item != null)
                {
                    item.isTaken = true;

                    if (itemName == buttonDataList.buttonDatas[i].isName && buttonDataList.buttonDatas[i].isWear == true)
                        item.isWear = true;
                    else
                        item.isWear = false;
                    
                    
                    ItemHolder.EquippedData.Add(item);
                }
                else
                {
                    Debug.LogWarning($"Item {itemName} not found in Resources.");
                }
                i = i + 1;

            }
        }
        else
        {
            Debug.Log("No existing data file found, starting fresh.");
        }
    }

    public void UpdateSavedData(So_Clothe_Settings soClothe)
    {
        LoadButtonData();

        var buttonData = buttonDataList.buttonDatas.Find(data => data.isName == soClothe.name);

        if (buttonData != null)
        {
            buttonData.isTaken = soClothe.isTaken;
            buttonData.isWear = soClothe.isWear;
        }

        WriteToJson();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            buttonDataList.buttonDatas.Clear();
        }
    }
}
