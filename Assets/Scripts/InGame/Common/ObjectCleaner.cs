using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("InteractiveObject"))
        {
            Destroy(collision.gameObject);
        }
    }
}
