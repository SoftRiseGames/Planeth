using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MissileLaunch : MonoBehaviour
{
    [SerializeField] Slider StartMissile;
    private float StartMissileValues;
    private bool isGameStart;
    private float missileValue;
    private Sequence missileSequence; // Sequence referans�n� saklamak i�in

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

    void Missile()
    {
        missileValue = StartMissile.value;
        isGameStart = true;
        Debug.Log("missile!!!");
        Debug.Log(isGameStart);

        // Sequence'i durdur
        if (missileSequence != null)
        {
            missileSequence.Kill();
        }
    }

    void MissileSliderLoop()
    {
        if (!isGameStart)
        {
            missileSequence = DOTween.Sequence(); // Sequence referans� olu�tur

            missileSequence.Append(DOTween.To(() => StartMissileValues, x => StartMissileValues = x, 1, .7f).OnUpdate(() =>
            {
                StartMissile.value = StartMissileValues;
            }).SetEase(Ease.Linear));

            missileSequence.Append(DOTween.To(() => StartMissileValues, x => StartMissileValues = x, 0, 0.5f).OnUpdate(() =>
            {
                StartMissile.value = StartMissileValues;
            }).SetEase(Ease.Linear));

            // Sonsuz d�ng� olarak ayarla
            missileSequence.SetLoops(-1).SetEase(Ease.Linear);
        }
    }
}
