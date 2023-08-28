using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODController : MonoBehaviour
{
    [SerializeField] private static starTier currentStars = starTier.ThreeStars;
    [SerializeField][Range(1,5)]private float currentMusicSlider = 3;
    [SerializeField] private float transitionSpeed;

    private FMOD.Studio.EventInstance eventInstance;

    public FMODUnity.EventReference fmodEvent;

    public static FMODController instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        eventInstance.start();
    }

    void Update()
    {
        updateMusicSlider();
        eventInstance.setParameterByName("STARS", currentMusicSlider);
    }

    public static void setStar(starTier star)
    {
        currentStars = star;
    }

    private void updateMusicSlider()
    {
        currentMusicSlider = Mathf.LerpAngle(currentMusicSlider, (float) currentStars, Time.deltaTime * transitionSpeed);
    }
}

