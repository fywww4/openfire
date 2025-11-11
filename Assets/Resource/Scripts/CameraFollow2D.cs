using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // 拖入玩家飛機
    public Vector3 offset = new Vector3(0, 0, -10f);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
