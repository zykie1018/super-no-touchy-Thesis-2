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

        // DontDestroyOnLoad(gameObject);
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
    #region Main Menu Button
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
    #endregion

    // Validation
    public void ConfirmNewGame()
    {
        GameManager.instance.ConfirmNewGame();
    }

    public void CancelNewGame()
    {
        GameManager.instance.ValidateCanceledNewGame();
    }

    public void ConfirmQuit()
    {
        GameManager.instance.confirmQuit();
    }

    public void CancelQuit()
    {
        GameManager.instance.ValidateCanceledQuit();
    }

    public void ConfirmReset()
    {
        GameManager.instance.confirmReset();
    }

    public void CancelReset()
    {
        GameManager.instance.ValidateCanceledReset();
    }
}
