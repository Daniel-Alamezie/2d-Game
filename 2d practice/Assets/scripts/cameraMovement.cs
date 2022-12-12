using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public float length, startPosition;
    public GameObject cam1;

    public float parallaxEffect;


    private void Awake()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
   
    private void Update()
    {
        float temp = (cam1.transform.position.x * (1 - parallaxEffect));
        float dist = (cam1.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPosition + dist, transform.position.y, transform.position.z);

        if (temp > startPosition + length)
        {
            startPosition += length;

        }
        else if (temp < startPosition - length)
        {
            startPosition -= length;
        }
    }
    private void FixedUpdate()
    {
        
    }
    void Follow()
    {
      
       
    }
}
