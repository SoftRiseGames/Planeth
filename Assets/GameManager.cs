using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CharacterDataScripts characterData;
    [SerializeField] SO_ValueMaker GameTotalCoin;
    [SerializeField] EquippedItem ItemHolder;
    [SerializeField] RocketDatas RocketData;
   
    private void Awake()
    {
       
    }
    private void Start()
    {
        ScriptableObjectDataManager.Instance.LoadEquippedData();
    }

    private void OnEnable()
    {
       
    }
    private void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.K))
            ScriptableObjectDataManager.Instance.DeleteAllJsonFiles();
        */
    }
    void LoadWearData()
    {
       
        foreach(So_Clothe_Settings equippedItem in ItemHolder.EquippedData)
        {
            Debug.Log("kontrol");
        }
        
    }

    void StartSprites()
    {
        foreach (So_Clothe_Settings equippedItem in ItemHolder.EquippedData)
        {
            if (equippedItem.isWear == true)
            {
                if (equippedItem.WearableType == ObjectType.Helmet)
                {
                    characterData.ChanceToItemDrop = equippedItem.ObjectDoubler;
                    characterData.HelmetSprite = equippedItem.HelmetWearSprite;
                }
                else if (equippedItem.WearableType == ObjectType.Armor)
                {
                    characterData.TotalHealth = equippedItem.ObjectDoubler;
                   characterData.ArmorSprite = equippedItem.ArmorWearSprite;
                }
                else if (equippedItem.WearableType == ObjectType.Shoes)
                {
                    characterData.MaxSpeed = equippedItem.ObjectDoubler;
                    characterData.ShoesSprite = equippedItem.ShoesWearSprite;
                }
                else if (equippedItem.WearableType == ObjectType.Glove)
                {
                    characterData.AttackSpeed = equippedItem.ObjectDoubler;
                    characterData.HandSprite = equippedItem.GloveWearSprite;
                }
                else if (equippedItem.WearableType == ObjectType.Sword)
                {
                    characterData.DamagePower = equippedItem.ObjectDoubler;
                    characterData.SwordSprite = equippedItem.SwordWearSprite;
                }
                else if(equippedItem.WearableType == ObjectType.Rocket)
                {
                    RocketData.Rockethandle = equippedItem.Rockethandle;
                    RocketData.RocketBackground = equippedItem.RocketBackground;
                    gameObject.GetComponent<MissileLaunch>().maxRocketSpeed = equippedItem.RocketMaxSpeed;

                }
            }

        }
    }

}
