using System.Collections;
using UnityEngine;

public enum AttackType { CircleFire = 0}

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject EnemyBullet;

    private void Start()
    {
        StartCoroutine(StartRandomAttack());
    }

    public void StartFiring(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());
    }

    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }

    private IEnumerator CircleFire()
    {
        float attackRate = 0.5f;
        int count = 30;
        float intervalAngle = 360 / count;
        float weightAngle = 0;

        while (true)
        {
            for(int i = 0;  i < count; i++)
            {
                GameObject clone = Instantiate(EnemyBullet, transform.position, Quaternion.identity);

                float angle = weightAngle + intervalAngle * i;

                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);

                clone.GetComponent<Movement2D>(). MoveTo(new Vector2(x, y));
            }

            weightAngle += 1;

            yield return new WaitForSeconds(attackRate);
        }
    }

    IEnumerator StartRandomAttack()
    {
        while (true)
        {
            int attackCount = System.Enum.GetValues(typeof(AttackType)).Length;
            int randomIndex = Random.Range(0, attackCount);
            AttackType randomAttack = (AttackType)randomIndex;
            StartFiring(randomAttack);

            yield return new WaitForSeconds(3f);
            
            StopFiring(randomAttack);

            yield return new WaitForSeconds(3f);
        }
    }
}
