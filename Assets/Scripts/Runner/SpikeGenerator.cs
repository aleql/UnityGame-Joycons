using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    public float tiempoMax = 1;

    private float tiempoInicial = 0;

    [SerializeField] private int maxRandom = 3;
    public GameObject spike;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject newObs = Instantiate(spike);
        //newObs.transform.position = transform.position +new Vector3(0,0,0);
        //Destroy(newObs, 8);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (RunnerController.Instance.currentGameState == RunnerController.RunnerGameStates.Playing)
        {
            if (tiempoInicial > tiempoMax)
            {
                int RandomOption = Random.Range(1, maxRandom);
                for (int i = 0; i < RandomOption; i++)
                {
                    GameObject newObs = Instantiate(spike);
                    newObs.transform.position = transform.position + new Vector3(i, 0, 0);
                    Destroy(newObs, 20);
                }
                tiempoInicial = 0;
            }
            else
            {
                tiempoInicial += Time.deltaTime;
            }
        }
    }

    public void SetMaxTime(float time){
        tiempoMax = time;
    }
}
