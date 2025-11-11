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
        string timeString = leftTime.ToString();
        int digitCount = timeString.Length; 

        float totalWidth = digitCount * 32f;
        float startX = (Screen.width / 2f) - (totalWidth / 2f);
        float startY = 20f;

        for (int i = 0; i < digitCount; i++)
        {
            int digit = System.Int32.Parse(timeString[i].ToString());
            GUI.DrawTexture(new Rect(startX + i * 32, startY, 32, 45), timeNumbers[digit]);
        }
    }
}
