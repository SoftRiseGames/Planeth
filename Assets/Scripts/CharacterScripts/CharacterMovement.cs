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
    public static Action CharacterReturnPosition;
    public static Action CharacterEndAction;

    //ardı ardına vurma mekaniği için !!!
    float Cooldown;
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
        var characterData = GetComponent<CharacterDataScripts>();
        if (characterData != null)
        {
            HorizontalMovementSpeed = characterData.HorizontalMovementSpeed;
            FinishCooldown = characterData.CooldownEnd;
            AttackSpeed = characterData.AttackSpeed;
        }
    }

    private void OnEnable()
    {
        if (!isStart)
            MissileLaunch.MissileTime += CharacterStart;
    }

    private void OnDisable()
    {
        CleanUpBeforeDestroy();
    }

    private void OnDestroy()
    {
        CleanUpBeforeDestroy();
    }

    private void CleanUpBeforeDestroy()
    {
        if (gameObject == null || !this) return; // Null güvenliği

        StopAllCoroutines();
        if (DOTween.IsTweening(this))
            DOTween.Kill(this);

        // Eventlerden güvenli çıkış
        if (Controlls.IsActionCharacter != null)
            Controlls.IsActionCharacter -= CharacterIsMove;

        if (Controlls.IsNonActionCharater != null)
            Controlls.IsNonActionCharater -= CharacterIsNonMove;

        if (CharacterDedectionControl.isEnemyCollide != null)
            CharacterDedectionControl.isEnemyCollide -= isPositionReset;

        if (MissileLaunch.MissileTime != null)
            MissileLaunch.MissileTime -= CharacterStart;

        if (CharacterZeroMovement != null)
            CharacterZeroMovement -= ZeroMovement;
    }

    private void Update()
    {
        if (gameObject == null || !this) return; // Null güvenliği

        CharacterMove();

        if (isStart && SpeedMeter > 0)
            SpeedMeter -= 1 * Time.deltaTime;
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

    void CharacterMove()
    {
        if (isMove)
        {
            if (!attackChecker)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = Vector2.MoveTowards(
                    new Vector2(transform.position.x, transform.position.y),
                    new Vector2(mousePosition.x, transform.position.y),
                    HorizontalMovementSpeed * Time.deltaTime
                );
            }
        }

        if (isAttack)
        {
            attackChecker = true;
            transform.position = new Vector2(
                transform.position.x,
                transform.position.y - AttackSpeed * Time.deltaTime
            );
        }
    }

    void CharacterPositionReset(float delayTime)
    {
        if (gameObject == null || !this) return; // Null güvenliği

        ObjectCollider.enabled = false;
        bool isMoveChecker = isMove;

        isReposition = true;
        CharacterZeroMovement?.Invoke();

        transform.DOMoveY(defaultYPosition, .3f)
            .SetEase(Ease.Linear)
            .OnUpdate(() => { Cooldown += Time.deltaTime; CharacterReturnPosition?.Invoke(); })
            .OnComplete(() =>
            {
                CharacterEndAction?.Invoke();
                StartCharacterFall();
                ObjectCollider.enabled = true;
                isReposition = false;
                attackChecker = false;

                if (isMoveChecker)
                    isMove = true;
            })
            .SetTarget(this);
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
        if (rb != null)
            rb.gravityScale = 3f;
    }

    void characterNonFall()
    {
        if (fallCoroutine != null)
        {
            StopCoroutine(fallCoroutine);
            fallCoroutine = null;
        }

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void CharacterStart()
    {
        transform.DOMoveY(4.24f, .5f).OnComplete(() =>
        {
            defaultYPosition = transform.position.y;
            StartGame();
        }).SetTarget(this);
    }

    async void StartGame()
    {
        await Task.Delay(2000);
        if (gameObject == null || !this) return; // Null güvenliği

        isStart = true;
        EventTrigger();
        EnemyComeAndGameStart?.Invoke();
        StartCharacterFall();
    }
}
