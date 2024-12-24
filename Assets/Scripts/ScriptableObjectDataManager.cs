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
    private string rawMaterialDataPath;

    public SO_ValueMaker CoinValue;
    public EquippedItem ItemHolder;

    private RawMaterialDataList rawMaterialDataList = new RawMaterialDataList(); // Yeni veri yapýsý

    private void Awake()
    {
        savePath = Path.Combine(Application.dataPath, "Datas.json");
        coinSavePath = Path.Combine(Application.dataPath, "Amounts.json");
        equippedDataPath = Path.Combine(Application.dataPath, "EquippedItemDataPath.json");
        rawMaterialDataPath = Path.Combine(Application.dataPath, "RawMaterialData.json"); // Yeni dosya yolu

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
        LoadRawMaterialData(); 
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
        public List<int> Amount = new List<int>();
    }

    [System.Serializable]
    public class RawMaterialData
    {
        public string MaterialName;
        public int ObjectCount;
    }

    [System.Serializable]
    public class RawMaterialDataList
    {
        public List<RawMaterialData> rawMaterials = new List<RawMaterialData>();
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
                isWear = soClothe.isWear
            };
            buttonDataList.buttonDatas.Add(buttonData);
        }
        else
        {
            buttonData.isTaken = soClothe.isTaken;
            buttonData.isWear = soClothe.isWear;
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
        File.WriteAllText(rawMaterialDataPath, JsonUtility.ToJson(rawMaterialDataList, true));
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

            // Update CoinValue's Amount from loaded data
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
                    item.isWear = buttonDataList.buttonDatas.Exists(data => data.isName == itemName && data.isWear);
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

    public void SaveRawMaterialData(So_RawMaterialScript rawMaterial)
    {
        var materialData = rawMaterialDataList.rawMaterials.Find(data => data.MaterialName == rawMaterial.ObjectName);

        if (materialData == null)
        {
            materialData = new RawMaterialData
            {
                MaterialName = rawMaterial.ObjectName,
                ObjectCount = rawMaterial.ObjectCount
            };
            rawMaterialDataList.rawMaterials.Add(materialData);
        }
        else
        {
            materialData.ObjectCount = rawMaterial.ObjectCount;
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
        WriteToJson(); 
    }

    private void WriteRawMaterialDataToJson()
    {
        File.WriteAllText(rawMaterialDataPath, JsonUtility.ToJson(rawMaterialDataList, true));
    }

    public void LoadRawMaterialData()
    {
        if (File.Exists(rawMaterialDataPath))
        {
            string json = File.ReadAllText(rawMaterialDataPath);
            rawMaterialDataList = JsonUtility.FromJson<RawMaterialDataList>(json);

            foreach (var i in rawMaterialDataList.rawMaterials)
            {
                So_RawMaterialScript RawMaterial = Resources.Load<So_RawMaterialScript>("RawMaterials/" + i.MaterialName);
                RawMaterial.ObjectCount = i.ObjectCount;
            }
        }
        else
        {
            Debug.Log("No existing raw material data file found, starting fresh.");
        }
    }

    public int GetObjectCount(string materialName)
    {
        var materialData = rawMaterialDataList.rawMaterials.Find(data => data.MaterialName == materialName);
        return materialData != null ? materialData.ObjectCount : 0;
    }

    public void UpdateSavedData(So_Clothe_Settings soClothe)
    {
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
            rawMaterialDataList.rawMaterials.Clear(); // Yeni veri temizleme
            WriteToJson();
            WriteRawMaterialDataToJson();
        }
    }
}
