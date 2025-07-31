using UnityEngine;

public class DeadState : IEnemyState
{
    private GameObject enemyObject;
    private Animator animator;

    public DeadState(GameObject enemyObject, Animator animator)
    {
        this.enemyObject = enemyObject;
        this.animator = animator;
    }

    public void Enter()
    {
        animator?.SetTrigger("Die");
        enemyObject.SetActive(false); // or add delay if you want
    }

    public void Update() { }

    public void Exit() { }
}
