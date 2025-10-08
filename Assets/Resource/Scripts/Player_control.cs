using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_control : MonoBehaviour
{
    public float speed = 2.0f;
    public GameObject projectile;

    // Update is called once per frame
    void Update()
    {

        transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime, speed * Input.GetAxis("Vertical") * Time.deltaTime, 0);

        if (transform.position.x < -3.25f) //偵測是否超出左邊邊界座標x=-3.25
            transform.position = new Vector3(-3.25f, transform.position.y, transform.position.z);//重新設定物件的Y軸座標在-3.25的位置

        if (transform.position.x > 3.25f) //偵測是否超出右邊邊界座標x=-3.25
            transform.position = new Vector3(3.25f, transform.position.y, transform.position.z);//重新設定物件的Y軸座標在3.25的位置

        if (transform.position.y < -4.4f) //偵測是否超出右邊邊界座標y=-4.5
            transform.position = new Vector3(transform.position.x, -4.4f, transform.position.z);//重新設定物件的Y軸座標在-4.4的位置

        if (transform.position.y > 4.4f) //偵測是否超出右邊邊界座標y=-4.5
            transform.position = new Vector3(transform.position.x, 4.4f, transform.position.z);//重新設定物件的Y軸座標在4.4的位置


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, transform.position, transform.rotation);
            Instantiate(projectile, transform.position + new Vector3(0.5f, -0.5f, 0f), transform.rotation );
            Instantiate(projectile, transform.position + new Vector3(-0.5f, -0.5f, 0f), transform.rotation );
        }
    }
}
