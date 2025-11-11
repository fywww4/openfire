using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float moveDistance = 3f; // 來回移動的範圍
    public float moveSpeed = 2f;    // 移動速度
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
    }
}
