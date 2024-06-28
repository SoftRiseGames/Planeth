using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;

public class ItemInspector : OdinEditorWindow
{
    private const string PreferencesKey = "ItemInspector.Data";

    [TableList]
    [SerializeField] List<So_Clothe_Settings> Clothes;

    [HorizontalGroup("Float Group")]
    [SerializeField] float AnchorXaxisBasePoint, AnchoryaxisBasePoint, DistanceBetweenObject;
    private float AnchorXaxisUpdated;
    private float AnchorYaxisUpdated;
    [HorizontalGroup("Second Float group")]
    [SerializeField] float MaxWidthLimit, LowerDistance;

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
            LowerDistance = LowerDistance
        };

        var serializedData = JsonUtility.ToJson(data);
        EditorPrefs.SetString(PreferencesKey, serializedData);
    }

    [Button("Load All Data")]
    private void LoadData()
    {
        if (EditorPrefs.HasKey(PreferencesKey))
        {
            var serializedData = EditorPrefs.GetString(PreferencesKey);
            var data = JsonUtility.FromJson<ItemInspectorData>(serializedData);

            Clothes = data.Clothes;
            AnchorXaxisBasePoint = data.AnchorXaxisBasePoint;
            AnchoryaxisBasePoint = data.AnchoryaxisBasePoint;
            DistanceBetweenObject = data.DistanceBetweenObject;
            MaxWidthLimit = data.MaxWidthLimit;
            LowerDistance = data.LowerDistance;
        }
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
            Instantiate(instantiateGameobject, new Vector2(AnchorXaxisUpdated, AnchorYaxisUpdated), Quaternion.identity);
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
        public float MaxWidthLimit;
        public float LowerDistance;
    }
}
