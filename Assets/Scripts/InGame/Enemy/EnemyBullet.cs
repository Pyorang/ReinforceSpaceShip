using UnityEngine;
using UnityEngine.Pool;

public class EnemyBullet : MonoBehaviour
{
    private IObjectPool<EnemyBullet> _ManagePool;

    public void SetManagePool(IObjectPool<EnemyBullet> pool)
    {
        _ManagePool = pool;
    }

    public void DestroyBullet()
    {
        _ManagePool.Release(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerStatus>()!= null)
        {
            collision.GetComponent<PlayerStatus>().GetDamaged();
            DestroyBullet();
        }
    }
}
