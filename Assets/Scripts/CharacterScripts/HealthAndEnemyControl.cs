using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class HealthAndEnemyControl : MonoBehaviour
{
    public List<Image> HealthObjects;
    private int enemyCount = 0;
    private int StaticEnemyCount = 0;
    int HealthControl = 2;
    public static Action IncreaseHealth;
    public static Action DecreaseHealth;
    bool LeastOneKill;

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
    [SerializeField] float minDistance = 1.5f;  // Minimum mesafe

    private List<Vector2> usedPositions = new List<Vector2>();

    private void OnEnable()
    {
        CharacterDedectionControl.isEnemyDecreasingOurhealth += DecrasingHealth;
        CharacterDedectionControl.isEnemyIncreasinghealth += IncreasingHealth;
        Enemies.isDeath += EnemyDecreaseCount;
        Enemies.isSpawn += EnemyStartCount;
    }

    private void OnDisable()
    {
        CharacterDedectionControl.isEnemyDecreasingOurhealth -= DecrasingHealth;
        CharacterDedectionControl.isEnemyIncreasinghealth -= IncreasingHealth;
        Enemies.isDeath -= EnemyDecreaseCount;
        Enemies.isSpawn -= EnemyStartCount;
    }

    void IncreasingHealth()
    {
        // Saðlýk artýrma iþlemi için gerekli kodlar
    }

    private void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
    }

    private void Update()
    {
        if ((enemyCount <= 0) || ((MaxEnemyCount-enemyCount<=2) && LeastOneKill == true))
        {
            StartCoroutine(EnemySpawnRoutine());
        }
        

    }

    void EnemyStartCount()
    {
        enemyCount += 1;
        StaticEnemyCount += 1;
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
        if (HealthControl >= 0)
        {
            HealthObjects[HealthControl].gameObject.SetActive(false);
            HealthControl -= 1;
        }
        else
            Debug.Log("Game Over");
    }

    IEnumerator EnemySpawnRoutine()
    {
        int HowManyEnemySpawn = 0;
        if (enemyCount<= 0)
             HowManyEnemySpawn = UnityEngine.Random.Range(MinEnemyCount, MaxEnemyCount + 1);
        else if(enemyCount<= 2 && enemyCount>0)
            HowManyEnemySpawn = UnityEngine.Random.Range(0, MaxEnemyCount-enemyCount);

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
                foreach (Vector2 usedPos in usedPositions)
                {
                    if (Vector2.Distance(usedPos, spawnPosition) < minDistance)
                    {
                        positionFound = false;
                        break;
                    }
                }
            }

            usedPositions.Add(spawnPosition);
            spawningGameobject.transform.DOMove(spawnPosition, .5f).SetEase(Ease.Flash);

            yield return new WaitForSeconds(0.5f); 
        }
    }
}
