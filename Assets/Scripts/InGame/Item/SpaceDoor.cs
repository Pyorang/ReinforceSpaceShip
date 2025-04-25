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
    divideAttackDamage, // ��Ÿ ������
    minusBomb
}

public class SpaceDoor : MonoBehaviour
{
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
                effectText.text = $"+? ���";
                break;
            case SpaceDoorEffect.plusAttackDamage:
                effectText.text = $"+? ������";
                break;
            case SpaceDoorEffect.multipleAttackDamage:
                effectText.text = $"X? ������";
                break;
            case SpaceDoorEffect.plusBomb:
                effectText.text = $"+? ��ź";
                break;
            case SpaceDoorEffect.minusLife:
                effectText.text = $"-? ���";
                break;
            case SpaceDoorEffect.minusAttackDamage:
                effectText.text = $"-? ������";
                break;
            case SpaceDoorEffect.divideAttackDamage:
                effectText.text = $"/? ������";
                break;
            case SpaceDoorEffect.minusBomb:
                effectText.text = $"-? ��ź";
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
                stats.AddRandomLife();
                break;
            case SpaceDoorEffect.plusAttackDamage:
                stats.AddRandomAttackDamage();
                break;
            case SpaceDoorEffect.multipleAttackDamage:
                stats.MultipleRandomAttackDamage();
                break;
            case SpaceDoorEffect.plusBomb:
                stats.AddRandomBomb();
                break;
            case SpaceDoorEffect.minusLife:
                stats.MinusRandomLife();
                break;
            case SpaceDoorEffect.minusAttackDamage:
                stats.MinusRandomAttackDamage();
                break;
            case SpaceDoorEffect.divideAttackDamage:
                stats.DivideRandomAttackDamage();
                break;
            case SpaceDoorEffect.minusBomb:
                stats.MinusRandomBomb();
                break;
        }
    }
}
