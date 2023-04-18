using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorManzana : MonoBehaviour
{
    public float tiempoMax = 1;

    private float tiempoInicial = 0;
    public GameObject manzana;
    public float maxPosY=5;
    public float minPosY=1;
    public float maxPosX=14;
    public float minPosX=6;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newObs = Instantiate(manzana);
        newObs.transform.position = transform.position +new Vector3(0,0,0);
        Destroy(newObs, 12);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        RaycastHit hit;
        
        direction.x += Random.Range(-3f,3f);
        direction.y += Random.Range(-1f, 1f);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3.0f))
        {
            //Choca contra el arbol
            if(transform.position.x <10){
                direction.x += 2;
            }
            else{
                direction.x -= 2;
            }

            if(transform.position.y <3){
                direction.y += 2;
            }
            else{
                direction.y -= 2;
            }
        }

        if(direction.y >0 && transform.position.y>=maxPosY){
            direction.y = 0;
        }
        if(direction.y <0 && transform.position.y<=minPosY){
            direction.y = 0;
        }
        if(direction.x >0 && transform.position.x>= maxPosX){
            direction.x = 0;
        }
        if(direction.x <0 && transform.position.x<=minPosX){
            direction.x = 0;
        }
        transform.position += direction * Time.deltaTime;

        if(tiempoInicial>tiempoMax){
            GameObject newObs = Instantiate(manzana);
            newObs.transform.position = transform.position +new Vector3(0,0,0);
            Destroy(newObs, 12);
            tiempoInicial = 0;
        }
        else{
            tiempoInicial += Time.deltaTime;
        }
    }
}