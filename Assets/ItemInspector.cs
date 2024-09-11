using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine.UI;

public class ItemInspector : OdinEditorWindow
{
    [Title("Clothing Settings")]
    [TableList]
    [SerializeField] List<So_Clothe_Settings> Clothes;

    [Space(10)]
    [Title("Grid Settings")]
    [HorizontalGroup("Float Group", 0.5f, LabelWidth = 150)]
    [LabelText("Cell Count")]
    [SerializeField] float CellCount;

    [HorizontalGroup("Second Float Group", 0.5f, LabelWidth = 150)]
    [LabelText("Distance Between Objects")] // "Distance Between Objects" now properly under "Grid Settings"
    [SerializeField] float DistanceBetweenObject;

    [Space(10)]
    [HorizontalGroup("Third Float Group", 0.5f, LabelWidth = 150)]
    [LabelText("Lower Distance")]
    [SerializeField] float LowerDistance;

    [Space(20)]
    [Title("Save/Load Settings")]
    [HorizontalGroup("Save Group", 0.5f)]
    [LabelText("Save Name")]
    [SerializeField] string saveName;

    [HorizontalGroup("Second Save Group", 0.5f)]
    [LabelText("JSON Load Name")] 
    [SerializeField] string jsonLoadName;

    [Space(10)]
    [Title("Instantiation Settings")]
    [LabelText("Canvas Under Object")]
    [SerializeField] GameObject CanvasUnderObject;

    [LabelText("Prefab to Instantiate")]
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
            DistanceBetweenObject = DistanceBetweenObject,
            LowerDistance = LowerDistance,
            CanvasUnderObject = CanvasUnderObject
        };

        var serializedData = JsonUtility.ToJson(data);
        string filePath = Application.dataPath + "/MenuSavingObjects/" + saveName + ".json";
        System.IO.File.WriteAllText(filePath, serializedData);
        Debug.Log("Settings saved successfully!");
    }

    [Button("Load All Data")]
    private void LoadData()
    {
        string dataReading = System.IO.File.ReadAllText(Application.dataPath + "/MenuSavingObjects/" + jsonLoadName + ".json");
        var data = JsonUtility.FromJson<ItemInspectorData>(dataReading);

        Clothes = data.Clothes;
        CellCount = data.CellCount;
        DistanceBetweenObject = data.DistanceBetweenObject;
        LowerDistance = data.LowerDistance;
        CanvasUnderObject = data.CanvasUnderObject;

        Debug.Log("Data loaded successfully!");
    }

    [Button("Instantiate Objects")]
    private void InstantiateObjects()
    {
        if (CanvasUnderObject == null || instantiateGameobject == null)
        {
            Debug.LogWarning("Canvas or Prefab not set!");
            return;
        }

        Debug.Log("Instantiating objects...");

        var gridControl = CanvasUnderObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<GridLayoutGroup>();
        RectTransform rectTransform = CanvasUnderObject.transform.GetChild(0).GetComponent<RectTransform>();

        float right = rectTransform.anchoredPosition.x + (rectTransform.sizeDelta.x * (1 - rectTransform.pivot.x)) - (gridControl.spacing.x * (CellCount - 1));
        float height = rectTransform.sizeDelta.y;

        float cellSpaceAllObject = gridControl.spacing.x * (CellCount - 1);
        float cellSize = right / CellCount;

        gridControl.cellSize = new Vector2(cellSize, cellSize);

        for (int i = 0; i < Clothes.Count; i++)
        {
            Instantiate(instantiateGameobject, CanvasUnderObject.transform.GetChild(0).GetChild(0).GetChild(0));
        }

        Debug.Log("Objects instantiated successfully!");
    }

    [System.Serializable]
    private class ItemInspectorData
    {
        public List<So_Clothe_Settings> Clothes;
        public float CellCount;
        public float DistanceBetweenObject;
        public float LowerDistance;
        public GameObject CanvasUnderObject;
    }
}
