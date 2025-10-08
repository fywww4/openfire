using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rock_move : MonoBehaviour
{
    public float speed = 2.5f;
    public GameObject bombrock;
    public GameObject bombplayer;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0); //������HSpeed��,�V�U����

        if (transform.position.y < -6f)//�P�_����O�_�W�X�C���e���~,�Y�O,�h���橳�U���{��
        {
            transform.position = new Vector3(Random.Range(-2.8f, 2.8f), 8f, -0.5f); //�⪫����@�ӷs���y��,X��-2.82.8����
            speed = Random.Range(2f, 4f); //�ᤩ����s���t��(�q2~4���@�H����)
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            transform.position = new Vector3(Random.Range(-2.8f, 2.8f), 6f, -0.5f);
            speed = Random.Range(2f, 4f);
            Destroy(other.gameObject);
        }
        if(other.tag == "Player")
        {
            transform.position = new Vector3(Random.Range(-2.8f, 2.8f), 6f, -0.5f);
        }
    }       
}
