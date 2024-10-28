using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Enemies : MonoBehaviour
{
    public SO_Enemytypes enemies; 
    public bool isHasSpike;
    public static Action isSpawn;
    public static Action isDeath;
    public int health;
    public bool isDamagable;
    private void Start()
    {
        isSpawn?.Invoke();
        
        gameObject.GetComponent<SpriteRenderer>().sprite = enemies.enemySprite;
        health = enemies.EnemyHealth;

        if (enemies.enemy == EnemyType.GuardedAndHasSpikes || enemies.enemy == EnemyType.OnlyGuarded)
            InvokeRepeating("EnemyWaitStatus", 0, 6);
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
        
    }

    IEnumerator EnemySpikeModeWait(int delay)
    {
        isHasSpike = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(delay);
        isHasSpike = true;
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    IEnumerator EnemyDamagableModeWait(int delay)
    {
        isDamagable = true;
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(delay);
        isDamagable = false;
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    
     void EnemyWaitStatus()
    {
        if (enemies.enemy == EnemyType.GuardedAndHasSpikes)
        {
            StartCoroutine(EnemyDamagableModeWait(3));
            StartCoroutine(EnemySpikeModeWait(3));
        }
        else if (enemies.enemy == EnemyType.OnlyGuarded)
            StartCoroutine(EnemyDamagableModeWait(3));
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
