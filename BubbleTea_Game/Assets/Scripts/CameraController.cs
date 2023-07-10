using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed;
    private bool isLeft = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Debug.Log(touch.position);
        }
    }

    public void MoveCamera(Vector2 position)
    {
        Vector2 newPos = Vector2.Lerp(transform.position, position, Time.deltaTime * moveSpeed);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
    



}
