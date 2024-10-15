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
    Coroutine fallCoroutine; 
    public static Action CharacterZeroMovement;

    private void Start()
    {
        defaultYPosition = gameObject.transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        StartCharacterFall(); 
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
            StartCharacterFall(); 
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
                gameObject.transform.position = Vector2.MoveTowards(
                    new Vector2(transform.position.x, gameObject.transform.position.y),
                    new Vector2(mousePosition.x, gameObject.transform.position.y),
                    1 * Time.fixedDeltaTime
                );
            }
        }

        if (isAttack)
        {
            attackChecker = true;
            gameObject.transform.position = new Vector2(
                gameObject.transform.position.x,
                gameObject.transform.position.y - 7 * Time.fixedDeltaTime
            );
        }
    }

    async void CharacterPositionReset(int delayTime)
    {
        isReposition = true;
        CharacterZeroMovement?.Invoke();
        gameObject.transform.DOMoveY(defaultYPosition, .5f).OnComplete(() => StartCharacterFall());
        await Task.Delay(delayTime);
        isReposition = false;
        attackChecker = false;
    }

    void isPositionReset()
    {
        CharacterPositionReset(500);
    }

    void StartCharacterFall()
    {
        if (fallCoroutine == null)
        {
            fallCoroutine = StartCoroutine(StartFallCoroutine());
        }
    }

    IEnumerator StartFallCoroutine()
    {
        yield return new WaitForSeconds(3f);
        rb.gravityScale = 0.5f;
    }

    void characterNonFall()
    {
        if (fallCoroutine != null)
        {
            StopCoroutine(fallCoroutine);
            fallCoroutine = null; 
        }
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero; 
    }
}
