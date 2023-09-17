using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagment;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    // Start is called before the first frame update
    
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timesScale = 0f; //https://docs.unity3d.com/ScriptReference/Time-timeScale.html
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timescale = 1f; // so that game unfreezes 
    }

    public void Home(int sceneID)
    {
        Time.timescale = 1f; // need to set to 1 as well
        SceneManager.LoadScene(sceneID);
    }
}
