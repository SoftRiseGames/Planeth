using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterCustomizationMenu : MonoBehaviour
{
    //0 for helmet
    //1 for armor
    //2 for shoes
    //3 for sword
    //4 for glove


    public EquippedItem equippedItem;
    public GameObject[] CustomizationButtons;
    public GameObject[] CharacterVisuals;

    void Start()
    {
        MenuCustomization();
    }
    void MenuCustomization()
    {
        
        foreach(So_Clothe_Settings equippedItem in equippedItem.EquippedData)
        {
            if (equippedItem.isWear)
            {
                if (equippedItem.WearableType == ObjectType.Helmet)
                {
                    CustomizationButtons[0].GetComponent<Image>().sprite = equippedItem.Background;
                    CustomizationButtons[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = equippedItem.ObjectName.ToString();
                    CustomizationButtons[0].transform.GetChild(1).GetComponent<Image>().sprite = equippedItem.Skin;
                    CharacterVisuals[0].GetComponent<Image>().sprite = equippedItem.HelmetWearSprite;
                }
                else if (equippedItem.WearableType == ObjectType.Armor)
                {
                    CustomizationButtons[1].GetComponent<Image>().sprite = equippedItem.Background;
                    CustomizationButtons[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = equippedItem.ObjectName.ToString();
                    CustomizationButtons[1].transform.GetChild(1).GetComponent<Image>().sprite = equippedItem.Skin;
                    CharacterVisuals[1].GetComponent<Image>().sprite = equippedItem.ArmorWearSprite;
                }
                else if (equippedItem.WearableType == ObjectType.Shoes)
                {
                    CustomizationButtons[2].GetComponent<Image>().sprite = equippedItem.Background;
                    CustomizationButtons[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = equippedItem.ObjectName.ToString();
                    CustomizationButtons[2].transform.GetChild(1).GetComponent<Image>().sprite = equippedItem.Skin;
                    CharacterVisuals[2].transform.GetChild(0).GetComponent<Image>().sprite = equippedItem.ShoesLWearSprite;
                    CharacterVisuals[2].transform.GetChild(1).GetComponent<Image>().sprite = equippedItem.ShoesRWearSprite;
                }
                else if (equippedItem.WearableType == ObjectType.Sword)
                {
                    CustomizationButtons[3].GetComponent<Image>().sprite = equippedItem.Background;
                    CustomizationButtons[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = equippedItem.ObjectName.ToString();
                    CustomizationButtons[3].transform.GetChild(1).GetComponent<Image>().sprite = equippedItem.Skin;
                    CharacterVisuals[3].GetComponent<Image>().sprite = equippedItem.SwordWearSprite;
                }
                else if (equippedItem.WearableType == ObjectType.Glove)
                {
                    CustomizationButtons[4].GetComponent<Image>().sprite = equippedItem.Background;
                    CustomizationButtons[4].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = equippedItem.ObjectName.ToString();
                    CustomizationButtons[4].transform.GetChild(1).GetComponent<Image>().sprite = equippedItem.Skin;
                    CharacterVisuals[4].transform.GetChild(0).GetComponent<Image>().sprite = equippedItem.GloveLWearSprite;
                    CharacterVisuals[4].transform.GetChild(1).GetComponent<Image>().sprite = equippedItem.GloveRWearSprite;
                }
            }
           
        }
        
    }
}
