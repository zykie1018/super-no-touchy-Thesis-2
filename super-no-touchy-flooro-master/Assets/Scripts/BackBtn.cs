using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; //for calling UI scripts

public class BackBtn : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject parentObj;
    //public Transform[] hiddenChildren;
    public static bool filterChecker;
    Transform checker;
    string pm = "PauseMenu";
    void Start()
    {
        FindObject();
    }
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
    public void FindObject()
    {
        parentObj = GameObject.FindGameObjectWithTag("GameCanvas");
        // Transform[] hiddenChildren = parentObj.GetComponentsInChildren<Transform>(true);
        
        // foreach (Transform active in hiddenChildren)
        // {
        //     if(active.name == pm)
        //     {
        //        checker = active.gameObject.FindGameObjectWithTag(pm).GetComponent<Transform>();
        //     }
        // }
        pauseUI = parentObj.transform.Find(pm).gameObject;

    }

    //Back to Main Menu
    public void LoadMainMenu()
    {
        filterChecker = true;
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
        Time.timeScale = 1f;
        Debug.Log("Game Saved");
        GameManager.instance.SaveMenuGame();
    }
}
