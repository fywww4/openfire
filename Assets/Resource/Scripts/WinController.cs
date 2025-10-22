using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public Texture winTexture;

    // Start is called before the first frame update
    void Start()
    {
        Rock_move.life = 3;
        Rock_move.score = 0;
        TimeRemain.leftTime = 100;
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), winTexture);
        if(Input.anyKeyDown)
            SceneManager.LoadScene("Start");
    }
    
}
