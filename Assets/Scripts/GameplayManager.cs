using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public ParticleSystem system;

    public ParticleSystem system2;

    public static float speed = 5f;

    public float time = 5f;

    public Generador generador;

    private int puntajes = 0;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.SwitchToGameplayMusic();
        system.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        system2.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    // Update is called once per frame
    void Update()
    {
        if(PuntajeCanvas.puntaje +puntajes >=1000){
            puntajes-=1000;
            system.Play(true);
            system2.Play(true);
            speed = speed * 1.1f;
            time = time*0.9f;
            generador.SetMaxTime(time);
        }
    }
}
