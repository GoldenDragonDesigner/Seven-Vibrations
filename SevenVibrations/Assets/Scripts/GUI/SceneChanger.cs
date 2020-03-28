using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Tooltip("Type the name of the scene you want to change here.  It is case sensitive")]
    public string sceneName;

    public void ChangeScenes()
    {
        SceneManager.LoadScene(sceneName);
    }
}
