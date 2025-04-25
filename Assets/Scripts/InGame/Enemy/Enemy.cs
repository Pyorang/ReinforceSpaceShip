using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("Boss Stats")]
    [Space]
    private int currentHP;
    [SerializeField] private int maxHP = 100;

    [Header("Boss Sprite")]
    [Space]
    private Color originalColor;
    [SerializeField] private SpriteRenderer bossSprite;

    public static event Action<float> uiChanged;

    private void Start()
    {
        SetBossMaxHP();

        originalColor = bossSprite.color;
    }

    public void SetBossMaxHP()
    {
        int currentChapterNum = UserDataManager.Instance.GetUserData<UserChapterData>().CurrentChapterNum;
        string filePath = "StageData/Stage" + currentChapterNum;
        StageData stageData = Resources.Load<StageData>(filePath);
        maxHP = stageData.GetBossMaxHP();

        currentHP = maxHP;
    }

    public void OnEnable()
    {
        AudioManager.Instance.Play(AudioType.BGM, "bossFight");
    }

    private void CheckDestroyed()
    {
        if(currentHP < 0)
            Destroy(gameObject);
    }

    public void GetDamaged(int amount)
    {
        AudioManager.Instance.Play(AudioType.SFX, "bossHit");
        currentHP -= amount;
        uiChanged?.Invoke((float)currentHP/maxHP);
        StopCoroutine(Flash_Red());
        StartCoroutine(Flash_Red());
        CheckDestroyed();
    }

    IEnumerator Flash_Red()
    {
        bossSprite.color = Color.red;
        yield return new WaitForSeconds(0.05f); // 0.1초 정도 빨간색 유지

        bossSprite.color = originalColor;
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
