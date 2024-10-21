
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
    public Sprite[] Background;
    public Sprite Skin;
    [Title("Boosters")]
    public int CoinBooster;
    public int ClickBooster;

    [Title("ObjectName")]
    public string ObjectName;

    [Title("ObjectFileNameAndFileLoad")]
    public string ObjectFileName;
    public string ObjectLoad;
    [Title("price")]
    public int price;


 
    [Title("Object Types")]
    public enumType WearableType;

    private So_Clothe_Settings loadedWearable;
    [MenuItem("Game Settings/WearableMaker")]
    public static void ShowWindow()
    {
        GetWindow<WearableMaker>().Show();
    }

    [Button("Oluþtur")]
    private void Atama()
    {
        // ScriptableObject yaratma ve kaydetme iþlemi
        So_Clothe_Settings newWearable = ScriptableObject.CreateInstance<So_Clothe_Settings>();

        // Ýlgili özellikleri atayýn
        /*
        newWearable.Background = Background[0];
        newWearable.Skin = Skin;
        */
        newWearable.CoinBooster = CoinBooster;
        newWearable.ClickBooster = ClickBooster;
        newWearable.price = price;
        newWearable.ObjectName = ObjectName;
        int enumtyper = ((int)WearableType);
        newWearable.WearableType = (enumType)enumtyper;

        // ScriptableObject'i kaydetme iþlemi
        string path = "Assets/ScriptableObjects/Wearables";
        if (!AssetDatabase.IsValidFolder(path))
        {
            AssetDatabase.CreateFolder("Assets/ScriptableObjects", "Wearables");
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
        // ScriptableObject dizinini tara
        string[] guids = AssetDatabase.FindAssets("t:So_Clothe_Settings", new[] { "Assets/ScriptableObjects/Wearables" });

        bool found = false;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string assetName = System.IO.Path.GetFileNameWithoutExtension(path);

            // Tam olarak ObjectLoad ile ayný ismi kontrol et
            if (assetName == ObjectLoad)
            {
                loadedWearable = AssetDatabase.LoadAssetAtPath<So_Clothe_Settings>(path);

                if (loadedWearable != null)
                {
                    // Yüklenen ScriptableObject'in özelliklerini mevcut pencereye aktar
                    /*
                    Background[0] = loadedWearable.Background;
                    Skin = loadedWearable.Skin;
                    */
                    CoinBooster = loadedWearable.CoinBooster;
                    ClickBooster = loadedWearable.ClickBooster;
                    price = loadedWearable.price;
                    ObjectName = loadedWearable.ObjectName;
                    int enumtyper = ((int)loadedWearable.WearableType);
                    WearableType = (enumType)enumtyper;

                    Debug.Log($"Wearable '{ObjectLoad}' baþarýyla yüklendi.");
                    Debug.Log(loadedWearable.name);
                    found = true;
                    break; // Doðru varlýk bulunduðunda döngüyü durdur
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
        /*
        Background[0] = loadedWearable.Background;
        Skin = loadedWearable.Skin;
        */
        loadedWearable.CoinBooster = CoinBooster;
        loadedWearable.ClickBooster = ClickBooster;
        loadedWearable.price = price;
        loadedWearable.ObjectName = ObjectName;
        int enumtyper = ((int)WearableType);
        loadedWearable.WearableType = (enumType)enumtyper;
    }
}
#endif
