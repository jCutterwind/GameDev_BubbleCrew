using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODController : MonoBehaviour
{
    [SerializeField] private starTier currentStars = starTier.ThreeStars;
    [SerializeField][Range(1,5)]private float currentMusicSlider;
    [SerializeField] private float transitionSpeed;

    private FMOD.Studio.EventInstance instance;

    public FMODUnity.EventReference fmodEvent;

   
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    void Update()
    {
        updateMusicSlider();
        instance.setParameterByName("STARS", currentMusicSlider);
    }

    public void setStar(starTier star)
    {
        this.currentStars = star;
    }

    private void updateMusicSlider()
    {
        currentMusicSlider = Mathf.LerpAngle(currentMusicSlider, (float) currentStars, Time.deltaTime * transitionSpeed);
    }
}

