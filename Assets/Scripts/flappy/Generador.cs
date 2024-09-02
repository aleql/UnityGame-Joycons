using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FlappyBirdController;

public class Generador : MonoBehaviour
{
    public float tiempoMax = 1;

    private float tiempoInicial = 0;
    public GameObject[] obstaculo;
    public float altura;
    private int _lastRandom = 0;

    // obstacle 1: 20, 14, 0
    // obstacle 2: 18, 10, 0
    // Start is called before the first frame update
    void Start()
    {
        GameObject newObs = Instantiate(obstaculo[0]);
        newObs.transform.position += transform.position;
        //Destroy(newObs, 12);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FlappyBirdController.Instance.currentGameState == FlappyBirdGameStates.Playing)
        {
            if (tiempoInicial>tiempoMax){
                int RandomOption = NextRandomObstacle();
                GameObject newObs = Instantiate(obstaculo[RandomOption]);
                newObs.transform.position += transform.position;
                //Destroy(newObs, 12);
                tiempoInicial = 0;
            }
            else{
                tiempoInicial += Time.deltaTime;
            }
        }
    }

    public void SetMaxTime(float time){
        tiempoMax = time;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("WORKS");
    }
    private int NextRandomObstacle()
    {
        int RandomOption = Random.Range(0, obstaculo.Length);
        while (RandomOption == _lastRandom)
        {
            RandomOption = Random.Range(0, obstaculo.Length);
        }
        _lastRandom = RandomOption;
        return RandomOption;
    }
}
