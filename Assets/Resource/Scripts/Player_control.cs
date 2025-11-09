using Microlight.MicroBar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using Microlight.MicroBar;

public class Player_control : MonoBehaviour
{
    public float speed = 2.0f;
    public GameObject projectile;
    [SerializeField] private Animator _animator;

    // --- 【新增】受傷與閃爍的變數 ---
    [Header("受傷與無敵")]
    public float invincibilityDuration = 2.0f; // 無敵總時長 (2秒)
    public float flashInterval = 0.1f; // 閃爍間隔 (每 0.1 秒閃一次)

    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    private Collider playerCollider;

    [Header("UI")]
    public MicroBar playerHealthBar;


    void Start()
    {
        // 取得需要的元件
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider>();
        UpdateHealthBar();
        // 註：如果你的 2D 遊戲用的是 Collider2D，上面那行要改成：
        // playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // --- 1. 移動與邊界 (你的原始碼) ---
        transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, speed * Input.GetAxis("Vertical") * Time.deltaTime, 0);

        if (transform.position.x > 4.63f)
            transform.position = new Vector3(4.63f, transform.position.y, transform.position.z);
        if (transform.position.x < -7.23f)
            transform.position = new Vector3(-7.23f, transform.position.y, transform.position.z);
        if (transform.position.y < -3.34f)
            transform.position = new Vector3(transform.position.x, -3.34f, transform.position.z);
        if (transform.position.y > 3.38f)
            transform.position = new Vector3(transform.position.x, 3.38f, transform.position.z);

        // --- 2. 開火 (你的原始碼) ---
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, transform.position + new Vector3(0.5f, 0, 0), transform.rotation);
            Instantiate(projectile, transform.position + new Vector3(0.1f, 0.5f, 0), transform.rotation);
            Instantiate(projectile, transform.position + new Vector3(0.1f, -0.5f, 0), transform.rotation);
        }

        // --- 3. 動畫 (你的原始碼) ---
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
    }

    // --- 【新增】被擊中的函式 (由 Rock_move.cs 呼叫) ---
    public void TakeDamage()
    {
        // 1. 如果正在無敵，就直接返回，不重複受傷
        if (isInvincible)
        {
            return;
        }

        // 2. 減少生命值 (我們把這邏輯從 Rock_move 搬過來了)
        Rock_move.life--;
        Debug.Log("玩家受傷! 剩餘生命: " + Rock_move.life);

        UpdateHealthBar();

        // 3. 檢查是否死亡
        if (Rock_move.life <= 0)
        {
            SceneManager.LoadScene("Lose");
            return; // 死了就不用閃爍了
        }

        // 4. 如果沒死，就開始無敵
        StartCoroutine(BecomeInvincible());
    }

    // --- 【新增】處理無敵狀態的協程 ---
    IEnumerator BecomeInvincible()
    {
        Debug.Log("無敵開始!");
        isInvincible = true;

        // 1. 關閉碰撞體 (這樣才不會在2秒內又被撞到)
        playerCollider.enabled = false;

        // 2. 開始閃爍 (另外呼叫一個協程來處理)
        StartCoroutine(InvincibilityFlash());

        // 3. 等待 2 秒鐘 (無敵總時長)
        yield return new WaitForSeconds(invincibilityDuration);

        // 4. 無敵時間到，停止閃爍並恢復正常
        StopCoroutine(InvincibilityFlash()); // 停止閃爍
        spriteRenderer.enabled = true; // 確保最後玩家一定是顯示的

        // 5. 重新開啟碰撞體
        playerCollider.enabled = true;

        isInvincible = false;
        Debug.Log("無敵結束!");
    }

    // --- 【新增】處理「閃爍」的協程 ---
    IEnumerator InvincibilityFlash()
    {
        // 只要還在無敵狀態 (isInvincible == true)，就一直執行迴圈
        while (isInvincible)
        {
            // 隱藏 Sprite
            spriteRenderer.enabled = false;
            // 等待 0.1 秒
            yield return new WaitForSeconds(flashInterval);

            // 顯示 Sprite
            spriteRenderer.enabled = true;
            // 再等待 0.1 秒
            yield return new WaitForSeconds(flashInterval);
        }
    }
    void UpdateHealthBar()
    {
        if (playerHealthBar == null)
        {
            Debug.LogError("血條 (playerHealthBar) 沒有被指定!");
            return;
        }

        // 計算目前生命值的百分比 (例如 2 / 3 = 0.66)
        // 我們需要用 (float) 來轉換型態，避免整數除法 (2 / 3 = 0)
        float percent = (float)Rock_move.life / (float)Rock_move.maxLife;

        // 呼叫 MicroBar 的功能來更新血條
        // .UpdateBar(percent) 是 MicroBar 最核心的函式
        playerHealthBar.UpdateBar(percent);
    }
}