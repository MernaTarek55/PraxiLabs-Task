using UnityEngine;

[RequireComponent(typeof(EnemyBase))]
public class EnemyStateMachine : MonoBehaviour
{
    private IEnemyState currentState;

    public void SetState(IEnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    private void Update()
    {
        currentState?.Update();
    }
}
