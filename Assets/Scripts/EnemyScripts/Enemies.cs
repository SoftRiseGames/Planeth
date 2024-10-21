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
    private int health;

    private void Start()
    {
        isSpawn?.Invoke();
        gameObject.GetComponent<SpriteRenderer>().sprite = enemies.enemySprite;
        health = enemies.EnemyHealth;
    }
    private void OnEnable()
    {
        InvokeRepeating("EnemyWaitStatus", 0, 6);
    }
    private void OnDisable()
    {
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
    
}
