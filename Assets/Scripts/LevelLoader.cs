using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    
    int currentSceneIndex;

    // Use this for initialization
    void Start () {
        //on starting a scene, fetches the index number of that scene.
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //reloads current scene
    public void RestartScene()
    {
        //reloads current scene, so game is restarted. Nothing saved.
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadYouLose()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
