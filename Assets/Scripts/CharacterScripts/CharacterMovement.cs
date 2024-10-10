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
    Rigidbody2D rb;
    public static Action CharacterZeroMovement;
    private void Start()
    {
        defaultYPosition = gameObject.transform.position.y;
        CharacterFall();
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void OnEnable()
    {
        Controlls.IsActionCharacter += CharacterIsMove;
        Controlls.IsNonActionCharater += CharacterIsNonMove;
        CharacterDedectionControl.isEnemyCollide += isPositionReset;
        CharacterZeroMovement += ZeroMovement;
    }
    private void OnDisable()
    {
        Controlls.IsActionCharacter -= CharacterIsMove;
        Controlls.IsNonActionCharater -= CharacterIsNonMove;
        CharacterDedectionControl.isEnemyCollide -= isPositionReset;
        CharacterZeroMovement -= ZeroMovement;
       
    }

    void CharacterIsMove()
    {
        if (!isReposition && !isAttack) 
        {
            isMove = true;
            isAttack = false;
            CharacterFall();
        }
    }

    void CharacterIsNonMove()
    {
        if (!isReposition) 
        {
            isMove = false;
            isAttack = true;
            characterNonFall();
        }
    }
    void ZeroMovement()
    {
        isAttack = false;
        isMove = false;
        characterNonFall();
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
                gameObject.transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, gameObject.transform.position.y), new Vector2(mousePosition.x, gameObject.transform.position.y), 1 * Time.fixedDeltaTime);
            }
        }
        if (isAttack)
        {
            attackChecker = true;
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 7* Time.fixedDeltaTime);
        }
    }
    
    async void CharacterPositionReset(int delayTime)
    {
        isReposition = true;
        CharacterZeroMovement?.Invoke();
        gameObject.transform.DOMoveY(defaultYPosition, .5f);
        await Task.Delay(delayTime);
        isReposition = false;
        attackChecker = false;
        characterNonFall();
        CharacterFall();
    }
    void isPositionReset()
    {
        CharacterPositionReset(500);
    }

    async void CharacterFall()
    {
        await Task.Delay(3000);
        rb.gravityScale = .2f;
    }
    void characterNonFall()
    {
        rb.gravityScale = 0;
    }
}
