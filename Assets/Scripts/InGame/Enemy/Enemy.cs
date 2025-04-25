using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int currentHP = 100;
    [SerializeField] private int maxHP = 100;

    public static event Action<float> uiChanged;

    private void CheckDestroyed()
    {
        if(currentHP < 0)
            Destroy(gameObject);
    }

    public void GetDamaged(int amount)
    {
        currentHP -= amount;
        uiChanged?.Invoke((float)currentHP/maxHP);
        CheckDestroyed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStatus>() != null)
        {
            var stats = collision.gameObject.GetComponent<PlayerStatus>();
            stats.GetDamaged();
        }
    }

    public void SetBossActive()
    {
        gameObject.SetActive(true);
    }
}
