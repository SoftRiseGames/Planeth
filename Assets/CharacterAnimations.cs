using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Controlls.IsNonActionCharater += CharacterAttack;
        CharacterDedectionControl.isEnemysDecreasingHealth += CharacterCollideEnemy;
        CharacterMovement.CharacterReturnPosition += CharacterReturnPosition;
        CharacterMovement.CharacterEndAction += CharacterEndAnimaton;
    }
    private void OnDisable()
    {
        Controlls.IsNonActionCharater -= CharacterAttack;
        CharacterDedectionControl.isEnemysDecreasingHealth -= CharacterCollideEnemy;
        CharacterMovement.CharacterReturnPosition -= CharacterReturnPosition;
        CharacterMovement.CharacterEndAction -= CharacterEndAnimaton;
    }

    void CharacterAttack() => animator.SetBool("isAttack", true);
    void CharacterCollideEnemy() => animator.SetBool("isSuccesfullAttack", true);
    void CharacterReturnPosition() => animator.SetBool("isReturn", true);
    void CharacterEndAnimaton() => animator.SetBool("isReposition", true);

    public void AllAnimationReset()
    {
        animator.SetBool("isAttack", false);
        animator.SetBool("isSuccesfullAttack", false);
        animator.SetBool("isReturn", false);
        animator.SetBool("isReposition", false);

    }


}
