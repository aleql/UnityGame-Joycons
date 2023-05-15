using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    public static float speed = 5f;

    public float time = 5f;

    public SpikeGenerator generador;

    private int puntajes = 0;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.SwitchToGameplayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if(PuntajeCanvas.puntaje +puntajes >=1000){
            puntajes-=1000;
            speed = speed * 1.1f;
            time = time*0.9f;
            generador.SetMaxTime(time);
        }
    }

}
