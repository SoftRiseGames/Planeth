using UnityEngine;

[CreateAssetMenu(fileName ="Wearable",menuName ="Customizable/Wearable")]
public class So_Clothe_Settings : ScriptableObject
{
    public Sprite Background;
    public Sprite Skin;
    public string ObjectName;
    public int ObjectDoubler;
    public int price;
    public ObjectType WearableType;
    public bool isTaken;
    public bool isWear;

}
