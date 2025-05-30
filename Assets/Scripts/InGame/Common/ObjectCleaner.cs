using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyBullet>() != null)
        {
            collision.GetComponent<EnemyBullet>().DestroyBullet();
            return;
        }

        if (collision.GetComponent<PlayerBullet>() != null)
        {
            collision.GetComponent<PlayerBullet>().DestroyBullet();
            return;
        }

        if(collision.CompareTag("InteractiveObject"))
        {
            Destroy(collision.gameObject);
        }
    }
}
