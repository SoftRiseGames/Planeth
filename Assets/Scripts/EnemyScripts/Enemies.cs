using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Enemies : MonoBehaviour
{
    [SerializeField] SO_Enemytypes enemies; 
    public bool isDamagable;
    public static Action isSpawn;
    public static Action isDeath;
    public int health;

    private void Start()
    {
        isSpawn?.Invoke();
        
        gameObject.GetComponent<SpriteRenderer>().sprite = enemies.enemySprite;
        health = enemies.EnemyHealth;
    }
    private void OnEnable()
    {
        CharacterDedectionControl.isEnemysDecreasingHealth += DecreaseHealth;
        InvokeRepeating("EnemyWaitStatus", 0, 6);
    }
    private void OnDisable()
    {
        CharacterDedectionControl.isEnemysDecreasingHealth -= DecreaseHealth;
        CancelInvoke("EnemyWaitStatus");
    }
    async void EnemyWaitStatus()
    {
        if(enemies.enemy == EnemyType.EnemyType2)
        {
            isDamagable = true;
            GetComponent<SpriteRenderer>().color = Color.white;
            await Task.Delay(3000);
            isDamagable = false;
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
    private void Update()
    {
        if(health <= 0)
        {
            isDeath?.Invoke();
            Destroy(this.gameObject);
        }
            

    }
    void DecreaseHealth()
    {
        health = health - 1;
    }
}
