using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcess : MonoBehaviour
{
    public PostProcessVolume volume;

    public PostProcessProfile protanPostFxProfile;
    public PostProcessProfile deuterPostFxProfile;
    private ColorGrading _colorGrading;

    void Start()
    {
        // volume.profile.TryGetSettings(out _colorGrading);

        // _colorGrading.mixerRedOutGreenIn.value = 0;

        //Finds the object in my scene with the post process volume on it
        // This is the code to avoid memory leaks 
        // RuntimeUtilities.DestroyProfile(volume.profile, true);
        volume = GameObject.Find("Volume").GetComponent<PostProcessVolume>();
    }

    public void ChangePostFxProfile()
    {
        if (volume.profile == protanPostFxProfile)
            volume.profile = deuterPostFxProfile;
        else
            volume.profile = protanPostFxProfile;
    }

    public void ChangeProtanPostFx()
    {


    }
}
