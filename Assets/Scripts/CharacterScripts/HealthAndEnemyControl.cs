using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndEnemyControl : MonoBehaviour
{
    public List<Image> HealthObjects;
    private int enemyCount = 0;
    public int TotalHealth;
    public static Action IncreaseHealth;
    public static Action DecreaseHealth;
    

    [Header("Enemy Setup")]
    [SerializeField] List<SO_Enemytypes> enemytypes;
    [SerializeField] List<Transform> PositionPoint;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] GameObject enemyObject;

    [Header("Enemy Spawn Options")]
    [SerializeField] int MinEnemyCount;
    [SerializeField] int MaxEnemyCount;

    [Header("Spawn Position MinMax Randomizer")]
    [SerializeField] float MaxExtraVertical;
    [SerializeField] float minDistance = 1.5f;  

    private List<Vector2> usedPositions;

    bool isStart;
    
    private void OnEnable()
    {
        CharacterDedectionControl.isEnemyDecreasingOurhealth += DecrasingHealth;
        CharacterDedectionControl.isEnemyIncreasinghealth += IncreasingHealth;
        Enemies.isDeath += EnemyDecreaseCount;
        Enemies.isSpawn += EnemyStartCount;
        CharacterMovement.EnemyComeAndGameStart += StartActivate;
    }
    void StartActivate()
    {
        isStart = true;
    }
    private void OnDisable()
    {
        CharacterDedectionControl.isEnemyDecreasingOurhealth -= DecrasingHealth;
        CharacterDedectionControl.isEnemyIncreasinghealth -= IncreasingHealth;
        Enemies.isDeath -= EnemyDecreaseCount;
        Enemies.isSpawn -= EnemyStartCount;
        CharacterMovement.EnemyComeAndGameStart += StartActivate;
    }

    void IncreasingHealth()
    {
        // Sa�l�k art�rma i�lemi i�in gerekli kodlar
    }

    private void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        HealthStart();
        
    }
    void HealthStart()
    {
        for(int i= 0; i<HealthObjects.Count; i++)
        {
            
            if (i < TotalHealth)
                HealthObjects[i].gameObject.SetActive(true);
            else
                return;
        }
    }
    private void Update()
    {
        if (enemyCount <= 0)
        {
            StartCoroutine(EnemySpawnRoutine());
        }
        

    }

    void EnemyStartCount()
    {
        enemyCount += 1;

    }

    void EnemyDecreaseCount()
    {
        enemyCount -= 1;
        RandomBeforeZeroEnemy();
    }

    void RandomBeforeZeroEnemy()
    {
        int RandomBeforeEnemy = UnityEngine.Random.Range(0, 101);

        if (RandomBeforeEnemy > 50)
            StartCoroutine(EnemySpawnRoutine());
    }

    void DecrasingHealth()
    {
        if (TotalHealth > 0)
        {
            TotalHealth -= 1;
            HealthObjects[TotalHealth].gameObject.SetActive(false);
            
        }
        else
            Debug.Log("Game Over");
    }

    IEnumerator EnemySpawnRoutine()
    {
        if (isStart)
        {
            int HowManyEnemySpawn = 0;
            if (enemyCount <= 0)
                HowManyEnemySpawn = UnityEngine.Random.Range(MinEnemyCount, MaxEnemyCount + 1);
            else if (enemyCount <= UnityEngine.Random.Range(2, 5) && enemyCount > 0)
                HowManyEnemySpawn = UnityEngine.Random.Range(0, MaxEnemyCount - enemyCount);
            
            if(usedPositions != null)
                usedPositions.Clear();


            for (int i = 0; i < HowManyEnemySpawn; i++)
            {
                GameObject spawningGameobject = Instantiate(enemyObject, SpawnPoint.position, Quaternion.identity);

                int RandomEnemyType = UnityEngine.Random.Range(0, enemytypes.Count);
                spawningGameobject.GetComponent<Enemies>().enemies = enemytypes[RandomEnemyType];

                Vector2 spawnPosition = SpawnPoint.position;
                bool positionFound = false;

                while (!positionFound)
                {
                    int RandomPositioner = UnityEngine.Random.Range(0, PositionPoint.Count);
                    spawnPosition = new Vector2(
                        PositionPoint[RandomPositioner].position.x,
                        UnityEngine.Random.Range(PositionPoint[RandomPositioner].position.y, PositionPoint[RandomPositioner].position.y + MaxExtraVertical)
                    );

                    positionFound = true;

                    if(usedPositions != null)
                    {
                        foreach (Vector2 usedPos in usedPositions)
                        {
                            if (Vector2.Distance(usedPos, spawnPosition) < minDistance)
                            {
                                positionFound = false;
                                break;
                            }
                        }
                    }
                    
                }

                if(usedPositions != null)
                    usedPositions.Add(spawnPosition);

                spawningGameobject.transform.DOMove(spawnPosition, .5f).SetEase(Ease.Flash);

                yield return new WaitForSeconds(0.5f);
            }
        
        }
    }
}
