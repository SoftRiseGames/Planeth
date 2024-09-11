using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterDedectionControl : MonoBehaviour
{
    public static Action isEnemyCollide;
    public static Action isEnemyDecreasinghealth;
    public static Action isEnemyIncreasinghealth;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "enemy")
        {
            isEnemyCollide?.Invoke();
            if (collision.gameObject.GetComponent<Enemies>().isDamagable == false)
                isEnemyDecreasinghealth?.Invoke();
            else
                return;
        }
    }
}
