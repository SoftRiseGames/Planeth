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
    public Rigidbody2D rb;

    float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isSpawn?.Invoke();
        
        gameObject.GetComponent<SpriteRenderer>().sprite = enemies.enemySprite;
        health = enemies.EnemyHealth;

        if (enemies.enemy == EnemyType.GuardedAndHasSpikes || enemies.enemy == EnemyType.OnlyGuarded)
            InvokeRepeating("EnemyWaitStatus", 0, 6);

        StartMovement();
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

    void StartMovement()
    {
        int MovementPositionRandomizer = UnityEngine.Random.Range(0, 2);

        if (MovementPositionRandomizer == 0)
            speed = enemies.SpeedForXaxis;
        else if (MovementPositionRandomizer == 1)
            speed = enemies.SpeedForXaxis * -1;
        rb.velocity = new Vector2(speed, 0)*Time.fixedDeltaTime;
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
