using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_move : MonoBehaviour
{

    public float speed = 1f;
   
    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        if (transform.position.x > 13)
            transform.position = new Vector3(-17, 0, 0);
    }
}
