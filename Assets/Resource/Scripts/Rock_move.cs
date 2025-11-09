using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rock_move : MonoBehaviour
{
    [Header("Rock Settings")]
    // --- 變數修改如下 ---
    public float resetX = 10f;      // 重生的 X 座標 (螢幕右側)
    public float minY = -4f;        // 重生的 Y 座標最小值
    public float maxY = 4f;         // 重生的 Y 座標最大值
    public float destroyX = -10f;   // 銷毀的 X 座標 (螢幕左側)
    // --- (舊的 minX, maxX, resetY 已被替換) ---

    public float minSpeed = 2f;
    public float maxSpeed = 4f;
    public float respawnDelay = 0.5f; // 延遲重生秒數

    [Header("Explosion Prefabs")]
    public GameObject bombrock;
    public GameObject bombplayer;

    [Header("Game Info")]
    public static int life = 3;
    public static int score = 0;

    private float speed;

    void Start()
    {
        RespawnImmediately();
    }

    void Update()
    {
        // 向左移動 (X 軸負方向)，這行是正確的
        transform.Translate(-speed * Time.deltaTime, 0, 0);

        // 檢查是否超出左邊界
        if (transform.position.x < destroyX)
        {
            RespawnImmediately();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish")) // 子彈打中隕石
        {
            Instantiate(bombrock, transform.position, Quaternion.identity);
            Debug.Log("隕石被子彈擊中！");
            Destroy(other.gameObject);
            score += 10;
            StartCoroutine(RespawnAfterDelay(respawnDelay));
        }
        else if (other.CompareTag("Player")) // 隕石撞到玩家
        {
            Debug.Log("玩家被隕石撞到！");
            Player_control player = other.GetComponent<Player_control>();
            if (player != null)
            {
                player.TakeDamage();
            }
            StartCoroutine(RespawnAfterDelay(respawnDelay));
        }
    }

    // --- 【修改重生邏輯】 ---
    private void RespawnImmediately()
    {
        // 重生在「右側 X 座標」和「隨機 Y 座標」
        transform.position = new Vector3(resetX, Random.Range(minY, maxY), -0.5f);
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // --- 【修改重生邏輯】 ---
    private IEnumerator RespawnAfterDelay(float delay)
    {
        // 隱藏隕石
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(delay);

        // 重生在「右側 X 座標」和「隨機 Y 座標」
        transform.position = new Vector3(resetX, Random.Range(minY, maxY), -0.5f);
        speed = Random.Range(minSpeed, maxSpeed);

        // 顯示回來
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(130, 40, 120, 180), "Score: " + score.ToString());
        GUI.Label(new Rect(470, 40, 60, 180), "Life: " + life.ToString());
    }
}