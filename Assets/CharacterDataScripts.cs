using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataScripts : MonoBehaviour
{
    
    public int DamagePower;
    public Sprite HelmetSprite;
    public Sprite ArmorSprite;
    public Sprite ShoesSprite;
    public Sprite HandSprite;

    public EquippedItem EquippedItems;
    void Start()
    {
       
    }

    void StartSprites()
    {
         foreach(So_Clothe_Settings equippedItem in EquippedItems.EquippedData)
        {
            if(equippedItem.isWear == true)
            {
                if (equippedItem.WearableType == ObjectType.Helmet)
                {
                    HelmetSprite = equippedItem.Skin;

                }
                else if(equippedItem.WearableType == ObjectType.armor)
                {
                    ArmorSprite = equippedItem.Skin;
                }
                else if(equippedItem.WearableType == ObjectType.shoes)
                {
                    ShoesSprite = equippedItem.Skin;
                }
                else if(equippedItem.WearableType == ObjectType.Hand)
                {
                    HandSprite = equippedItem.Skin;
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
