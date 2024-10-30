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
    BoxCollider2D ObjectCollider;
    public static Action CharacterZeroMovement;
    float Cooldown;
    [SerializeField] float MinCooldown;
    [SerializeField] float MaxCooldown;
    bool isStart = false;
    public static Action EnemyCome;
    private float StartSpeed;
    private void Start()
    {

        ObjectCollider = GetComponent<BoxCollider2D>();
        
        rb = GetComponent<Rigidbody2D>();
        StartCharacterFall();

    }
    void EventTrigger()
    {
        if (isStart)
        {
            Controlls.IsActionCharacter += CharacterIsMove;
            Controlls.IsNonActionCharater += CharacterIsNonMove;
            CharacterDedectionControl.isEnemyCollide += isPositionReset;
            CharacterZeroMovement += ZeroMovement;
        }
    }
    private void OnEnable()
    {
         if (!isStart)
            MissileLaunch.MissileTime += CharacterStart;
    }

    private void OnDisable()
    {
        Controlls.IsActionCharacter -= CharacterIsMove;
        Controlls.IsNonActionCharater -= CharacterIsNonMove;
        CharacterDedectionControl.isEnemyCollide -= isPositionReset;
        MissileLaunch.MissileTime -= CharacterStart;
        CharacterZeroMovement -= ZeroMovement;
        MissileLaunch.MissileTime -= CharacterStart;
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
        if (!isReposition ||(Cooldown> MinCooldown && Cooldown<MaxCooldown))
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

    void CharacterPositionReset(float delayTime)
    {
        ObjectCollider.enabled = false;
        bool isMoveChecker = false;
        isReposition = true;

        if (isMove == true)
            isMoveChecker = true;
        else
            isMoveChecker = false;

        CharacterZeroMovement?.Invoke();
        gameObject.transform.DOMoveY(defaultYPosition, .3f).OnUpdate(() => { Cooldown += Time.deltaTime; }).OnComplete(() =>
        {
            StartCharacterFall();
            ObjectCollider.enabled = true;
            isReposition = false;
            attackChecker = false;

            if (isMoveChecker == true)
                isMove = true;
        }).SetEase(Ease.Linear);

    }

    void isPositionReset()
    {
        CharacterPositionReset(.5f);
    }

    void StartCharacterFall()
    {
        if (fallCoroutine == null && isStart)
        {
            fallCoroutine = StartCoroutine(StartFallCoroutine());
        }
    }

    IEnumerator StartFallCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        rb.gravityScale = 3f;
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

    void CharacterStart()
    {
        Debug.Log("missile");
        gameObject.transform.DOMoveY(4.24f, .5f).OnComplete(() => { isStart = true; EventTrigger(); defaultYPosition = gameObject.transform.position.y; EnemyCome?.Invoke(); });
       
    }
}