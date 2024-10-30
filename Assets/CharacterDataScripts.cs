using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataScripts : MonoBehaviour
{
    
    
    public Sprite HelmetSprite;
    public Sprite ArmorSprite;
    public Sprite ShoesSprite;
    public Sprite HandSprite;
    public Sprite SwordSprite;

    public int ChanceToItemDrop;
    public int TotalHealth;
    public int MaxSpeed;
    public int AttackSpeed;
    public int DamagePower;



    [SerializeField] EquippedItem EquippedItems;
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
                    ChanceToItemDrop = equippedItem.ObjectDoubler;
                    HelmetSprite = equippedItem.Skin;
                }
                else if(equippedItem.WearableType == ObjectType.Armor)
                {
                    TotalHealth = equippedItem.ObjectDoubler;
                    ArmorSprite = equippedItem.Skin;
                }
                else if(equippedItem.WearableType == ObjectType.Shoes)
                {
                    MaxSpeed = equippedItem.ObjectDoubler;
                    ShoesSprite = equippedItem.Skin;
                }
                else if(equippedItem.WearableType == ObjectType.Glove)
                {
                    AttackSpeed = equippedItem.ObjectDoubler;
                    HandSprite = equippedItem.Skin;
                }
                else if(equippedItem.WearableType == ObjectType.Sword)
                {
                    DamagePower = equippedItem.ObjectDoubler;
                    SwordSprite = equippedItem.Skin;
                }
            }
            
        }
    }
}
