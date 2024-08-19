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
    public static Action IsActionCharacter;
    public static Action IsNonActionCharater;
   
    void MouseZeroActions()
    {
        if(Input.GetMouseButtonDown(0))
            IsActionCharacter?.Invoke();
        else if(Input.GetMouseButtonUp(0))
            IsNonActionCharater?.Invoke();;
    }

    private void Update()
    {
        RaycastCheck();
        MouseZeroActions();
    }
    public void isPreviousControl() => isPrevious?.Invoke();
    public void isNextControl() => isNext?.Invoke();
    
    void RaycastCheck()
    {
        /*
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CharacterHit = Physics2D.Raycast(mousePosition, Vector3.forward, 50, CharacterLayer);
        */
    }
    

}
