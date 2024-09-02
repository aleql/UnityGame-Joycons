using UnityEngine;

public class Puntaje : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") 
        {
            PuntajeCanvas.puntaje += 200;

            Vector3 position = BirdPlayerController.Instance.transform.position;
            position += new Vector3(0f, 3f, 0f);
            ScoreManager.Instance.Score("200", position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") 
        {
            PuntajeCanvas.puntaje += 200;

            Vector3 position = BirdPlayerController.Instance.transform.position;
            position += new Vector3(0f, 3f, 0f);
            ScoreManager.Instance.Score("200", position);

        }
    }
}
