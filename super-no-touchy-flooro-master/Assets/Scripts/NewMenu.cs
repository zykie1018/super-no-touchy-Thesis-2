using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMenu : MonoBehaviour
{

    public Button backBtnConfirmSave;
    private BackBtn backBtn;

    public static NewMenu instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    #region Main Menu Button
    public void NewGame()
    {
        GameManager.instance.NewGame();
    }

    // public void ContinueGame()
    // {
    //     GameManager.instance.ContinueGame();
    // }

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

    public void ConfirmBackToMainMenu()
    {
        GameManager.instance.LoadMainMenu();
        GameManager.instance.ValidateCancelBackToMainMenu();
    }

    public void ConfirmSaveBackToMainMenu()
    {
        // backBtnConfirmSave.confirmBtn.interactable = true;
        // backBtnConfirmSave.interactableBtn();
        backBtnConfirmSave.interactable = true;
        GameManager.instance.SaveMenuGame();
    }

    public void CancelBackToMainMenu()
    {
        GameManager.instance.ValidateCancelBackToMainMenu();
    }
}
