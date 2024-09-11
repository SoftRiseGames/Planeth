using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public enum EnemyType
{
    EnemyType1,
    EnemyType2
}
public class Enemies : MonoBehaviour
{
    [SerializeField] EnemyType enemyTypes;
    public bool isDamagable;

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
        if(enemyTypes == EnemyType.EnemyType2)
        {
            isDamagable = true;
            GetComponent<SpriteRenderer>().color = Color.white;
            await Task.Delay(3000);
            isDamagable = false;
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
    
}
