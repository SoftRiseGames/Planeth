using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class Enemies : MonoBehaviour
{
    public SO_Enemytypes enemies;
    public bool isHasSpike;
    public static Action isSpawn;
    public static Action isDeath;
    public int health;
    public bool isDamagable;
    public Rigidbody2D rb;

    float speedX;
    float speedY;

    float FallTimer;
    float FallSpeed;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isSpawn?.Invoke();
        CharacterClasses();
        gameObject.GetComponent<SpriteRenderer>().sprite = enemies.enemySprite;

        if (enemies.enemy == EnemyType.GuardedAndHasSpikes || enemies.enemy == EnemyType.OnlyGuarded)
            InvokeRepeating("EnemyWaitStatus", 0, 6);

        StartMovement();

        this.gameObject.transform.localScale = new Vector2(enemies.EnemyScale, enemies.EnemyScale);
    }

    void CharacterClasses()
    {
        health = enemies.EnemyHealth;
        FallTimer = enemies.enemyFallTimer;
        FallSpeed = enemies.EnemyFallSpeed;
    }

    private void OnEnable()
    {
        CharacterDedectionControl.isEnemysDecreasingHealth += DecreaseHealth;
    }

    private void OnDisable()
    {
        CharacterDedectionControl.isEnemysDecreasingHealth -= DecreaseHealth;
    }
    private void OnDestroy()
    {
        DOTween.Kill(this);
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
        {
            StartCoroutine(EnemyDamagableModeWait(3));
        }
    }

    void StartMovement()
    {
        int MovementPositionXRandomizer = UnityEngine.Random.Range(0, 2);
        int MovementPositionYRandomizer = UnityEngine.Random.Range(0, 2);

        if (MovementPositionXRandomizer == 0)
            speedX = enemies.SpeedForXaxis;
        else if (MovementPositionXRandomizer == 1)
            speedX = enemies.SpeedForXaxis * -1;

        rb.linearVelocity = new Vector2(speedX, 0) * Time.fixedDeltaTime;
    }

    private void Update()
    {
        FallTimerDecrease();

        
        if (health <= 0)
        {
            Kill();
        }

        
        if (FallTimer <= 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -FallSpeed * Time.fixedDeltaTime);
        }
    }
    void Kill()
    {
        isDeath?.Invoke();
        Destroy(this.gameObject);
    }
    void FallTimerDecrease()
    {
        FallTimer -= Time.deltaTime;
    }

    void DecreaseHealth()
    {
        health -= 1;
    }

    IEnumerator AutoKill()
    {
        yield return new WaitForSeconds(3.5f);
        Kill();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "killer")
        {
            Kill();
        }
    }
}
