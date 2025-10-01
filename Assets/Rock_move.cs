using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_move : MonoBehaviour
{
    public float speed = 2.5f;
   
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);

        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.Range(-2.8f, 2.8f), 8f, -0.5f);
            speed = Random.Range(2f, 4f);
        }
    }
}
