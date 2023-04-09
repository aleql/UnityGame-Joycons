using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
    public float tiempoMax = 1;

    private float tiempoInicial = 0;
    public GameObject[] obstaculo;
    public float altura;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newObs = Instantiate(obstaculo[0]);
        newObs.transform.position = transform.position +new Vector3(0,0,0);
        Destroy(newObs, 6);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(tiempoInicial>tiempoMax){
            int RandomOption = Random.Range(0, obstaculo.Length);
            GameObject newObs = Instantiate(obstaculo[RandomOption]);
            newObs.transform.position = transform.position +new Vector3(0,0,0);
            Destroy(newObs, 6);
            tiempoInicial = 0;
        }
        else{
            tiempoInicial += Time.deltaTime;
        }
    }
}
