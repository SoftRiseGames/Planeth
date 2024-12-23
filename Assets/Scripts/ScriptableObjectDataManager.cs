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
            return;
        }

        LoadButtonData();
        LoadEquippedData();
    }

    [System.Serializable]
    public class ButtonData
    {
        public string isName;
        public bool isTaken;
        public bool isWear;
        public int ObjectUpgaradeIndex; // Upgrade indeksi
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
        public List<int> Amount = new List<int>();
    }

    public void SaveData(So_Clothe_Settings soClothe)
    {
        var buttonData = buttonDataList.buttonDatas.Find(data => data.isName == soClothe.name);

        if (buttonData == null)
        {
            buttonData = new ButtonData
            {
                isName = soClothe.name,
                isTaken = soClothe.isTaken,
                isWear = soClothe.isWear,
                ObjectUpgaradeIndex = soClothe.ObjectUpgradeIndex
            };
            buttonDataList.buttonDatas.Add(buttonData);
        }
        else
        {
            buttonData.isTaken = soClothe.isTaken;
            buttonData.isWear = soClothe.isWear;
            buttonData.ObjectUpgaradeIndex = soClothe.ObjectUpgradeIndex;
        }

        for (int i = 0; i < CoinValue.Amount.Count; i++)
        {
            if (i >= currencyData.Amount.Count)
            {
                currencyData.Amount.Add(CoinValue.Amount[i]);
            }
            else
            {
                currencyData.Amount[i] = CoinValue.Amount[i];
            }
        }

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
        File.WriteAllText(coinSavePath, JsonUtility.ToJson(currencyData, true));
        File.WriteAllText(equippedDataPath, JsonUtility.ToJson(equippedLister, true));
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
            Debug.Log("No existing button data file found, starting fresh.");
        }

        if (File.Exists(coinSavePath))
        {
            string coinJson = File.ReadAllText(coinSavePath);
            currencyData = JsonUtility.FromJson<CurrencyData>(coinJson);

            CoinValue.Amount.Clear();
            CoinValue.Amount.AddRange(currencyData.Amount);
        }
        else
        {
            Debug.Log("No existing currency data file found, starting fresh.");
        }
    }

    public void LoadEquippedData()
    {
        if (File.Exists(equippedDataPath))
        {
            string equippedJson = File.ReadAllText(equippedDataPath);
            equippedLister = JsonUtility.FromJson<EquippedLister>(equippedJson);

            ItemHolder.EquippedData.Clear();
            foreach (string itemName in equippedLister.EquippedDataNames)
            {
                So_Clothe_Settings item = Resources.Load<So_Clothe_Settings>("Wearables/" + itemName);
                if (item != null)
                {
                    item.isTaken = true;

                    var buttonData = buttonDataList.buttonDatas.Find(data => data.isName == itemName);
                    if (buttonData != null)
                    {
                        item.isWear = buttonData.isWear;
                        item.ObjectUpgradeIndex = buttonData.ObjectUpgaradeIndex;
                    }

                    ItemHolder.EquippedData.Add(item);
                }
                else
                {
                    Debug.LogWarning($"Item {itemName} not found in Resources.");
                }
            }
        }
        else
        {
            Debug.Log("No existing equipped data file found, starting fresh.");
        }
    }

    public void UpdateSavedData(So_Clothe_Settings soClothe)
    {
        var buttonData = buttonDataList.buttonDatas.Find(data => data.isName == soClothe.name);

        if (buttonData != null)
        {
            buttonData.isTaken = soClothe.isTaken;
            buttonData.isWear = soClothe.isWear;
            buttonData.ObjectUpgaradeIndex = soClothe.ObjectUpgradeIndex; // ObjectUpgradeIndex güncelleniyor
        }
        else
        {
            buttonData = new ButtonData
            {
                isName = soClothe.name,
                isTaken = soClothe.isTaken,
                isWear = soClothe.isWear,
                ObjectUpgaradeIndex = soClothe.ObjectUpgradeIndex
            };
            buttonDataList.buttonDatas.Add(buttonData);
        }

        WriteToJson();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            buttonDataList.buttonDatas.Clear();
            WriteToJson();
        }
    }
}
