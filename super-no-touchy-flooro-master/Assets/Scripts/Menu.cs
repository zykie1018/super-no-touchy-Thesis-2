using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu instance; //needed for button functions, easier music/sounds bc gm wouldn't play when transition to load state

    public AudioClip menuMusic;
    public AudioClip cursorScroll;
    public AudioClip cursorSelect;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            AudioPlayer.instance.PlaySound(cursorScroll);
        }
        if (Input.GetButtonDown("Submit"))
        {
            AudioPlayer.instance.PlaySound(cursorSelect);
        }
    }

    public void NewGame()
    {
        GameManager.instance.NewGame();
    }

    public void ContinueGame()
    {
        GameManager.instance.ContinueGame();
    }

    public void Guide()
    {
        GameManager.instance.GuideScene();
    }

    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }

    public void Filter()
    {
        GameManager.instance.Filter();
    }

    public void PpAdjuster()
    {
        GameManager.instance.PpAdjuster();
    }

    public void ToggleClassicMode()
    {
        GameManager.instance.ToggleClassicMode();
    }

    public void RestartGame()
    {
        GameManager.instance.RestartGame();
    }

    public void ResetSave()
    {
        GameManager.instance.ResetSave();
    }

    public void confirmNewGame()
    {
        GameManager.instance.confirmNewGame();
    }

    public void cancelNewGame()
    {
        GameManager.instance.cancelNewGame();
    }

    public void confirmQuit()
    {
        GameManager.instance.confirmQuit();
    }

    public void cancelQuit()
    {
        GameManager.instance.cancelQuit();
    }

    public void confirmReset()
    {
        GameManager.instance.confirmReset();
    }

    public void cancelReset()
    {
        GameManager.instance.cancelReset();
    }
}
