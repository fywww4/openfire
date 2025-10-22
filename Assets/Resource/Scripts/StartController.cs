using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    private string instrctionText = "Instrction:\n\n Press left and Right arrow to move. \n Press Space to fire.";
    public Texture startTexture;
    // Update is called once per frame
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), startTexture);
        GUI.Label(new Rect(10, 10, 250, 200), instrctionText);
        if (Input.anyKeyDown)
            SceneManager.LoadScene("Level");
    }
}

