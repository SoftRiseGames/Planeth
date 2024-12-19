using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    // Kontrol bool'ları
    bool isMove;
    bool isAttack;
    bool isRepositioning;
    bool isReposition;
    bool attackChecker;
    bool isStart = false;
    float defaultYPosition;

    // Collider'lar
    Rigidbody2D rb;
    Coroutine fallCoroutine;
    BoxCollider2D ObjectCollider;

    // Event'ler
    public static Action CharacterZeroMovement;
    public static Action EnemyComeAndGameStart;
    public static Action CharacterReturnPosition;
    public static Action CharacterEndAction;

    // Ardışık vurma mekaniği için !!!
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
        HorizontalMovementSpeed = characterData.HorizontalMovementSpeed;
        FinishCooldown = characterData.CooldownEnd;
        AttackSpeed = characterData.AttackSpeed;
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
        {
            MissileLaunch.MissileTime += CharacterStart;
        }
    }

    private void OnDisable()
    {
        CleanUpEvents();
    }

    private void OnDestroy()
    {
        CleanUpEvents();
    }

    void CleanUpEvents()
    {
        StopAllCoroutines();
        DOTween.Kill(this, true);

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

    void CharacterIsMove()
    {
        if (this == null) return;
        if (!isReposition && !isAttack)
        {
            isMove = true;
            isAttack = false;
            StartCharacterFall();
        }
    }

    void CharacterIsNonMove()
    {
        if (this == null) return;
        if (!isReposition)
        {
            isMove = false;
            isAttack = true;
            characterNonFall();
        }
    }

    void ZeroMovement()
    {
        if (this == null) return;
        isAttack = false;
        isMove = false;
        characterNonFall();
    }

    private void Update()
    {
        if (this == null) return;
        CharacterMove();

        if (isStart && SpeedMeter > 0)
            SpeedMeter -= 1 * Time.deltaTime;
    }

    void CharacterMove()
    {
        if (this == null) return;
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
        if (this == null) return;

        ObjectCollider.enabled = false;
        bool isMoveChecker = isMove;
        isReposition = true;

        CharacterZeroMovement?.Invoke();
        gameObject.transform.DOMoveY(defaultYPosition, .3f)
            .SetEase(Ease.Linear)
            .OnUpdate(() => { Cooldown += Time.deltaTime; CharacterReturnPosition?.Invoke(); })
            .OnComplete(() =>
            {
                if (this == null) return;
                CharacterEndAction?.Invoke();
                StartCharacterFall();
                ObjectCollider.enabled = true;
                isReposition = false;
                attackChecker = false;
                isMove = isMoveChecker;
            })
            .SetTarget(this);
    }

    void isPositionReset()
    {
        if (this == null) return;
        CharacterPositionReset(.5f);
    }

    void StartCharacterFall()
    {
        if (this == null || fallCoroutine != null || !isStart) return;
        fallCoroutine = StartCoroutine(StartFallCoroutine());
    }

    IEnumerator StartFallCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        if (this == null) yield break;
        rb.gravityScale = 3f;
    }

    void characterNonFall()
    {
        if (this == null) return;
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
        if (this == null) return;
        gameObject.transform.DOMoveY(4.24f, .5f).OnComplete(() =>
        {
            if (this == null) return;
            defaultYPosition = gameObject.transform.position.y;
            StartGame();
        }).SetTarget(this);
    }

    async void StartGame()
    {
        if (this == null) return;
        await Task.Delay(2000);
        if (this == null) return;
        isStart = true;
        EventTrigger();
        EnemyComeAndGameStart?.Invoke();
        StartCharacterFall();
    }
}
