using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public ParticleSystem system;

    public ParticleSystem system2;

    [SerializeField] public static float speed = 5f;
    public static float Gravity;

    public float time = 5f;
    public float timeApple = 5f;

    public Generador generador;
    public GeneradorManzana generadorManzanas;

    private int puntajes = 400;

    // Start is called before the first frame update
    void Start()
    {
        Gravity = 8f;
        speed = 5f;
        time = 5f;
        AudioManager.Instance.SwitchToGameplayMusic();
        system.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        system2.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    // Update is called once per frame
    void Update()
    {
        if(PuntajeCanvas.puntaje >=puntajes){
            Gravity = Gravity*1.2f;
            puntajes = puntajes * 2;
            system.Play(true);
            system2.Play(true);
            speed = speed * 1.25f;
            time = time*0.8f;
            timeApple = timeApple*1.1f;
            generador.SetMaxTime(time);
            generadorManzanas.SetMaxTime(timeApple);
        }
    }
}
