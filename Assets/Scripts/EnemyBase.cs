using UnityEngine;
using Utilities;
public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    public System.Action OnEnemyKilled;

    protected Renderer[] renderers;
    protected MaterialPropertyBlock propertyBlock;
    protected EnemyBehaviorBase behavior;
    public ObjectPool<EnemyBase> PoolReference { get; set; }
    public virtual void Initialize()
    {
        renderers = GetComponentsInChildren<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
        behavior?.Activate();
    }

    public virtual void OnClick()
    {
        Die();
    }

    protected virtual void Die()
    {
        
            OnEnemyKilled?.Invoke();


        ResetEnemy();
    }


    public virtual void ResetEnemy()
    {
        behavior?.ResetBehavior();
        //if (PoolReference != null)
        //    PoolReference.ReturnToPool(this);
        //else
        gameObject.SetActive(false);
    }

    public void SetBehavior(EnemyBehaviorBase newBehavior)
    {
        behavior = newBehavior;
    }
}
