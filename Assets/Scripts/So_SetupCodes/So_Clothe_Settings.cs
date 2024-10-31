using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName ="Wearable",menuName ="Customizable/Wearable")]
public class So_Clothe_Settings : ScriptableObject
{
    [Header("Button Settings")]
    public Sprite Background;
    public Sprite Skin;
    public string ObjectName;
    public int ObjectDoubler;
    public int price;
    public ObjectType WearableType;
    public bool isTaken;
    public bool isWear;
    [Header("After Take Object")]
    public Sprite Rockethandle;
    public Sprite RocketBackground;
    public float RocketMaxSpeed;
    
    [Header("After Take Object")]
    public Sprite HelmetWearSprite;
    [Header("After Take Object")]
    public Sprite ArmorWearSprite;
    [Header("After Take Object")]
    public Sprite ShoesWearSprite;
    [Header("After Take Object")]
    public Sprite GloveWearSprite;
    [Header("After Take Object")]
    public Sprite SwordWearSprite;




    [CustomEditor(typeof(So_Clothe_Settings))]
    public class So_Clothe_SettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            So_Clothe_Settings settings = (So_Clothe_Settings)target;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Background"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Skin"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectName"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectDoubler"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("price"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("WearableType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isTaken"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isWear"));

            
            if (settings.WearableType == ObjectType.Rocket)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Rockethandle"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("RocketBackground"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("RocketMaxSpeed"));
            }
            else if(settings.WearableType == ObjectType.Armor)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("ArmorWearSprite"));
            }
            else if(settings.WearableType == ObjectType.Glove)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("GloveWearSprite"));
            }
            else if(settings.WearableType == ObjectType.Helmet)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("HelmetWearSprite"));
            }
            else if(settings.WearableType == ObjectType.Shoes)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("ShoesWearSprite"));
            }
            else if(settings.WearableType == ObjectType.Sword)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("SwordWearSprite"));
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
