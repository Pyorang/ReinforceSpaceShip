using JetBrains.Annotations;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    public int GetDamage() { return damage; }
    public void SetDamage(int value ) { damage = value; }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().GetDamaged(damage);
            Destroy(gameObject);
        }
    }
}
