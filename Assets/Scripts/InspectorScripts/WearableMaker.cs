 
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;
using UnityEngine.UI;

public class WearableMaker : OdinEditorWindow
{
    [Title("Sprites")]
    public Sprite Background;
    public Sprite Skin;
    [Title("Boosters")]
    public List<int> ObjectMainFeatureValue;

    [Title("ObjectName")]
    public string ObjectName;

    [Title("ObjectFileNameAndFileLoad")]
    public string ObjectFileName;
    public string ObjectLoad;
    [Title("price")]
    public List<int> MaterialRequirement;


 
    [Title("Object Types")]
    public ObjectType WearableType;

    private So_Clothe_Settings loadedWearable;
    [MenuItem("Game Settings/WearableMaker")]
    public static void ShowWindow()
    {
        GetWindow<WearableMaker>().Show();
    }

    [Button("Oluþtur")]
    private void Atama()
    {
        So_Clothe_Settings newWearable = ScriptableObject.CreateInstance<So_Clothe_Settings>();

        // Ýlgili özellikleri atayýn
        /*
        newWearable.Background = Background[0];
        newWearable.Skin = Skin;
        */
        newWearable.ObjectMainFeatureValue = ObjectMainFeatureValue;
        newWearable.MaterialRequirement = MaterialRequirement;
        newWearable.ObjectName = ObjectName;
        newWearable.Skin = Skin;
        newWearable.Background = Background;
        int enumtyper = ((int)WearableType);
        newWearable.WearableType = (ObjectType)enumtyper;

        
        string path = "Assets/Resources/Wearables";
        if (!AssetDatabase.IsValidFolder(path))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Wearables");
        }

        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{path}/"+ObjectFileName+".asset");
        AssetDatabase.CreateAsset(newWearable, assetPath);
        AssetDatabase.SaveAssets();

        // Oluþum tamamlandýðýnda konsola bilgi verin
        Debug.Log("Yeni Wearable ScriptableObject oluþturuldu ve kaydedildi.");
    }

    [Button("Tara ve Yükle")]
    private void TaraVeYukle()
    {
        
        string[] guids = AssetDatabase.FindAssets("t:So_Clothe_Settings", new[] { "Assets/Resources/Wearables" });

        bool found = false;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string assetName = System.IO.Path.GetFileNameWithoutExtension(path);
            
            if (assetName == ObjectLoad)
            {
                loadedWearable = AssetDatabase.LoadAssetAtPath<So_Clothe_Settings>(path);

                if (loadedWearable != null)
                {

                    ObjectMainFeatureValue = loadedWearable.ObjectMainFeatureValue;
                    
                    MaterialRequirement = loadedWearable.MaterialRequirement;
                    ObjectName = loadedWearable.ObjectName;
                    Background = loadedWearable.Background;
                    Skin = loadedWearable.Skin;
                    int enumtyper = ((int)loadedWearable.WearableType);
                    WearableType = (ObjectType)enumtyper;

                    Debug.Log($"Wearable '{ObjectLoad}' baþarýyla yüklendi.");
                    Debug.Log(loadedWearable.name);
                    found = true;
                    break; 
                }
            }
        }
        

        if (!found)
        {
            Debug.LogWarning($"Wearable '{ObjectLoad}' bulunamadý.");
        }
    }
    [Button("Güncelle")]
    private void UpdateObject()
    {
      
        loadedWearable.ObjectMainFeatureValue = ObjectMainFeatureValue;
        loadedWearable.MaterialRequirement = MaterialRequirement;
        loadedWearable.ObjectName = ObjectName;
        loadedWearable.Skin = Skin;
        loadedWearable.Background = Background;
        int enumtyper = ((int)WearableType);
        loadedWearable.WearableType = (ObjectType)enumtyper;
    }
}
#endif
