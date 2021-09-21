using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class WeightControl : MonoBehaviour
{
    ColorGrading colorGrading;
    [SerializeField] private PostProcessVolume[] volume;
    public Slider weightSlider;

    // [Range(0, 1)]
    // public float changeSliderValue;

    private float weightShiftMin = 0f;
    private float weightShiftMax = 1f;
    private void Start()
    {
        // volume.gameObject;

        // colorGrading = volume.profile.GetSetting<ColorGrading>();
        //Debug.Log("weight = " + volume.weight);

        volume = GameManager.instance.AccessVolume();
    }

    private void Update()
    {
    }

    public void ChangeColor(System.Single sliderValue)
    {

        //colorGrading.hueShift.value = Random.Range(hueShiftMin, hueShiftMax);
        //colorGrading.mixerGreenOutGreenIn.value = -50f;
        Debug.Log(volume.Length);
        for (int i = 0; i < volume.Length; i++)
        {
            volume[i].weight = Mathf.Lerp(weightShiftMin, weightShiftMax, sliderValue);
            Debug.Log("value: " + sliderValue);

        }
    }
}
