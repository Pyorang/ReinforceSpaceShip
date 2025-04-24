using UnityEngine;
using System;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int life = 1;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private int attackRate = 1;
    [SerializeField] private int bomb = 1;

    //ÃÑ¾Ë ÇÁ¸®ÆÕ [SerializeField] private

    public static event Action<int, int> uiChanged;

    public void Attack()
    {

    }

    public void addLife(int amount)
    {
        life += amount;
        uiChanged?.Invoke(1, life);
    }

    public void addAttackDamage(int amount)
    {
        attackDamage += amount;
        uiChanged?.Invoke(2, attackDamage);
    }

    public void setAttackRate(double value)
    {

    }

    public void addBomb(int amount)
    {
        bomb += amount;
        uiChanged?.Invoke(3, bomb);
    }

    public void useBomb()
    {

    }
}
