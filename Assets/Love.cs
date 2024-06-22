using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Love : MonoBehaviour
{
    private bool isLoveClick;

    private void OnEnable()
    {
        Controlls.isLoveClick += ClickCheck;
        Controlls.StopLoveClick += ClickStopCheck;
    }
    private void OnDisable()
    {
        Controlls.isLoveClick -= ClickCheck;
        Controlls.StopLoveClick -= ClickStopCheck;
    }
    private void OnMouseDrag()
    {
        if (isLoveClick)
            InvokeRepeating(nameof(LovePoint), 0f, 1f);
        else if (!isLoveClick)
            CancelInvoke(nameof(LovePoint));

    }
    private void OnMouseUp()
    {
        isLoveClick = false;
        CancelInvoke(nameof(LovePoint));
    }
    void ClickCheck() => isLoveClick = true;
    private void ClickStopCheck() => isLoveClick = false;
    private void LovePoint()
    {
        Debug.Log("love");
    }


}
