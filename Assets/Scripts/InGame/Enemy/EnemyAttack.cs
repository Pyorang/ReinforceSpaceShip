using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public enum AttackType { CircleFire = 0}

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject enemyBulletPrefab;

    private IObjectPool<EnemyBullet> _Pool;

    private void Awake()
    {
        _Pool = new ObjectPool<EnemyBullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, maxSize : 50);
    }

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
                //GameObject clone = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
                EnemyBullet clone = _Pool.Get();
                clone.transform.position = transform.position;
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

    private EnemyBullet CreateBullet()
    {
        EnemyBullet bullet = Instantiate(enemyBulletPrefab).GetComponent<EnemyBullet>();
        bullet.SetManagePool(_Pool);
        return bullet;
    }

    private void OnGetBullet(EnemyBullet enemyBullet)
    {
        enemyBullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(EnemyBullet enemyBullet)
    {
        enemyBullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(EnemyBullet enemyBullet)
    {
        Destroy(enemyBullet.gameObject);
    }
}
