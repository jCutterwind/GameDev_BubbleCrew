using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChangeAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve myCurve;
    [SerializeField] private float lerpSpeed;
    private Vector2 endScale;

    // Start is called before the first frame update
    void Start()
    {
        endScale = this.transform.localScale;
        this.transform.localScale = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startZoomAnim()
    {
        StartCoroutine(startAnim());
    }

    private IEnumerator startAnim()
    {
        float time = 0;
        while(time<1)
        {
            this.transform.localScale = Vector2.Lerp(Vector2.zero, endScale, myCurve.Evaluate(time));
            time += Time.deltaTime * lerpSpeed;
            yield return null;
        }
    }

}
