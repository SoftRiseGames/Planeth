using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Love : MonoBehaviour
{
    private Vector3 lastMousePosition;

    private bool isLoveClick;
    private bool isMouseMoving;
    
    [SerializeField] float threshold;
    private void Update()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        if (Mathf.Abs(currentMousePosition.x - lastMousePosition.x) > threshold || Mathf.Abs(currentMousePosition.y - lastMousePosition.y) > threshold)
        {
            isMouseMoving = true;
            lastMousePosition = currentMousePosition;
        }
        else
        {
            isMouseMoving = false;
            return;
        }
    }
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
        if (isLoveClick && isMouseMoving)
            LovePoint();
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
