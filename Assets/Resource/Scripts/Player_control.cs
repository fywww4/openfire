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

        if (transform.position.x < -3.25f) //�����O�_�W�X������ɮy��x=-3.25
            transform.position = new Vector3(-3.25f, transform.position.y, transform.position.z);//���s�]�w����Y�b�y�Цb-3.25����m

        if (transform.position.x > 3.25f) //�����O�_�W�X�k����ɮy��x=-3.25
            transform.position = new Vector3(3.25f, transform.position.y, transform.position.z);//���s�]�w����Y�b�y�Цb3.25����m

        if (transform.position.y < -4.4f) //�����O�_�W�X�k����ɮy��y=-4.5
            transform.position = new Vector3(transform.position.x, -4.4f, transform.position.z);//���s�]�w����Y�b�y�Цb-4.4����m

        if (transform.position.y > 4.4f) //�����O�_�W�X�k����ɮy��y=-4.5
            transform.position = new Vector3(transform.position.x, 4.4f, transform.position.z);//���s�]�w����Y�b�y�Цb4.4����m


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, transform.position, transform.rotation);
            Instantiate(projectile, transform.position + new Vector3(0.5f, -0.5f, 0f), transform.rotation );
            Instantiate(projectile, transform.position + new Vector3(-0.5f, -0.5f, 0f), transform.rotation );
        }
    }
}
