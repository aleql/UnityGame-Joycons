using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerManager : MonoBehaviour
{
    [SerializeField] public static float speed = 5f;

    public float time = 5f;

    public SpikeGenerator generador;

    private int puntajes = 200;

    [SerializeField] public static float Mag = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Mag = 0.1f;
        speed = 5f;
        time = 5f;
        AudioManager.Instance.SwitchToGameplayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if(PuntajeCanvas.puntaje >=puntajes){
            Mag += Mag*0.2f;
            puntajes = puntajes*2;
            speed = speed * 1.1f;
            time = time*0.8f;
            generador.SetMaxTime(time);
        }
    }

}
