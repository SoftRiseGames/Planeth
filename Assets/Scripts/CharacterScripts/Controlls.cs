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
    public static Action CharacterZeroMovement;

    public static Action Launch;

    bool isStart;
   
    void MouseZeroActions()
    {
        if (Input.GetMouseButtonDown(0))
            IsActionCharacter?.Invoke();
        else if (Input.GetMouseButtonUp(0))
            IsNonActionCharater?.Invoke();
    }
    void mouseLaunchAction()
    {
        if (Input.GetMouseButtonDown(0))
            Launch?.Invoke();
        else if (Input.GetMouseButtonUp(0))
            isStart = true;
    }


    private void Update()
    {
        RaycastCheck();
        
        
        if (isStart)
            MouseZeroActions();
        else if (!isStart)
            mouseLaunchAction();

    }
   
    void RaycastCheck()
    {
        /*
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CharacterHit = Physics2D.Raycast(mousePosition, Vector3.forward, 50, CharacterLayer);
        */
    }
    

}
