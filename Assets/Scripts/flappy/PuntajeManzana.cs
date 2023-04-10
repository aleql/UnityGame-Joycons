using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntajeManzana : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidad;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,20*Time.deltaTime,0);
        transform.position += Vector3.back *velocidad*Time.deltaTime; 
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") 
    {
        PuntajeCanvas.puntaje += 100;
        Destroy(gameObject);
    }
    }
}
