using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthAndEnemyControl : MonoBehaviour
{
    public List<GameObject> HealthObjects;
    private int enemyCount = 0;
    int HealthControl = 2;
    public static Action IncreaseHealth;
    public static Action DecreaseHealth;
    
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
   
}
