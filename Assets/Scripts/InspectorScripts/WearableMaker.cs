#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

public class WearableMaker : OdinEditorWindow
{
    [Title("Sprites")]
    public Sprite Background;
    public Sprite Skin;

    [Title("Object Name")]
    public string ObjectName;

    [Title("Object File Name and File Load")]
    public string ObjectFileName;
    public string ObjectLoad;

    [Title("Price")]
    public List<int> MaterialRequirement;

    [Title("Object Types")]
    public ObjectType WearableType;

    [ShowIf("WearableType", ObjectType.Rocket)]
    public Sprite Rockethandle;
    [ShowIf("WearableType", ObjectType.Rocket)]
    public Sprite RocketBackground;
    [ShowIf("WearableType", ObjectType.Rocket)]
    public float RocketMaxSpeed;

    [ShowIf("WearableType", ObjectType.Helmet)]
    public Sprite HelmetWearSprite;

    [ShowIf("WearableType", ObjectType.Armor)]
    public Sprite ArmorWearSprite;

    [ShowIf("WearableType", ObjectType.Shoes)]
    public Sprite ShoesLWearSprite;
    [ShowIf("WearableType", ObjectType.Shoes)]
    public Sprite ShoesRWearSprite;
    [ShowIf("WearableType", ObjectType.Shoes)]
    public int MaxSpeedIncreaseValue;

    [ShowIf("WearableType", ObjectType.Glove)]
    public Sprite GloveLWearSprite;
    [ShowIf("WearableType", ObjectType.Glove)]
    public Sprite GloveRWearSprite;

    [ShowIf("WearableType", ObjectType.Sword)]
    public Sprite SwordWearSprite;

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

        newWearable.MaterialRequirement = MaterialRequirement;
        newWearable.ObjectName = ObjectName;
        newWearable.Skin = Skin;
        newWearable.Background = Background;
        newWearable.WearableType = WearableType;

        if (WearableType == ObjectType.Rocket)
        {
            newWearable.Rockethandle = Rockethandle;
            newWearable.RocketBackground = RocketBackground;
            newWearable.RocketMaxSpeed = RocketMaxSpeed;
        }
        else if (WearableType == ObjectType.Helmet)
        {
            newWearable.HelmetWearSprite = HelmetWearSprite;
        }
        else if (WearableType == ObjectType.Armor)
        {
            newWearable.ArmorWearSprite = ArmorWearSprite;
        }
        else if (WearableType == ObjectType.Shoes)
        {
            newWearable.ShoesLWearSprite = ShoesLWearSprite;
            newWearable.ShoesRWearSprite = ShoesRWearSprite;
            newWearable.MaxSpeedIncreaseValue = MaxSpeedIncreaseValue;
        }
        else if (WearableType == ObjectType.Glove)
        {
            newWearable.GloveLWearSprite = GloveLWearSprite;
            newWearable.GloveRWearSprite = GloveRWearSprite;
        }
        else if (WearableType == ObjectType.Sword)
        {
            newWearable.SwordWearSprite = SwordWearSprite;
        }

        string path = "Assets/Resources/Wearables";
        if (!AssetDatabase.IsValidFolder(path))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Wearables");
        }

        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{path}/{ObjectFileName}.asset");
        AssetDatabase.CreateAsset(newWearable, assetPath);
        AssetDatabase.SaveAssets();

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
                    MaterialRequirement = loadedWearable.MaterialRequirement;
                    ObjectName = loadedWearable.ObjectName;
                    Background = loadedWearable.Background;
                    Skin = loadedWearable.Skin;
                    WearableType = loadedWearable.WearableType;

                    if (WearableType == ObjectType.Rocket)
                    {
                        Rockethandle = loadedWearable.Rockethandle;
                        RocketBackground = loadedWearable.RocketBackground;
                        RocketMaxSpeed = loadedWearable.RocketMaxSpeed;
                    }
                    else if (WearableType == ObjectType.Helmet)
                    {
                        HelmetWearSprite = loadedWearable.HelmetWearSprite;
                    }
                    else if (WearableType == ObjectType.Armor)
                    {
                        ArmorWearSprite = loadedWearable.ArmorWearSprite;
                    }
                    else if (WearableType == ObjectType.Shoes)
                    {
                        ShoesLWearSprite = loadedWearable.ShoesLWearSprite;
                        ShoesRWearSprite = loadedWearable.ShoesRWearSprite;
                        MaxSpeedIncreaseValue = loadedWearable.MaxSpeedIncreaseValue;
                    }
                    else if (WearableType == ObjectType.Glove)
                    {
                        GloveLWearSprite = loadedWearable.GloveLWearSprite;
                        GloveRWearSprite = loadedWearable.GloveRWearSprite;
                    }
                    else if (WearableType == ObjectType.Sword)
                    {
                        SwordWearSprite = loadedWearable.SwordWearSprite;
                    }

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
}
#endif
