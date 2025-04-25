using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SpaceDoorEffect
{
    plusLife,
    plusAttackDamage,
    multipleAttackDamage,
    plusBomb,
    minusLife,
    minusAttackDamage,
    divideAttackDamage, // ¿ÀÅ¸ ¼öÁ¤ÇÔ
    minusBomb
}

public class SpaceDoor : MonoBehaviour
{
    private int effectAmount = 1;
    [SerializeField] private bool GoodEffect = true;
    [SerializeField] private TextMeshPro effectText;

    private SpaceDoorEffect spaceDoorEffect = SpaceDoorEffect.plusLife;

    private void Awake()
    {
        ChooseEffect();
        UpdateEffectText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ApplyEffect(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void ChooseEffect()
    {
        if (GoodEffect)
        {
            int random = Random.Range(0, 4);
            spaceDoorEffect = (SpaceDoorEffect)random;
        }
        else
        {
            int random = Random.Range(4, 8);
            spaceDoorEffect = (SpaceDoorEffect)random;
        }
    }

    private void UpdateEffectText()
    {
        switch (spaceDoorEffect)
        {
            case SpaceDoorEffect.plusLife:
                effectAmount = UnityEngine.Random.Range(1, 4);
                effectText.text = $"+{effectAmount} ¸ñ¼û";
                break;
            case SpaceDoorEffect.plusAttackDamage:
                effectAmount = UnityEngine.Random.Range(1, 51);
                effectText.text = $"+{effectAmount} µ¥¹ÌÁö";
                break;
            case SpaceDoorEffect.multipleAttackDamage:
                effectAmount = UnityEngine.Random.Range(1, 5);
                effectText.text = $"X{effectAmount} µ¥¹ÌÁö";
                break;
            case SpaceDoorEffect.plusBomb:
                effectAmount = UnityEngine.Random.Range(1, 4);
                effectText.text = $"+{effectAmount} ÆøÅº";
                break;
            case SpaceDoorEffect.minusLife:
                effectAmount = UnityEngine.Random.Range(1, 4);
                effectText.text = $"-{effectAmount} ¸ñ¼û";
                break;
            case SpaceDoorEffect.minusAttackDamage:
                effectAmount = UnityEngine.Random.Range(1, 51);
                effectText.text = $"-{effectAmount} µ¥¹ÌÁö";
                break;
            case SpaceDoorEffect.divideAttackDamage:
                effectAmount = UnityEngine.Random.Range(1, 5);
                effectText.text = $"/{effectAmount} µ¥¹ÌÁö";
                break;
            case SpaceDoorEffect.minusBomb:
                effectAmount = UnityEngine.Random.Range(1, 4);
                effectText.text = $"-{effectAmount} ÆøÅº";
                break;
        }
    }

    private void ApplyEffect(GameObject player)
    {
        PlayerStatus stats = player.GetComponent<PlayerStatus>();
        
        if (stats == null)
        {
            return;
        }

        switch (spaceDoorEffect)
        {
            case SpaceDoorEffect.plusLife:
                stats.AddLife(effectAmount);
                break;
            case SpaceDoorEffect.plusAttackDamage:
                stats.AddAttackDamage(effectAmount);
                break;
            case SpaceDoorEffect.multipleAttackDamage:
                stats.MultipleAttackDamage(effectAmount);
                break;
            case SpaceDoorEffect.plusBomb:
                stats.AddBomb(effectAmount);
                break;
            case SpaceDoorEffect.minusLife:
                stats.AddLife(-effectAmount);
                break;
            case SpaceDoorEffect.minusAttackDamage:
                stats.AddAttackDamage(-effectAmount);
                break;
            case SpaceDoorEffect.divideAttackDamage:
                stats.DivideAttackDamage(effectAmount);
                break;
            case SpaceDoorEffect.minusBomb:
                stats.AddBomb(-effectAmount);
                break;
        }
    }
}
