using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataScripts : MonoBehaviour
{
    
    
    public Sprite HelmetSprite;
    public Sprite ArmorSprite;
    public Sprite ShoesLSprite;
    public Sprite ShoesRSprite;
    public Sprite HandLSprite;
    public Sprite HandRSprite;
    public Sprite SwordSprite;

    public int ChanceToItemDrop;
    public int TotalHealth;
    public int MaxSpeed;
    public int MaxIncreaseSpeedMultiplier;
    public int AttackSpeed;
    public int DamagePower;
    public float HorizontalMovementSpeed;
    public float CooldownEnd;
    


    [SerializeField] EquippedItem EquippedItems;
    void Start()
    {
        
    }

   
}
