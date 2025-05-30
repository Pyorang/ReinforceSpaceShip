using JetBrains.Annotations;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private IObjectPool<PlayerBullet> playerBulletPool;

    public int GetDamage() { return damage; }
    public void SetDamage(int value ) { damage = value; }

    public void SetManagePool(IObjectPool<PlayerBullet> pool)
    {
        playerBulletPool = pool;
    }

    public void DestroyBullet()
    {
        playerBulletPool.Release(this);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().GetDamaged(damage);
            DestroyBullet();
        }
    }
}
