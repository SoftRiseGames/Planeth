using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CharacterDedectionControl : MonoBehaviour
{
    public static Action isEnemyCollide;
    public static Action isEnemyDecreasingOurhealth;
    public static Action isEnemyIncreasinghealth;
    public static Action isEnemysDecreasingHealth;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy" || collision.gameObject.name == "fallground")
        {
            isEnemyCollide?.Invoke();
            if(collision.gameObject.tag == "enemy")
            {
                if (collision.gameObject.GetComponent<Enemies>().isDamagable == false)
                    isEnemyDecreasingOurhealth?.Invoke();
                else if (collision.gameObject.GetComponent<Enemies>().isDamagable == true)
                    collision.gameObject.GetComponent<Enemies>().health = collision.gameObject.GetComponent<Enemies>().health - GetComponent<CharacterDataScripts>().DamagePower;
            }
          
        }
    }
}
