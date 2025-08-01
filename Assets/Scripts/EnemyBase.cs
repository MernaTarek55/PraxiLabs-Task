using UnityEngine;
using Utilities;
public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    public System.Action OnEnemyKilled;

    protected Renderer[] renderers;
    protected MaterialPropertyBlock propertyBlock;
    public ObjectPool<EnemyBase> PoolReference { get; set; }
    public virtual void Initialize()
    {
        renderers = GetComponentsInChildren<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
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
        //if (PoolReference != null)
        //    PoolReference.ReturnToPool(this);
        //else
        gameObject.SetActive(false);
    }

}
