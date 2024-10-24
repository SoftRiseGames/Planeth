using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Enemies : MonoBehaviour
{
    public SO_Enemytypes enemies; 
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
        /*
        if (enemies.enemy == EnemyType.EnemyType2)
            InvokeRepeating("EnemyWaitStatus", 0, 6);
        else
            return;
        */

    }
    private void OnDisable()
    {
        CharacterDedectionControl.isEnemysDecreasingHealth -= DecreaseHealth;

        /*
        if (enemies.enemy == EnemyType.EnemyType2)
            CancelInvoke("EnemyWaitStatus");
        else
            return;
        */
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
            Debug.Log("zero");
            Destroy(this.gameObject);
        }
            

    }
    void DecreaseHealth()
    {
        health = health - 1;
    }
}
