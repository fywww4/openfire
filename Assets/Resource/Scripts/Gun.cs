using UnityEngine;

public class Gun : MonoBehaviour 
{
    public float speed = 10f;
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }
}