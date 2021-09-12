using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGuide : MonoBehaviour
{
    public static bool filterChecker;
    public void GuideToMainMenu()
    {
        filterChecker = true;
        GameManager.instance.GuideToMainMenu();
    }
}
