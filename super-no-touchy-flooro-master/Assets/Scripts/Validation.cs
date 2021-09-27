using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Validation : MonoBehaviour
{
    public static Validation instance = null;

    private void Awake()
    {
        //Unity tut, Data persistence
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

}
