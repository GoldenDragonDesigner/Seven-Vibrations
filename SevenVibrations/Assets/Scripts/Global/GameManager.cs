using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    [Header("Gameplay Variables")]
    public bool gameStart = false;
    public bool gamePause = false;

    [Header("Time Limit Slider")]
    public float timeLimitLeft;
    public float timeMax;
    public Slider countDownSlider;

    [Header("Game Pause Variables")]
    public GameObject pauseMenu;
    public PlayerFollow playerFollow;

    private void Awake()
    {
        if(gm != this && gm != null)
        {
            Destroy(gm.gameObject);
            gm = this;
        }
        else
        {
            gm = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!gamePause && gameStart)
        {
            Debug.Log("Counting down the time for the player to find the object while chanting");
        }

        if (Input.GetButtonDown("Menu"))
        {
            PauseControl();
        }
    }

    public void PauseControl()
    {
        gamePause = !gamePause;
        Time.timeScale = Convert.ToInt32(!gamePause);
        pauseMenu.SetActive(gamePause);
        playerFollow.enabled = !gamePause;
        Cursor.visible = gamePause;
        if (gamePause)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    //Save CreateSaveGameObject()
    //{
    //    Save save = new Save();
    //    //int i = 0;
    //    save.playerPosition.Add(save.SavePosition(GlobalVariables.PLAYER.transform.position));
    //    return save;
    //}

    //public void SaveGame()
    //{
    //    Save save = CreateSaveGameObject();
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Create(Application.persistentDataPath + "/Vibrations.save");
    //    bf.Serialize(file, save);
    //    file.Close();

    //    Debug.Log("Saved Game");
    //}

    //public void LoadGame()
    //{
    //    //1
    //    if (File.Exists(Application.persistentDataPath + "/Vibrations.save"))
    //    {
    //        //2
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(Application.persistentDataPath + "/Vibrations.save", FileMode.Open);
    //        Save save = (Save)bf.Deserialize(file);
    //        file.Close();

    //        //3
    //        //for (int i = 0; i < sa)
    //        GlobalVariables.PLAYER.transform.position = save.LoadPosition(save.playerPosition[0]);
    //        Debug.Log("Game Loaded");
    //    }
    //    else
    //    {
    //        Debug.Log("No game Saved!");
    //    }
    //}
}
