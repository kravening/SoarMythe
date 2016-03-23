using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSelector : MonoBehaviour 
{
	public string loadLevel;
    XboxInputMenu XboxInput;
    bool aButton;

    void start()
    {
        //xbox360WiredInputController = GetComponent<Xbox360Wired_InputController>();
    }

	public void ChangeLevel()
	{
        if (aButton == true)
        {
            SceneManager.LoadScene(loadLevel);
        }
	}
}
