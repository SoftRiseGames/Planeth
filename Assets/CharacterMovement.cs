using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    bool isMove;
    private void OnEnable()
    {
        Controlls.IsActionCharacter += CharacterIsMove;
        Controlls.IsNonActionCharater += CharacterIsNonMove;

    }
    private void OnDisable()
    {
        Controlls.IsActionCharacter -= CharacterIsMove;
        Controlls.IsNonActionCharater -= CharacterIsNonMove;
    }
    void CharacterIsMove() => isMove = true;
    void CharacterIsNonMove() => isMove = false;


    void CharacterMove()
    {
        if (isMove)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(mousePosition.x, transform.position.y), 8 * Time.deltaTime);
        }

    }
    private void Update()
    {
        CharacterMove();
    }
}
