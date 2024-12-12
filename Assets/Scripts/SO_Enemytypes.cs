using UnityEngine;

[CreateAssetMenu(menuName ="Enemies/EnemySettings",fileName ="EnemySettings")]

public class SO_Enemytypes : ScriptableObject
{
    public EnemyType enemy;
    public Sprite enemySprite;
    public int EnemyHealth;
    public Animator EnemyDamageAnim;
    public Animator EnemySpikeAnim;
    public float SpeedForXaxis;
    public float enemyFallTimer;
    public float EnemyFallSpeed;
    public float EnemyScale;
}
public enum EnemyType
{
    NoGuard,
    GuardedAndHasSpikes,
    OnlyGuarded
}

