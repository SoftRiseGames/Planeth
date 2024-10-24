using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class HealthAndEnemyControl : MonoBehaviour
{
    public List<GameObject> HealthObjects;
    private int enemyCount = 0;
    int HealthControl = 2;
    public static Action IncreaseHealth;
    public static Action DecreaseHealth;

    [Header("Enemy Setup")]
    [SerializeField] List<SO_Enemytypes> enemytypes;
    [SerializeField] List<Transform> PositionPoint;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] GameObject enemyObject;

    [Header("Enemy Spawn Options")]
    [SerializeField] int MinEnemyCount;

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

    }
    private void Start()
    {
        EnemyReposition();
    }
    private void Update()
    {
        if (enemyCount <= 0)
            EnemyReposition();

    }
    void EnemyStartCount()
    {
        enemyCount = enemyCount + 1;
    }
    void EnemyDecreaseCount()
    {
        enemyCount = enemyCount - 1;
    }
    void DecrasingHealth()
    {
        if (HealthControl >= 0)
        {
            HealthObjects[HealthControl].gameObject.SetActive(false);
            HealthControl = HealthControl - 1;
        }
        else
            Debug.Log("game Over");
    }
    void EnemyReposition()
    {
        int HowManyEnemySpawn = UnityEngine.Random.Range(MinEnemyCount, PositionPoint.Count);
        for (int i = 0; i < HowManyEnemySpawn; i++)
        {
            GameObject spawningGameobject = Instantiate(enemyObject, new Vector2(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y), Quaternion.identity); 
            int RandomEnemyType = UnityEngine.Random.Range(0, enemytypes.Count);
            spawningGameobject.GetComponent<Enemies>().enemies = enemytypes[RandomEnemyType];
            spawningGameobject.transform.DOMove(new Vector2(PositionPoint[i].transform.position.x,PositionPoint[i].transform.position.y), 1);

        }
    }
}
