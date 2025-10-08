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
        transform.Translate(0, -speed * Time.deltaTime, 0); //讓物件以Speed值,向下移動

        if (transform.position.y < -6f)//判斷物件是否超出遊戲畫面外,若是,則執行底下的程式
        {
            transform.position = new Vector3(Random.Range(-2.8f, 2.8f), 8f, -0.5f); //把物件放到一個新的座標,X為-2.82.8之間
            speed = Random.Range(2f, 4f); //賦予物件新的速度(從2~4取一隨機值)
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
