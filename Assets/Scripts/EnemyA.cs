public class EnemyA : EnemyBase
{
    public EnemyBehaviorBase defaultBehavior;

    public override void Initialize()
    {
        base.Initialize();
        SetBehavior(defaultBehavior);
        behavior.Activate();
    }

    protected override void Die()
    {
        // play effect, sound
        base.Die();
    }
}
