using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using DG.Tweening;
public class CharacterMovement : MonoBehaviour
{
    bool isMove;
    bool isAttack;
    bool isRepositioning;
    bool isReposition;
    bool attackChecker;
    float defaultYPosition;

    public static Action CharacterZeroMovement;
    private void Start()
    {
        defaultYPosition = gameObject.transform.position.y;
        Debug.Log(defaultYPosition);
    }
    private void OnEnable()
    {
        Controlls.IsActionCharacter += CharacterIsMove;
        Controlls.IsNonActionCharater += CharacterIsNonMove;
        CharacterZeroMovement += ZeroMovement;
    }
    private void OnDisable()
    {
        Controlls.IsActionCharacter -= CharacterIsMove;
        Controlls.IsNonActionCharater -= CharacterIsNonMove;
        CharacterZeroMovement -= ZeroMovement;
    }

    void CharacterIsMove()
    {
        if (!isReposition && !isAttack) 
        {
            isMove = true;
            isAttack = false;
        }
    }

    void CharacterIsNonMove()
    {
        if (!isReposition && !isAttack) 
        {
            isMove = false;
            isAttack = true;
        }
    }
    void ZeroMovement()
    {
        isAttack = false;
        isMove = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name =="enemy")
        {
            Debug.Log("collide");
            CharacterPositionReset(500);
        }
    }
    private void Update()
    {
        CharacterMove();
    }
   
    void CharacterMove()
    {
        if (isMove)
        {
            if (!attackChecker)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                gameObject.transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(mousePosition.x, transform.position.y), 8 * Time.deltaTime);
            }
        }
        if (isAttack)
        {
            attackChecker = true;
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y-29*Time.deltaTime);
        }
        Debug.Log(attackChecker);

    }
    
    async void CharacterPositionReset(int delayTime)
    {
        isReposition = true;
        CharacterZeroMovement?.Invoke();
        gameObject.transform.DOMoveY(defaultYPosition, .5f);
        await Task.Delay(delayTime);
        isReposition = false;
        attackChecker = false;
        
    }
    

}
