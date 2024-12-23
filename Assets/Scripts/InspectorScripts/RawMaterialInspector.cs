#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine.UI;

public class RawMaterialInspector : OdinEditorWindow
{
    [Title("Raw Material Settings")]
    [TableList]
    [SerializeField] List<So_RawMaterialScript> RawMaterials; // RawMaterials, So_RawMaterialScript türünde

    [Space(10)]
    [Title("Grid Settings")]
    [HorizontalGroup("Float Group", 0.5f, LabelWidth = 150)]
    [LabelText("Cell Count")]
    [SerializeField] float CellCount;

    [HorizontalGroup("Second Float Group", 0.5f, LabelWidth = 150)]
    [LabelText("Distance Between Objects")]
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

    [MenuItem("Game Settings/Raw Material Inspector")]
    public static void ShowWindow()
    {
        GetWindow<RawMaterialInspector>().Show();
    }

    [Button("Save All Settings")]
    private void SaveData()
    {
        var data = new RawMaterialInspectorData
        {
            RawMaterials = RawMaterials,
            DistanceBetweenObject = DistanceBetweenObject,
            LowerDistance = LowerDistance,
            CanvasUnderObject = CanvasUnderObject
        };

        var serializedData = JsonUtility.ToJson(data);
        string filePath = Application.dataPath + "/MenuSavingObjects/" + saveName + ".json";
        System.IO.File.WriteAllText(filePath, serializedData);
        Debug.Log("Raw Material settings saved successfully!");
    }

    [Button("Load All Data")]
    private void LoadData()
    {
        string dataReading = System.IO.File.ReadAllText(Application.dataPath + "/MenuSavingObjects/" + jsonLoadName + ".json");
        var data = JsonUtility.FromJson<RawMaterialInspectorData>(dataReading);

        RawMaterials = data.RawMaterials;
        CellCount = data.CellCount;
        DistanceBetweenObject = data.DistanceBetweenObject;
        LowerDistance = data.LowerDistance;
        CanvasUnderObject = data.CanvasUnderObject;

        Debug.Log("Raw Material data loaded successfully!");
    }

    [Button("Instantiate Objects")]
    private void InstantiateObjects()
    {
        if (CanvasUnderObject == null || instantiateGameobject == null)
        {
            Debug.LogWarning("Canvas or Prefab not set!");
            return;
        }

        Debug.Log("Instantiating raw material objects...");

        var gridControl = CanvasUnderObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<GridLayoutGroup>();
        RectTransform rectTransform = CanvasUnderObject.transform.GetChild(0).GetComponent<RectTransform>();

        float paddingHorizontal = gridControl.padding.left + gridControl.padding.right;

        gridControl.spacing = new Vector2(DistanceBetweenObject, LowerDistance);
        float availableWidth = rectTransform.rect.width - paddingHorizontal - (gridControl.spacing.x * (CellCount - 1));

        float cellSize = availableWidth / CellCount;

        gridControl.cellSize = new Vector2(cellSize, cellSize);

        for (int i = 0; i < RawMaterials.Count; i++)
        {
            // Instantiate the prefab under the Canvas structure
            var instantiatedObject = Instantiate(instantiateGameobject, CanvasUnderObject.transform.GetChild(0).GetChild(0).GetChild(0));
            var marketingButtonOptions = instantiatedObject.GetComponent<RawMateraiStore>();

            // Reset the button settings before assigning a new one
            marketingButtonOptions.ButtonSettingObject = null;

            // Atama iþlemi - `So_RawMaterialScript` türünde nesneyi `MarketingButtonOptions`'a atýyoruz
            marketingButtonOptions.ButtonSettingObject = RawMaterials[i];

            // Button'u yeniden oluþtur
            marketingButtonOptions.ReCreate();
        }

    }

    [System.Serializable]
    private class RawMaterialInspectorData
    {
        public List<So_RawMaterialScript> RawMaterials; // RawMaterials listesinin tipi So_RawMaterialScript
        public float CellCount;
        public float DistanceBetweenObject;
        public float LowerDistance;
        public GameObject CanvasUnderObject;
    }
}
#endif
