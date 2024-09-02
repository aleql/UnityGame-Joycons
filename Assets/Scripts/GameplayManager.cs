using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FlappyBirdController;

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

    private float puntajes = 1600;



    // Start is called before the first frame update
    void Start()
    {
        Gravity = 8f;
        speed = 5f;
        time = 5f;
        //AudioManager.Instance.SwitchToGameplayMusic();

        system.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        system.gameObject.SetActive(false);
        system2.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        system2.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (FlappyBirdController.Instance.currentGameState == FlappyBirdGameStates.Playing)
        {
            if (PuntajeCanvas.puntaje >= puntajes)
            {
                Gravity = Gravity * 1.2f; //
                puntajes = puntajes * 2.0f; //

                LevelUpEffects();

                speed = speed * 1.5f; // 1.1
                time = time * 0.98f; // 0.8
                timeApple = timeApple * 1.05f;
                generador.SetMaxTime(time);
                generadorManzanas.SetMaxTime(timeApple);
            }
        }
    }

    public void LevelUpEffects()
    {
        Vector3 position = BirdPlayerController.Instance.transform.position;
        position += new Vector3(0f, 3f, 0f);

        ScoreManager.Instance.Score("LEVEL UP!", position);
        StartCoroutine(FireworksSFX());
    }

    public IEnumerator FireworksSFX()
    {
        system.gameObject.SetActive(true);
        system.Play(true);

        system2.gameObject.SetActive(true);
        system2.Play(true);
        yield return new WaitForSeconds(5);
        system.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        system.gameObject.SetActive(false);
        system2.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        system2.gameObject.SetActive(false);


    }


}
