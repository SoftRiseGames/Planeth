#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

public class RawMaterialMaker : OdinEditorWindow
{
    [Title("Sprites")]
    public Sprite Background;
    public Sprite Skin;

    [Title("Material Properties")]
    public string MaterialName;
    public int ObjectCount;

    [Title("Material File Name and Load")]
    public string MaterialFileName;
    public string MaterialLoad;

    [Title("Requirements")]
    public List<int> MaterialRequirements;

    private So_RawMaterialScript loadedMaterial;

    [MenuItem("Game Settings/RawMaterialMaker")]
    public static void ShowWindow()
    {
        GetWindow<RawMaterialMaker>().Show();
    }

    [Button("Create Material")]
    private void CreateMaterial()
    {
        So_RawMaterialScript newMaterial = ScriptableObject.CreateInstance<So_RawMaterialScript>();

        newMaterial.Background = Background;
        newMaterial.Skin = Skin;
        newMaterial.ObjectName = MaterialName;
        newMaterial.ObjectCount = ObjectCount;
        newMaterial.MaterialRequipment = MaterialRequirements;

        string path = "Assets/Resources/RawMaterials";
        if (!AssetDatabase.IsValidFolder(path))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "RawMaterials");
        }

        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{path}/" + MaterialFileName + ".asset");
        AssetDatabase.CreateAsset(newMaterial, assetPath);
        AssetDatabase.SaveAssets();

        Debug.Log("New Raw Material ScriptableObject created and saved.");
    }

    [Button("Scan and Load Material")]
    private void ScanAndLoadMaterial()
    {
        string[] guids = AssetDatabase.FindAssets("t:So_RawMaterialScript", new[] { "Assets/Resources/RawMaterials" });

        bool found = false;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string assetName = System.IO.Path.GetFileNameWithoutExtension(path);

            if (assetName == MaterialLoad)
            {
                loadedMaterial = AssetDatabase.LoadAssetAtPath<So_RawMaterialScript>(path);

                if (loadedMaterial != null)
                {
                    MaterialName = loadedMaterial.ObjectName;
                    ObjectCount = loadedMaterial.ObjectCount;
                    MaterialRequirements = loadedMaterial.MaterialRequipment;
                    Background = loadedMaterial.Background;
                    Skin = loadedMaterial.Skin;

                    Debug.Log($"Material '{MaterialLoad}' successfully loaded.");
                    found = true;
                    break;
                }
            }
        }

        if (!found)
        {
            Debug.LogWarning($"Material '{MaterialLoad}' not found.");
        }
    }

    [Button("Update Material")]
    private void UpdateMaterial()
    {
        if (loadedMaterial == null)
        {
            Debug.LogWarning("No material loaded to update.");
            return;
        }

        loadedMaterial.ObjectName = MaterialName;
        loadedMaterial.ObjectCount = ObjectCount;
        loadedMaterial.MaterialRequipment = MaterialRequirements;
        loadedMaterial.Background = Background;
        loadedMaterial.Skin = Skin;

        EditorUtility.SetDirty(loadedMaterial);
        AssetDatabase.SaveAssets();

        Debug.Log("Material updated successfully.");
    }
}
#endif
