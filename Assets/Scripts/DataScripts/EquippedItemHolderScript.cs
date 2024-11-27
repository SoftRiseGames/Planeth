using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItemHolderScript : MonoBehaviour
{
    public EquippedItem EquippedItem;
    [SerializeField] GameObject EquipButton;
    private int EquippedItemCount;
    public List<GameObject> EquippedItemMenu;


    //0 for helmet
    //1 for armor
    //2 for shoes
    private void Start()
    {
        foreach (GameObject EquippedObject in EquippedItemMenu)
            EquippedObject.SetActive(false);

        EquippedItemMenu[0].SetActive(true);

        EquippedItemCount = -1;
        EquipItemList();
    }
    void EquipItemList()
    {

        EquippedItemHolderScript equippeditems = GetComponent<EquippedItemHolderScript>();

        for (int i = 0; i < equippeditems.EquippedItem.EquippedData.Count; i++)
        {
            if (i > EquippedItemCount)
            {
                if (equippeditems.EquippedItem.EquippedData[i].WearableType == ObjectType.Helmet)
                {
                    GameObject newButton = Instantiate(EquipButton, EquippedItemMenu[0].transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform);
                    newButton.GetComponent<EquipButton>().clothes = equippeditems.EquippedItem.EquippedData[i];
                }
                else if (equippeditems.EquippedItem.EquippedData[i].WearableType == ObjectType.Armor)
                {
                    GameObject newButton = Instantiate(EquipButton, EquippedItemMenu[1].transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform);
                    newButton.GetComponent<EquipButton>().clothes = equippeditems.EquippedItem.EquippedData[i];
                }
                else if (equippeditems.EquippedItem.EquippedData[i].WearableType == ObjectType.Shoes)
                {
                    GameObject newButton = Instantiate(EquipButton, EquippedItemMenu[2].transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform);
                    newButton.GetComponent<EquipButton>().clothes = equippeditems.EquippedItem.EquippedData[i];
                }


            }
        }

        EquippedItemCount = equippeditems.EquippedItem.EquippedData.Count - 1;
    }
}
