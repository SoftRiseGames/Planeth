using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthSystem : MonoBehaviour
{
    public List<GameObject> HealthObjects;

    public static Action IncreaseHealth;
    public static Action DecreaseHealth;
    int HealthControl = 2;
    private void OnEnable()
    {
        CharacterDedectionControl.isEnemyDecreasinghealth += DecrasingHealth;
        CharacterDedectionControl.isEnemyIncreasinghealth += IncreasingHealth;
    }
    private void OnDisable()
    {
        CharacterDedectionControl.isEnemyDecreasinghealth -= DecrasingHealth;
        CharacterDedectionControl.isEnemyIncreasinghealth -= IncreasingHealth;
    }
    void IncreasingHealth()
    {

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
