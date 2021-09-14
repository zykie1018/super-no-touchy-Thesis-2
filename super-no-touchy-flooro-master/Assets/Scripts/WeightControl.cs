using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class WeightControl : MonoBehaviour
{
    ColorGrading colorGrading;
    PostProcessVolume volume;
    PostProcessProfile profile;

    public Slider slider;

    [Range(0, 1)]
    public float changeSliderValue;
    private float hueShiftMin = -180f;
    private float hueShiftMax = 180f;

    private void Start()
    {
        volume = GameObject.Find("ProtanPostFx").GetComponent<PostProcessVolume>();

        colorGrading = volume.profile.GetSetting<ColorGrading>();
    }

    private void Update()
    {
        ChangeColor();
    }

    public void ChangeColor()
    {
        //colorGrading.hueShift.value = Random.Range(hueShiftMin, hueShiftMax);
        //colorGrading.mixerGreenOutGreenIn.value = -50f;
        changeSliderValue = slider.value;
        volume.weight = changeSliderValue;
    }
}
