using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 5f;
    [Header("Explosion Prefabs")]
    public GameObject bombrock; 

    void Start()
    {
        Destroy(gameObject, 5f); 
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Finish") || other.CompareTag("Missile"))
        {

            if (other.CompareTag("Player"))
            {
                Player_control player = other.GetComponent<Player_control>();
                if (player != null)
                {
                    player.TakeDamage();
                }
            }
            if (bombrock != null)
            {
                Instantiate(bombrock, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}