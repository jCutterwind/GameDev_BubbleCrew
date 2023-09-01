using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODController : MonoBehaviour
{
    [SerializeField] private static starTier currentStars = starTier.ThreeStars;
    [SerializeField][Range(1,5)]private float currentMusicSlider = 3;
    [SerializeField] private float transitionSpeed;
    private float currentMenuSlider = 1;
    private float currentPeopleSlider;
    private int isMenu = 1;
    [SerializeField] private float tolerance;

    private FMOD.Studio.EventInstance eventInstance;

    public FMODUnity.EventReference fmodEvent;

    public static FMODController instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
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
        eventInstance.setParameterByName("MainMenu", currentMenuSlider);
    }

    public void setStar(starTier star)
    {
        currentStars = star;
    }

    public void setMenu(int isMenu)
    {
        this.isMenu = isMenu;
    }

    private void updateMusicSlider()
    {
        if(Mathf.Abs(currentMusicSlider-(float)currentStars)>tolerance)
        {
            currentMusicSlider = Mathf.LerpAngle(currentMusicSlider, (float)currentStars, Time.deltaTime * transitionSpeed);
        }
        else
        {
            currentMusicSlider = (float)currentStars;
        }

        if(Mathf.Abs(currentMenuSlider - isMenu) > tolerance)
        {
            currentMenuSlider = Mathf.LerpAngle(currentMenuSlider, isMenu, Time.deltaTime * transitionSpeed);
        }
        else
        {
            currentMenuSlider = isMenu;
        }

    }
}

