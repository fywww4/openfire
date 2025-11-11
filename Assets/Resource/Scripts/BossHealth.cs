using UnityEngine;
using UnityEngine.SceneManagement;
using Microlight.MicroBar;

public class BossHealth : MonoBehaviour
{
    [Header("UI")]
    public MicroBar bossHealthBar;

    [Header("³]©w")]
    public float maxHealth = 200f;
    public float damagePerHit = 30f;

    private void Start()
    {
        bossHealthBar.Initialize(maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish") || other.CompareTag("Missile"))
        {
            bossHealthBar.UpdateBar(bossHealthBar.CurrentValue - damagePerHit);

            if (bossHealthBar.CurrentValue <= 0)
            {
                SceneManager.LoadScene("Win");
                Destroy(gameObject);
            }
        }
    }
}
