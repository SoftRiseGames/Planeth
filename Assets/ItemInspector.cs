using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;

public class ItemInspector : OdinEditorWindow
{
    [TableList]
    [SerializeField] List<So_Clothe_Settings> Clothes;

    [HorizontalGroup("Float Group")]
    [SerializeField] float AnchorXaxisBasePoint, AnchoryaxisBasePoint, DistanceBetweenObject;
    private float AnchorXaxisUpdated;
    private float AnchorYaxisUpdated;
    [HorizontalGroup("Second Float group")]
    [SerializeField] int MaxWidthLimit;
    [SerializeField] float LowerDistance;

    [SerializeField] string saveName;
    [SerializeField] string jsonLoadName;
    [SerializeField] GameObject CanvasUnderObject;
    [SerializeField] GameObject instantiateGameobject;
    [MenuItem("Game Settings/Main Menu")]
    public static void ShowWindow()
    {
        GetWindow<ItemInspector>().Show();
    }
    [Button("Save All Settings")]
    private void SaveData()
    {
        var data = new ItemInspectorData
        {
            Clothes = Clothes,
            AnchorXaxisBasePoint = AnchorXaxisBasePoint,
            AnchoryaxisBasePoint = AnchoryaxisBasePoint,
            DistanceBetweenObject = DistanceBetweenObject,
            MaxWidthLimit = MaxWidthLimit,
            LowerDistance = LowerDistance,
            CanvasUnderObject = CanvasUnderObject
            
        };

        var serializedData = JsonUtility.ToJson(data);
        string filePath = Application.dataPath + "/MenuSavingObjects/" + saveName+".json";
        System.IO.File.WriteAllText(filePath, serializedData);
        Debug.Log("save");
    }

    [Button("Load All Data")]
    private void LoadData()
    {
        string dataReading = System.IO.File.ReadAllText(Application.dataPath + "/MenuSavingObjects/" + jsonLoadName + ".json");
        var data = JsonUtility.FromJson<ItemInspectorData>(dataReading);

        Clothes = data.Clothes;
        AnchorXaxisBasePoint = data.AnchorXaxisBasePoint;
        AnchoryaxisBasePoint = data.AnchoryaxisBasePoint;
        DistanceBetweenObject = data.DistanceBetweenObject;
        MaxWidthLimit = data.MaxWidthLimit;
        LowerDistance = data.LowerDistance;
        CanvasUnderObject = data.CanvasUnderObject;
        
    }
    [Button("Set Variables")]
    public void StartingVariables()
    {
        AnchorXaxisUpdated = AnchorXaxisBasePoint;
        AnchorYaxisUpdated = AnchoryaxisBasePoint;
        Debug.Log("Variables Balanced");
    }

    [Button("Olustur")]
    void DebuggerButton()
    {
        Debug.Log("Objeler olusuturuldu");
        for (int i = 0; i < Clothes.Count; i++)
        {
            if (i / MaxWidthLimit - 1 == 0)
            {
                Debug.Log(i / MaxWidthLimit - 1);
                AnchorYaxisUpdated = AnchorYaxisUpdated - LowerDistance;
                AnchorXaxisUpdated = AnchorXaxisBasePoint;
            }
            RectTransform rectTransform = instantiateGameobject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(AnchorXaxisUpdated, AnchorYaxisUpdated);
            Instantiate(instantiateGameobject,CanvasUnderObject.transform);
            AnchorXaxisUpdated = AnchorXaxisUpdated + DistanceBetweenObject;
        }
    }

  

    
    [System.Serializable]
    private class ItemInspectorData
    {
        public List<So_Clothe_Settings> Clothes;
        public float AnchorXaxisBasePoint;
        public float AnchoryaxisBasePoint;
        public float DistanceBetweenObject;
        public int MaxWidthLimit;
        public float LowerDistance;
        public GameObject CanvasUnderObject;
    }
}
