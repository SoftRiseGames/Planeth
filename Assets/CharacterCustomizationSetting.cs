using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class CharacterCustomizationSetting : MonoBehaviour
{
    public List<Button> CustomizationButtons;
    public EquippedItem equippedItems;
    void Start()
    {
        for(int i = 0; i<equippedItems.EquippedData.Count; i++)
        {
            if(equippedItems.EquippedData[i].isWear == true)
            {
                if (equippedItems.EquippedData[i].WearableType == ObjectType.Helmet)
                {
                    CustomizationButtons[0].GetComponent<Image>().sprite = equippedItems.EquippedData[i].Background;
                    CustomizationButtons[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = equippedItems.EquippedData[i].name;
                    CustomizationButtons[0].transform.GetChild(1).GetComponent<Image>().sprite = equippedItems.EquippedData[i].Skin;
                }
                else if (equippedItems.EquippedData[i].WearableType == ObjectType.Armor)
                {
                    CustomizationButtons[1].GetComponent<Image>().sprite = equippedItems.EquippedData[i].Background;
                    CustomizationButtons[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = equippedItems.EquippedData[i].name;
                    CustomizationButtons[1].transform.GetChild(1).GetComponent<Image>().sprite = equippedItems.EquippedData[i].Skin;
                }
                else if (equippedItems.EquippedData[i].WearableType == ObjectType.Shoes)
                {
                    CustomizationButtons[2].GetComponent<Image>().sprite = equippedItems.EquippedData[i].Background;
                    CustomizationButtons[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = equippedItems.EquippedData[i].name;
                    CustomizationButtons[2].transform.GetChild(1).GetComponent<Image>().sprite = equippedItems.EquippedData[i].Skin;
                }
                else if (equippedItems.EquippedData[i].WearableType == ObjectType.Sword)
                {
                    CustomizationButtons[3].GetComponent<Image>().sprite = equippedItems.EquippedData[i].Background;
                    CustomizationButtons[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = equippedItems.EquippedData[i].name;
                    CustomizationButtons[3].transform.GetChild(1).GetComponent<Image>().sprite = equippedItems.EquippedData[i].Skin;
                }
                else if (equippedItems.EquippedData[i].WearableType == ObjectType.Glove)
                {
                    CustomizationButtons[4].GetComponent<Image>().sprite = equippedItems.EquippedData[i].Background;
                    CustomizationButtons[4].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = equippedItems.EquippedData[i].name;
                    CustomizationButtons[4].transform.GetChild(1).GetComponent<Image>().sprite = equippedItems.EquippedData[i].Skin;
                }

            }
            

        }
    }
}
