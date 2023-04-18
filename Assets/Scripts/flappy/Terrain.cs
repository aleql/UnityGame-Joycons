using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    public float velocidad;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.z < -210){
            transform.position = transform.position + new Vector3(0,0,400);
        }
        transform.position += Vector3.back *velocidad*Time.deltaTime; 
    }
}
