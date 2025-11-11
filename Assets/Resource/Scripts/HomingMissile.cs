using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 5f;
    private Transform target;
    public GameObject explosionPrefab;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnTriggerEnter(Collider other)
    {
        // 檢查是否撞到「敵人」、「Boss」或「敵人的砲彈」
        if (other.CompareTag("Enemy") || other.CompareTag("Boss") || other.CompareTag("EnemyBullet"))
        {
            // 播放爆炸
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            // (如果撞到的是 Enemy 或 EnemyBullet，則銷毀對方)
            else if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
            {
                Destroy(other.gameObject);
            }

            // 銷毀「飛彈自己」
            Destroy(gameObject);
        }
    }
}