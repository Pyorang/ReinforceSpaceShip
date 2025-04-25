using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    private bool isInvincible = false;

    [Header("Stats")]
    [Space]
    [SerializeField] private int life = 1;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackRate = 0.1f;
    [SerializeField] private int bomb = 1;

    [Header("Bullets")]
    [Space]
    private Vector3 bulletSpawnPointOffSet = new Vector3(0, 0.1f, 0);
    [SerializeField] private GameObject playerBulletPrefab;

    [SerializeField] private Vector3 PlayerSpawnPoint = new Vector3(0,-1.65f, 0);

    [Header ("Sprites")]
    [Space]
    [SerializeField] private SpriteRenderer spaceShip;
    [SerializeField] private SpriteRenderer flame;
    [SerializeField] private SpriteRenderer lines;

    public static event Action<int, int> uiChanged;

    public void FightBoss()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (true)
        {
            GameObject bullet = Instantiate(playerBulletPrefab, transform.position + bulletSpawnPointOffSet, Quaternion.identity);
            bullet.GetComponent<PlayerBullet>().SetDamage(attackDamage);
            yield return new WaitForSeconds(attackRate);
        }
    }

    public void GetDamaged()
    {
        if(!isInvincible)
        {
            life--;
            uiChanged?.Invoke(1, life);

            if (life <= 0)
            {
                Destroy(this.gameObject);
                var uiData = new BaseUIData();
                UIManager.Instance.OpenUI<DefeatImageUI>(uiData);
            }
            else
            {
                this.transform.position = PlayerSpawnPoint;
                StartCoroutine(InvincibleStarted());
            }
        }
    }

    IEnumerator InvincibleStarted()
    {
        isInvincible = true;

        for (int i = 0; i < 3; i++)
        {
            yield return StartCoroutine(FadeAlpha(0f, 0.5f));
            yield return StartCoroutine(FadeAlpha(1f, 0.5f));
        }

        isInvincible = false;
    }

    IEnumerator FadeAlpha(float targetAlpha, float duration)
    {
        Color color1 = spaceShip.color;
        Color color2 = flame.color;
        Color color3 = lines.color;

        float startAlpha = color1.a;
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            spaceShip.color = new Color(color1.r, color1.g, color1.b, newAlpha);
            flame.color = new Color(color2.r, color2.g, color2.b, newAlpha);
            lines.color = new Color(color3.r, color3.g, color3.b, newAlpha);
            time += Time.deltaTime;
            yield return null;
        }

        // 마지막 알파값 보정
        spaceShip.color = new Color(color1.r, color1.g, color1.b, targetAlpha);
        flame.color = new Color(color2.r,color2.g, color2.b, targetAlpha);
        lines.color = new Color(color3.r, color3.g, color3.b, targetAlpha);
    }

    public void AddLife(int amount)
    {
        life += amount;
        if (life < 1) life = 1;
        uiChanged?.Invoke(1, life);
    }

    public void AddAttackDamage(int amount)
    {
        attackDamage += amount;
        if(attackDamage < 1) attackDamage = 1;
        uiChanged?.Invoke(2, attackDamage);
    }

    public void SetAttackRate(float value)
    {

    }

    public void AddBomb(int amount)
    {
        bomb += amount;
        if (bomb < 1) bomb = 1;
        uiChanged?.Invoke(3, bomb);
    }

    public void UseBomb()
    {

    }

    //관문 통과용 함수
    public int AddRandomLife()
    {
        int value = UnityEngine.Random.Range(1, 4);
        AddLife(value);
        return value;
    }

    public int MinusRandomLife()
    {
        int value = UnityEngine.Random.Range(1, 4);
        AddLife(-value);
        return value;
    }

    public int AddRandomAttackDamage()
    {
        int value = UnityEngine.Random.Range(1, 51);
        AddAttackDamage(value);
        return value;
    }

    public int MinusRandomAttackDamage()
    {
        int value = UnityEngine.Random.Range(1, 51);
        AddAttackDamage(-value);
        return value;
    }

    public int MultipleRandomAttackDamage()
    {
        int multiplier = UnityEngine.Random.Range(1, 5);
        attackDamage *= multiplier;
        uiChanged?.Invoke(2, attackDamage);
        return multiplier;
    }

    public int DivideRandomAttackDamage()
    {
        int divisor = UnityEngine.Random.Range(1, 5);
        attackDamage = Mathf.Max(1, attackDamage / divisor);
        uiChanged?.Invoke(2, attackDamage);
        return divisor;
    }

    public int AddRandomBomb()
    {
        int value = UnityEngine.Random.Range(1, 4);
        AddBomb(value);
        return value;
    }

    public int MinusRandomBomb()
    {
        int value = UnityEngine.Random.Range(1, 4);
        AddBomb(-value);
        return value;
    }
}
