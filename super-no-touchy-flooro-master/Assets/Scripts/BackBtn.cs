using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtn : MonoBehaviour
{
    public GameObject pauseUI;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
