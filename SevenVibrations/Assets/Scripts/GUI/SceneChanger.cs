using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Tooltip("Type the name of the scene you want to change here.  It is case sensitive")]
    public string sceneName;

    public void ChangeScenes()//function for changing scenes
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()//function for quitting the game
    {
        Application.Quit();
        Debug.Log("Game Closing");
    }
}
