using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Controlls : MonoBehaviour
{
    public static Action isPrevious;
    public static Action isNext;
   
    public void isPreviousControl() => isPrevious?.Invoke();

    public void isNextControl()=> isNext?.Invoke();


}
