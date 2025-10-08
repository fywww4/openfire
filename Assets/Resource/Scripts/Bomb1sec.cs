using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb1sec : MonoBehaviour
{
    public float delaytime = 5000.0f;
    float i = 0f;
    void Update()
    {
        if(i/Time.deltaTime > delaytime)
            Destroy(gameObject);
        i++;
    }

}
