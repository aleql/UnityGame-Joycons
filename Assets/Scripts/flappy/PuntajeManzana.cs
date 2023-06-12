using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntajeManzana : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidad;
    public static int manzanas;
    public int puntaje;
    public int health;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,20*Time.deltaTime,0);
        transform.position += Vector3.back *velocidad*Time.deltaTime; 
        velocidad = GameplayManager.speed;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IPickable pickable))
        {   
            manzanas++;
            PuntajeCanvas.puntaje += puntaje;
            // Health the object that collide with me
            pickable.Pick(health);
            Destroy(gameObject);
        }
    }
}
