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
    public float SpeedForYaxis;
}
public enum EnemyType
{
    NoGuard,
    GuardedAndHasSpikes,
    OnlyGuarded
}

