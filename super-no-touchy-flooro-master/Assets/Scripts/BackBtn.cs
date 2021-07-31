using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; //for calling UI scripts

public class BackBtn : MonoBehaviour
{
    public string backTomenu;
    public GameObject pauseUI;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }     
    }

    // Pause Menu
    public void Toggle()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
            if(pauseUI.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
    }

    //Back to Main Menu
    public void LoadMainMenu()
    {
        //Debug.Log("Loading Main Menu...");
        Time.timeScale = 1f;
        GameManager.instance.LoadMainMenu();
    }
    //Quit Game
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    //Save Game- semiworking 
    public void SaveGame()
    {
        Debug.Log("Game Saved");
        GameManager.instance.SaveMenuGame();
    }
}
