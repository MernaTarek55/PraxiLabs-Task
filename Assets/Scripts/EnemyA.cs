using UnityEngine;

public class EnemyA : EnemyBase
{
    private EnemyStateMachine stateMachine;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackRange = 2f;

    public override void Initialize()
    {
        base.Initialize();

        var player = FindAnyObjectByType<PlayerController>().transform;
        stateMachine = GetComponent<EnemyStateMachine>();
        if (player != null)
        {
            var chase = new ChaseState(stateMachine, transform, player, moveSpeed, attackRange, animator);
            stateMachine.SetState(chase);
        }
    }

    protected override void Die()
    {

        base.Die();
    }
}
