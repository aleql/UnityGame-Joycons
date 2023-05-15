using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public float velocidad = 5; 

    // Update is called once per frame
    void Update()
    {
        velocidad = RunnerManager.speed;
        transform.position += Vector3.left *velocidad*Time.deltaTime;
        if(transform.position.x <= -24){
            transform.position = transform.position + new Vector3(48,0,0);
        }
    }
}
