using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class MissileLaunch : MonoBehaviour
{
    
    [SerializeField] Slider StartMissile;
    private float StartMissileValues;
    private bool isGameStart;
    private float missileValue;
    private Sequence missileSequence;

    [SerializeField] Image Background;
    [SerializeField] Image Handle;

    public static Action MissileTime;

    public float maxRocketSpeed;

    public CharacterMovement Character;
    void Start()
    {
        isGameStart = false;
        StartMissile.value = StartMissileValues;
        MissileSliderLoop();
    }

    private void OnEnable()
    {
        Controlls.Launch += Missile;
    }

    private void OnDisable()
    {
        Controlls.Launch -= Missile;
    }

    void Update()
    {
        StartMissile.value = StartMissileValues;
    }
    void AfterMissile()
    {
        Handle.DOFade(0, .5f);
        Background.DOFade(0, .5f);
    }
    void Missile()
    {
        missileValue = StartMissile.value*maxRocketSpeed;
        Character.SpeedMeter = missileValue;
        Debug.Log(missileValue);
        MissileTime?.Invoke();
        isGameStart = true;
        
        
        AfterMissile();
        
        if (missileSequence != null)
        {
            missileSequence.Kill();
        }
    }

    void MissileSliderLoop()
    {
        if (!isGameStart)
        {
            missileSequence = DOTween.Sequence(); 

            missileSequence.Append(DOTween.To(() => StartMissileValues, x => StartMissileValues = x, 1, .7f).OnUpdate(() =>
            {
                StartMissile.value = StartMissileValues;
            }).SetEase(Ease.Linear));

            missileSequence.Append(DOTween.To(() => StartMissileValues, x => StartMissileValues = x, 0, 0.5f).OnUpdate(() =>
            {
                StartMissile.value = StartMissileValues;
            }).SetEase(Ease.Linear));

            
            missileSequence.SetLoops(-1).SetEase(Ease.Linear);
        }
    }
}
