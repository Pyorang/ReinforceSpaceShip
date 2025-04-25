using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            var _InGameManager = FindAnyObjectByType<InGameManager>();
            _InGameManager.StartBossMode();
        }
    }
}
