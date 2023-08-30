using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChangeAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve myCurve;
    [SerializeField] private float lerpSpeed;
    private Vector3 endScale;
    private bool firstTime = true;

    // Start is called before the first frame update
    void Awake()
    {
        endScale = this.transform.localScale;
        this.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startZoomAnim()
    {
        if(!firstTime)
        {
            StartCoroutine(startAnim());
        }
        else
        {
            firstTime = false;
        }
    }

    private IEnumerator startAnim()
    {
        float time = 0;
        while(time<1)
        {
            this.transform.localScale = Vector3.Lerp(Vector3.zero, endScale, myCurve.Evaluate(time));
            time += Time.deltaTime * lerpSpeed;
            yield return null;
        }
        this.transform.localScale = Vector3.zero;
    }

}
