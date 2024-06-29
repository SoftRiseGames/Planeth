using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;
using UnityEngine.UI;
public class ItemInspector : OdinEditorWindow
{
    [TableList]
    [SerializeField] List<So_Clothe_Settings> Clothes;

    [HorizontalGroup("Float Group")]
    [SerializeField] float CellCount, DistanceBetweenObject;
    [HorizontalGroup("Second Float group")]

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
          
            DistanceBetweenObject = DistanceBetweenObject,
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
        CellCount = data.CellCount;
        DistanceBetweenObject = data.DistanceBetweenObject;
        LowerDistance = data.LowerDistance;
        CanvasUnderObject = data.CanvasUnderObject;
        
    }

    [Button("Olustur")]
    void DebuggerButton()
    {
        Debug.Log("Objeler olusuturuldu");
       
        var GridControl = CanvasUnderObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<GridLayoutGroup>();
        RectTransform rectTransform = CanvasUnderObject.transform.GetChild(0).GetComponent<RectTransform>();

        float right = rectTransform.anchoredPosition.x + (rectTransform.sizeDelta.x * (1 - rectTransform.pivot.x))-(GridControl.spacing.x*(CellCount-1));
        float height = rectTransform.sizeDelta.y;


        var CellSpaceAllObject = GridControl.spacing.x * (CellCount - 1);
        var CellSize = right / CellCount;
      
        GridControl.cellSize = new Vector2(CellSize, CellSize);
     

        for (int i = 0; i < Clothes.Count; i++)
        {
            Instantiate(instantiateGameobject,CanvasUnderObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform);
        }
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
