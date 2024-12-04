using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] CharacterDataScripts characterData;
    [SerializeField] SO_ValueMaker GameTotalCoin;
    [SerializeField] EquippedItem ItemHolder;
    [SerializeField] RocketDatas RocketData;
    [SerializeField] TextMeshProUGUI DistanceMeterText;
    [SerializeField] CharacterMovement Character;
    [SerializeField] HealthAndEnemyControl totalHealth;
    private int DistanceValue;
   
    private void Awake()
    {
       
    }
    private void Start()
    {
        //StartOptions();
        ScriptableObjectDataManager.Instance.LoadEquippedData();
        Application.targetFrameRate = 60;
        
    }

    
    private void Update()
    {
        DistanceInformation();
    }
    void DistanceInformation()
    {
        if (Character.SpeedMeter >= 10)
            DistanceMeterText.text = "Distance " + Character.SpeedMeter.ToString("00");
        else if(Character.SpeedMeter<10)
            DistanceMeterText.text = "Distance " + Character.SpeedMeter.ToString("0");

    }

    void StartOptions()
    {
        foreach (So_Clothe_Settings equippedItem in ItemHolder.EquippedData)
        {
            if (equippedItem.isWear == true)
            {
                if (equippedItem.WearableType == ObjectType.Helmet)
                {
                    characterData.ChanceToItemDrop = equippedItem.ObjectMainFeatureValue;
                    characterData.HelmetSprite = equippedItem.HelmetWearSprite;
                }
                else if (equippedItem.WearableType == ObjectType.Armor)
                {
                    totalHealth.TotalHealth = equippedItem.ObjectMainFeatureValue;
                   characterData.ArmorSprite = equippedItem.ArmorWearSprite;
                }
                else if (equippedItem.WearableType == ObjectType.Shoes)
                {
                    characterData.MaxSpeed = equippedItem.ObjectMainFeatureValue;
                    characterData.MaxIncreaseSpeedMultiplier = equippedItem.MaxSpeedIncreaseValue;
                    characterData.ShoesLSprite = equippedItem.ShoesLWearSprite;
                    characterData.ShoesRSprite = equippedItem.ShoesRWearSprite;

                }
                else if (equippedItem.WearableType == ObjectType.Glove)
                {
                    characterData.AttackSpeed = equippedItem.ObjectMainFeatureValue;
                    characterData.HandLSprite = equippedItem.GloveLWearSprite;
                    characterData.HandRSprite = equippedItem.GloveRWearSprite;
                }
                else if (equippedItem.WearableType == ObjectType.Sword)
                {
                    characterData.DamagePower = equippedItem.ObjectMainFeatureValue;
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
