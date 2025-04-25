using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerStatus>()!= null)
        {
            collision.GetComponent<PlayerStatus>().GetDamaged();
        }
    }
}
