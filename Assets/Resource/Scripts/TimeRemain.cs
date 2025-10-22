using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeRemain : MonoBehaviour
{
    public Texture[] timeNumbers;
    public static int leftTime = 100;
    float myTime;

    // Update is called once per frame
    void Update()
    {
        myTime += Time.deltaTime;
        if (myTime > 1)
        {
            leftTime--;
            myTime = 0;
        }
        if(leftTime == 0)
            SceneManager.LoadScene("Win");
    }
    

    void OnGUI()
    {
        for (int i = 0; i < leftTime.ToString().Length; i++)
            GUI.DrawTexture(new Rect(265 + i * 32, 20, 32, 45), timeNumbers[System.Int32.Parse((leftTime.ToString())[i].ToString())]);
    }
}
