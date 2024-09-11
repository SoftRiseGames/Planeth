using UnityEngine;

[CreateAssetMenu(fileName ="Wearable",menuName ="Customizable/Wearable")]
public class So_Clothe_Settings : ScriptableObject
{
    public Sprite Background;
    public Sprite Skin;
    public int CoinBooster;
    public int ClickBooster;
    public int price;
    public enumType WearableType;


}
public enum enumType
{
    Hand = 0,
    Helmet = 1,
    armor = 2,
    shoes = 3
}
