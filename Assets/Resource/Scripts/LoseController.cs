using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseController : MonoBehaviour
{
    public Texture loseTexture;
    // Start is called before the first frame update
    void Start()
    {
        Score.Instance = null;
        TimeRemain.leftTime = 100;
    }

    // Update is called once per frame
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), loseTexture);
        if (Input.anyKeyDown)
            SceneManager.LoadScene("Start");
    }

}
