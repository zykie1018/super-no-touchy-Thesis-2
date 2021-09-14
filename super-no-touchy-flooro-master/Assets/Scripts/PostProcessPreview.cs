using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Wilberforce; //namespace reference when using plugin
using UnityEngine.Rendering.PostProcessing;
public class PostProcessPreview : MonoBehaviour
{
    private static int ppCounter = 0;

    public void FilterButton()
    {
        ppCounter++;
        {
            if (ppCounter == 1)
            {
                GameManager.instance.cam1.SetActive(true);
                GameManager.instance.cam2.SetActive(false);
                GameManager.instance.cam3.SetActive(false);
                GameManager.instance.cam5.SetActive(true);
                GameObject.Find("filter").GetComponent<Text>().text = "Protanopia";
                GameManager.instance.cbeFilter.Type = 1;

            }
            else if (ppCounter == 2)
            {
                GameManager.instance.cam1.SetActive(false);
                GameManager.instance.cam2.SetActive(true);
                GameManager.instance.cam3.SetActive(false);
                GameManager.instance.cam5.SetActive(true);
                GameObject.Find("filter").GetComponent<Text>().text = "Deuteranopia";
                GameManager.instance.cbeFilter.Type = 2;
            }
            else if (ppCounter == 3)
            {
                GameManager.instance.cam1.SetActive(false);
                GameManager.instance.cam2.SetActive(false);
                GameManager.instance.cam3.SetActive(true);
                GameManager.instance.cam5.SetActive(true);
                GameObject.Find("filter").GetComponent<Text>().text = "Tritanopia";
                GameManager.instance.cbeFilter.Type = 3;
            }
            else
            {
                GameManager.instance.cam1.SetActive(false);
                GameManager.instance.cam2.SetActive(false);
                GameManager.instance.cam3.SetActive(false);
                GameManager.instance.cam5.SetActive(true);
                GameObject.Find("filter").GetComponent<Text>().text = "Normal Vision";
                ppCounter = 0;
                GameManager.instance.cbeFilter.Type = 0;
            }
            Debug.Log("filter counter: " + ppCounter);
            Debug.Log("Filter: " + GameManager.instance.cbeFilter.Type);
        }
    }

    public void BackButton()
    {

        GameManager.instance.cam1.SetActive(false);
        GameManager.instance.cam2.SetActive(false);
        GameManager.instance.cam3.SetActive(false);
        GameManager.instance.cam5.SetActive(true);
        ppCounter = 0;
        GameManager.instance.PreviewToMainMenu();
    }

}
