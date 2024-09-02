using System.Collections;
using UnityEngine;
using static FlappyBirdController;

public class Obstaculo : MonoBehaviour
{
    public float velocidad;
    public float multiplicator;
    [SerializeField] private Renderer[] _renderers;
    public bool setForDestruction;
    public float fadeDuration = 2f; // Duración del fade en segundos
    
    void Start()
    {
        multiplicator = 0.0f;
        setForDestruction = false;

        foreach (var renderer in _renderers)
        {
            renderer.material.SetFloat("_Opacity", 0f);
        }


        StartCoroutine(FadeInOpacity());
    }

    // Update is called once per frame
    void Update()
    {
        if (FlappyBirdController.Instance.currentGameState == FlappyBirdGameStates.Playing)
        {
        transform.position += Vector3.back *velocidad*Time.deltaTime;
        velocidad = GameplayManager.speed + multiplicator;
        }
    }

    public IEnumerator FadeOutOpacity()
    {
        float currentTime = 0;

        while (currentTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);

            foreach (var renderer in _renderers)
            {
                renderer.material.SetFloat("_Opacity", alpha);
            }

            currentTime += Time.deltaTime;
            yield return null;
        }

        // Asegura que la opacidad sea 0 al final del proceso
        foreach (var renderer in _renderers)
        {
            if (renderer.material.HasProperty("Opacity"))
            {
                renderer.material.SetFloat("_Opacity", 0f); // Ajusta la opacidad a 0
            }
        }
    }

    public IEnumerator FadeInOpacity()
    {
        float currentTime = 0;

        while (currentTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / fadeDuration);

            foreach (var renderer in _renderers)
            {
                renderer.material.SetFloat("_Opacity", alpha);
            }

            currentTime += Time.deltaTime;
            yield return null;
        }

        // Asegura que la opacidad sea 1 al final del proceso
        foreach (var renderer in _renderers)
        {
            if (renderer.material.HasProperty("Opacity"))
            {
                renderer.material.SetFloat("_Opacity", 1f); // Ajusta la opacidad a 0
            }
        }
    }

}
