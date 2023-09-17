using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] GameObject pauseMenu;
	[SerializeField] private bool paused = false;

	public void TogglePause(InputAction.CallbackContext context)
	{
		Debug.Log("TogglePause");
		if (context.performed)
		{
			paused = !paused;
			if (paused)
			{
				Instantiate(pauseMenu);
				Time.timeScale = 0f;
			}
			else
			{
				Destroy(GameObject.Find("PauseMenu(Clone)"));
				Time.timeScale = 1f;
			}

		}
	}

	public void Home(int sceneID)
	{
		Time.timeScale = 1f; 
		SceneManager.LoadScene(sceneID);
	}
}
