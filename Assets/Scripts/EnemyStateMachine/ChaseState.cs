using UnityEngine;

public class ChaseState : IEnemyState
{
    private EnemyStateMachine machine;
    private Transform enemyTransform;
    private Transform player;
    private float speed;
    private float attackRange;
    private Animator animator;

    public ChaseState(EnemyStateMachine machine, Transform enemyTransform, Transform player, float speed, float attackRange, Animator animator)
    {
        this.machine = machine;
        this.enemyTransform = enemyTransform;
        this.player = player;
        this.speed = speed;
        this.attackRange = attackRange;
        this.animator = animator;
    }

    public void Enter()
    {
        animator?.SetBool("IsRunning", true);
    }

    public void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(enemyTransform.position, player.position);
        if (distance <= attackRange)
        {
            machine.SetState(new AttackState(machine, enemyTransform, player, animator));
            return;
        }

        Vector3 direction = (player.position - enemyTransform.position).normalized;
        direction.y = 0;
        enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
        enemyTransform.position += direction * speed * Time.deltaTime;
    }

    public void Exit()
    {
        animator?.SetBool("IsRunning", false);
    }
}
