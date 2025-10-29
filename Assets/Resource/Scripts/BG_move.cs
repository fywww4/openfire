using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_move : MonoBehaviour
{

    public float speed = 1f;
   
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
        if (transform.position.x < 5)
            transform.position = new Vector3(10, 0, 0);
    }
}
