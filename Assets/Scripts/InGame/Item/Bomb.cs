using UnityEngine;

public class Bomb : MonoBehaviour ,Item
{
    [SerializeField] private int addAmount = 1;
    [SerializeField] private Movement2D movement2D;

    public void Start()
    {
        movement2D.MoveTo(Vector3.down);
    }

    public void applyItemEffect(PlayerStatus status)
    {
        status.AddBomb(addAmount);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        PlayerStatus status = other.GetComponent<PlayerStatus>();

        if (status != null)
        {
            AudioManager.Instance.Play(AudioType.SFX, "item");
            applyItemEffect(status);

            Destroy(this.gameObject);
        }
    }
}
