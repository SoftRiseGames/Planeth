using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    //Kontrol boolları
    bool isMove;
    bool isAttack;
    bool isRepositioning;
    bool isReposition;
    bool attackChecker;
    bool isStart = false;
    float defaultYPosition;

    //colliderler
    Rigidbody2D rb;
    Coroutine fallCoroutine;
    BoxCollider2D ObjectCollider;

    //Eventler
    public static Action CharacterZeroMovement;
    public static Action EnemyComeAndGameStart;

    //ardı ardına vurma mekaniği için !!!
    float Cooldown;
    
    //[SerializeField] float MinCooldown;
    float FinishCooldown;

    float HorizontalMovementSpeed;
    float AttackSpeed;
    

    public float SpeedMeter;
    private void Awake()
    {
        ObjectCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        HorizontalMovementSpeed = GetComponent<CharacterDataScripts>().HorizontalMovementSpeed;
        FinishCooldown = GetComponent<CharacterDataScripts>().CooldownEnd;
        AttackSpeed = GetComponent<CharacterDataScripts>().AttackSpeed;
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
        StopAllCoroutines(); 
        DOTween.Kill(this);   
        Controlls.IsActionCharacter -= CharacterIsMove;
        Controlls.IsNonActionCharater -= CharacterIsNonMove;
        CharacterDedectionControl.isEnemyCollide -= isPositionReset;
        MissileLaunch.MissileTime -= CharacterStart;
        CharacterZeroMovement -= ZeroMovement;
        MissileLaunch.MissileTime -= CharacterStart;
    }

    private void OnDestroy()
    {
        StopAllCoroutines(); 
        DOTween.Kill(this);   
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

        if (isStart && SpeedMeter > 0)
            SpeedMeter -= 1 * Time.deltaTime;
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
                    HorizontalMovementSpeed * Time.deltaTime
                );
            }
        }

        if (isAttack)
        {
            attackChecker = true;
            gameObject.transform.position = new Vector2(
                gameObject.transform.position.x,
                gameObject.transform.position.y - AttackSpeed * Time.deltaTime
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
        gameObject.transform.DOMoveY(defaultYPosition, .3f)
            .SetEase(Ease.Linear)
            .OnUpdate(() => { Cooldown += Time.deltaTime; })
            .OnComplete(() =>
            {
                StartCharacterFall();
                ObjectCollider.enabled = true;
                isReposition = false;
                attackChecker = false;

                if (isMoveChecker == true)
                    isMove = true;
            })
            .SetTarget(this); // Bu animasyonu CharacterMovement nesnesine ba�lar
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
        rb.linearVelocity = Vector2.zero;
    }

    void CharacterStart()
    {
        gameObject.transform.DOMoveY(4.24f, .5f).OnComplete(() =>
        {
            defaultYPosition = gameObject.transform.position.y;
            StartGame();
        }).SetTarget(this); 
    }

    async void StartGame()
    {
        await Task.Delay(2000);
        isStart = true;
        EventTrigger();
        EnemyComeAndGameStart?.Invoke();
        StartCharacterFall();
    }
}
