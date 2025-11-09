using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // 這個函式將被我們的動畫事件呼叫
    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}