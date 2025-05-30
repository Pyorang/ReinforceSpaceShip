using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.Pool;

public class PlayerStatus : MonoBehaviour
{
    private bool isInvincible = false;
    private IObjectPool<PlayerBullet> playerBulletPool;

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
    [SerializeField] private GameObject playerBombPrefab;

    [SerializeField] private Vector3 PlayerSpawnPoint = new Vector3(0,-1.65f, 0);

    [Header ("Sprites")]
    [Space]
    [SerializeField] private SpriteRenderer spaceShip;
    [SerializeField] private SpriteRenderer flame;
    [SerializeField] private SpriteRenderer lines;

    public static event Action<int, int> uiChanged;

    private void Awake()
    {
        playerBulletPool = new ObjectPool<PlayerBullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, maxSize: 30);
    }

    public void FightBoss()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        while (true)
        {
            var bullet = playerBulletPool.Get();
            bullet.transform.position = transform.position + bulletSpawnPointOffSet;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetDamage(attackDamage);
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

    public void MultipleAttackDamage(int amount)
    {
        attackDamage *= amount;
        if (attackDamage < 1) attackDamage = 1;
        uiChanged?.Invoke(2, attackDamage);
    }

    public void DivideAttackDamage(int amount)
    {
        attackDamage /= amount;
        if (attackDamage < 1) attackDamage = 1;
        uiChanged?.Invoke(2, attackDamage);
    }

    public void AddBomb(int amount)
    {
        bomb += amount;
        if (bomb < 0) bomb = 0;
        uiChanged?.Invoke(3, bomb);
    }

    public void UseBomb()
    {
        if(bomb >= 1)
        {
            bomb--;
            uiChanged?.Invoke(3, bomb);
            Instantiate(playerBombPrefab, transform.position + bulletSpawnPointOffSet, Quaternion.identity);
        }
    }

    private PlayerBullet CreateBullet()
    {
        PlayerBullet bullet = Instantiate(playerBulletPrefab).GetComponent<PlayerBullet>();
        bullet.SetManagePool(playerBulletPool);
        return bullet;
    }

    private void OnGetBullet(PlayerBullet playerBullet)
    {
        playerBullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(PlayerBullet playerBullet)
    {
        playerBullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(PlayerBullet playerBullet)
    {
        Destroy(playerBullet.gameObject);
    }
}
