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
        Controlls.Launch += RocketAnimation;
        CharacterDedectionControl.isEnemysDecreasingHealth += CharacterDamageEnemy;
        CharacterDedectionControl.isEnemyDecreasingOurhealth += CharacterUnDamageEnemy;
        CharacterDedectionControl.isCollideLava += CharacterCollideLava;
        CharacterMovement.CharacterReturnPosition += CharacterReturnPosition;
        CharacterMovement.CharacterEndAction += CharacterEndAnimaton;
    }
    private void OnDisable()
    {
        Controlls.IsNonActionCharater -= CharacterAttack;
        Controlls.Launch -= RocketAnimation;
        CharacterDedectionControl.isEnemysDecreasingHealth -= CharacterDamageEnemy;
        CharacterDedectionControl.isEnemyDecreasingOurhealth -= CharacterUnDamageEnemy;
        CharacterDedectionControl.isCollideLava -= CharacterCollideLava;
        CharacterMovement.CharacterReturnPosition -= CharacterReturnPosition;
        CharacterMovement.CharacterEndAction -= CharacterEndAnimaton;
    }

    void CharacterAttack() => animator.SetBool("isAttack", true);
    void CharacterDamageEnemy() => animator.SetBool("isSuccesfullAttack", true);
    void CharacterUnDamageEnemy() => animator.SetBool("isUnSuccessfullAttack", true);
    void CharacterReturnPosition() => animator.SetBool("isReturn", true);
    void CharacterEndAnimaton() => animator.SetBool("isReposition", true);
    void CharacterCollideLava() => animator.SetBool("isFallGroundCollide", true);
    public void AllAnimationReset()
    {
        animator.SetBool("isAttack", false);
        animator.SetBool("isSuccesfullAttack", false);
        animator.SetBool("isUnSuccessfullAttack", false);
        animator.SetBool("isReturn", false);
        animator.SetBool("isReposition", false);
        animator.SetBool("isFallGroundCollide", false);

    }
    public void RocketAnimation()
    {
        animator.SetBool("isRocket", true);
    }
    public void RocketOffAnimation()
    {
        animator.SetBool("isRocket", false);
    }


}
