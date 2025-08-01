using UnityEngine;

public class AttackState : IEnemyState
{
    private EnemyStateMachine machine;
    private Transform enemyTransform;
    private Transform player;
    private Animator animator;
    private float attackCooldown = 1.5f;
    private float lastAttackTime;

    public AttackState(EnemyStateMachine machine, Transform enemyTransform, Transform player, Animator animator)
    {
        this.machine = machine;
        this.enemyTransform = enemyTransform;
        this.player = player;
        this.animator = animator;
    }

    public void Enter()
    {
        animator?.SetTrigger("Attack");
        lastAttackTime = Time.time;
    }

    public void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(enemyTransform.position, player.position);
        if (distance > 2f)
        {
            machine.SetState(new ChaseState(machine, enemyTransform, player, 3f, 2f, animator));
            return;
        }

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            animator?.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }
    }

    public void Exit()
    {
        animator?.ResetTrigger("Attack");
    }
}
