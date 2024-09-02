using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassfieldGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _grassFieldsPrefabs;
    public float tiempoMax;
    private float tiempoInicial = 0;

    void FixedUpdate()
    {
        if (RunnerController.Instance.currentGameState == RunnerController.RunnerGameStates.Playing)
        {
            if (tiempoInicial > tiempoMax)
            {
                int RandomOption = Random.Range(0, _grassFieldsPrefabs.Length);

                GameObject newObs = Instantiate(_grassFieldsPrefabs[RandomOption]);
                newObs.transform.position = transform.position + new Vector3(0, 0, 0);
                Destroy(newObs, 360);

                tiempoInicial = 0;
            }
            else
            {
                tiempoInicial += Time.deltaTime;
            }
        }
    }

    public void SetMaxTime(float time)
    {
        tiempoMax = time;
    }
}


