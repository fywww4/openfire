using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform[] firePoints;

    public float minShootInterval = 0.5f; 
    public float maxShootInterval = 1.5f; 

    private float nextShootTime; 

    void Start()
    {
        ScheduleNextShot();
    }

    void Update()
    {
        if (Time.time >= nextShootTime)
        {
            Shoot(); 
            ScheduleNextShot(); 
        }
    }

    void ScheduleNextShot()
    {
        // 預定下一次發射時間 = 現在時間 + (一個 0.5 ~ 1.5 秒的隨機數字)
        nextShootTime = Time.time + Random.Range(minShootInterval, maxShootInterval);
    }

    void Shoot()
    {
        int randomIndex = Random.Range(0, firePoints.Length);
        Transform selectedFirePoint = firePoints[randomIndex];

        if (selectedFirePoint == null)
        {
            Debug.LogError("砲口清單(FirePoints)是空的或有遺失！");
            return;
        }
        Instantiate(bulletPrefab, selectedFirePoint.position, Quaternion.identity);
    }
}