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
    [SerializeField] int MaxEnemyCount;
    [Header("Spawn Position MinMax Randomizer")]
    [SerializeField] float MaxExtraVertical;

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
        int HowManyEnemySpawn = UnityEngine.Random.Range(MinEnemyCount, MaxEnemyCount+1);
        for (int i = 0; i < HowManyEnemySpawn; i++)
        {
            GameObject spawningGameobject = Instantiate(enemyObject, new Vector2(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y), Quaternion.identity); 
            int RandomEnemyType = UnityEngine.Random.Range(0, enemytypes.Count);
            spawningGameobject.GetComponent<Enemies>().enemies = enemytypes[RandomEnemyType];
            int RandomPositioner = UnityEngine.Random.Range(0, PositionPoint.Count);
            spawningGameobject.transform.DOMove(new Vector2( PositionPoint[RandomPositioner].transform.position.x,UnityEngine.Random.Range(PositionPoint[RandomPositioner].transform.position.y, PositionPoint[RandomPositioner].transform.position.y+MaxExtraVertical)), 1);

        }
    }
}
