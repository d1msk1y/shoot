using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;

public class SlowMotion : MonoBehaviour
{
    //Post proc ref
    public PostProcessVolume postproc;
    private ChromaticAberration _ChromaticAberration;
    private Vignette _Vignette;

    //Aberation
    private float currentAberation;
    public float aberationSpeed;

    //SlowMO
    public float slowMotionSpeed,slowMotionScale = 0.05f;
    private float slowMotionVelocity;

    //Vignette
    private float vignetteVel;
    public float vignetteScale;

    public bool isStoped;
    public static SlowMotion instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
            SlowMotionOn();
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            isStoped = false;
        }

        if(isStoped == false)
        {
            SlowMotionOff();
        }
        
    }

    public void SlowMotionOn()
    {
        //GameManager.instance.lowPasFrequencyFadeOut();
        isStoped = true;
        postproc.profile.TryGetSettings(out _ChromaticAberration);
        _ChromaticAberration.intensity.value = Mathf.SmoothDamp(_ChromaticAberration.intensity.value, 0.60f, ref currentAberation, aberationSpeed);
        postproc.profile.TryGetSettings<Vignette>(out _Vignette);
        _Vignette.intensity.value = Mathf.SmoothDamp(_Vignette.intensity.value, vignetteScale, ref vignetteVel, 0.1f);
        Time.timeScale = Mathf.SmoothDamp(Time.timeScale, slowMotionScale, ref slowMotionVelocity, 0.1f);
    }
    public void SlowMotionOff()
    {
        /*
        postproc.profile.TryGetSettings(out _ChromaticAberration);
        _ChromaticAberration.intensity.value = Mathf.SmoothDamp(_ChromaticAberration.intensity.value, 0.2f, ref currentAberation, aberationSpeed);
        Time.timeScale = Mathf.SmoothDamp(Time.time, 1f, ref slowMotionVelocity, 0.1f);
        */
        //GameManager.instance.lowPasFrequencyFadeOut();
        postproc.profile.TryGetSettings(out _ChromaticAberration);
        _ChromaticAberration.intensity.value = Mathf.SmoothDamp(_ChromaticAberration.intensity.value, 0.2f, ref currentAberation, aberationSpeed);
        postproc.profile.TryGetSettings<Vignette>(out _Vignette);
        _Vignette.intensity.value = Mathf.SmoothDamp(_Vignette.intensity.value, 0.25f, ref vignetteVel, 0.1f);
        Time.timeScale = 1;
    }
}
