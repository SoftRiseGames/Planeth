using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Controlls : MonoBehaviour
{
    RaycastHit2D CharacterHit;
    [SerializeField]LayerMask CharacterLayer;

   
    public static Action isPrevious;
    public static Action isNext;
    public static Action isLoveClick;
    public static Action StopLoveClick;
    private void Update()
    {
        RaycastCheck();
        CharacterLoveEventSender();
    }
    public void isPreviousControl() => isPrevious?.Invoke();
    public void isNextControl() => isNext?.Invoke();
    
    void RaycastCheck()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CharacterHit = Physics2D.Raycast(mousePosition, Vector3.forward, 50, CharacterLayer);
    }
    void CharacterLoveEventSender()
    {
        if (CharacterHit.collider != null)
            isLoveClick?.Invoke();

        if (CharacterHit.collider == null)
            StopLoveClick?.Invoke();

    }
   
   
   

}
