using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsController : MonoBehaviour
{
    public static GameAnalyticsController instance = null;
    private void Awake()
    {
        //Unity tut, Data persistence
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // GameAnalytics.Initialize();
        StartCoroutine(startInitializeAfterDelay());

    }

    IEnumerator startInitializeAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        GameAnalytics.Initialize();
    }

    // Progression Status has Start,Complete,Fail
    public static void SendProgressionEvent(GAProgressionStatus progressionStatus, string progression01)
    {
        GameAnalytics.NewProgressionEvent(progressionStatus, progression01);
        // Debug.Log(progression01);
    }

    public static void SendGAEventDesign(string eventName)
    {
        GameAnalytics.NewDesignEvent(eventName);

        // checker while game isnt built for webgl yet
        // Debug.Log(eventName);

    }
}
