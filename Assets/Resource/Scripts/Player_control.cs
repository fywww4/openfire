using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microlight.MicroBar;
using TMPro;

public class Player_control : MonoBehaviour
{
    public float speed = 2.0f;
    public GameObject projectile;
    [SerializeField] private Animator _animator;

    [Header("UI (血條)")]
    public MicroBar healthBar;

    [Header("受傷與無敵")]
    public float invincibilityDuration = 2.0f; 
    public float flashInterval = 0.1f; 

    private bool isInvincible = false; 
    private SpriteRenderer spriteRenderer;
    private Collider playerCollider;

    [Header("飛彈冷卻設定")]
    public float missileCooldown = 5f; 
    private float nextMissileTime = 0f; 
    public GameObject homingMissilePrefab;

    public TextMeshProUGUI missileCooldownText;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider>();
        healthBar.Initialize(100f);
    }

    void Update()
    {
        transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, speed * Input.GetAxis("Vertical") * Time.deltaTime, 0);

        if (transform.position.x > 4.63f)
            transform.position = new Vector3(4.63f, transform.position.y, transform.position.z);
        if (transform.position.x < -7.23f)
            transform.position = new Vector3(-7.23f, transform.position.y, transform.position.z);
        if (transform.position.y < -3.34f)
            transform.position = new Vector3(transform.position.x, -3.34f, transform.position.z);
        if (transform.position.y > 3.38f)
            transform.position = new Vector3(transform.position.x, 3.38f, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, transform.position + new Vector3(0.5f, 0, 0), transform.rotation);
            Instantiate(projectile, transform.position + new Vector3(0.1f, 0.5f, 0), transform.rotation);
            Instantiate(projectile, transform.position + new Vector3(0.1f, -0.5f, 0), transform.rotation);
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        if (horizontalInput != 0 || verticalInput != 0)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= nextMissileTime)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length == 0)
                return;

            int missileCount = 5;

            for (int i = 0; i < missileCount; i++)
            {
                GameObject enemy = enemies[i % enemies.Length];

                Vector3 offset = new Vector3(
                    Random.Range(-0.3f, 0.3f),
                    Random.Range(-0.3f, 0.3f),
                    0f
                );

                GameObject missile = Instantiate(homingMissilePrefab, transform.position + offset, Quaternion.identity);

                float randomZRotation = Random.Range(-10f, 10f);
                missile.transform.rotation = Quaternion.Euler(0f, 0f, randomZRotation);

                HomingMissile missileScript = missile.GetComponent<HomingMissile>();
                if (missileScript != null)
                {
                    missileScript.SetTarget(enemy.transform);
                }
            }

            // 設定下一次可發射的時間
            nextMissileTime = Time.time + missileCooldown;
        }


        float timeLeft = nextMissileTime - Time.time;
        if (timeLeft > 0)
        {
            missileCooldownText.text = $"Missile Cooldown: {timeLeft:F1}s";
        }
        else
        {
            missileCooldownText.text = "Missile Ready!";
        }

    }

    public void TakeDamage()
    {
        float damageAmount = 20f; 
        healthBar.UpdateBar(healthBar.CurrentValue - damageAmount);

        if (healthBar.CurrentValue <= 0)
        {
            SceneManager.LoadScene("Lose");
            return;
        }
        StartCoroutine(BecomeInvincible());
    }

    IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        playerCollider.enabled = false;
        StartCoroutine(InvincibilityFlash());
        yield return new WaitForSeconds(invincibilityDuration);
        StopCoroutine(InvincibilityFlash()); 
        spriteRenderer.enabled = true; 
        playerCollider.enabled = true;
        isInvincible = false;
    }


    IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashInterval);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashInterval);
        }
    }
}