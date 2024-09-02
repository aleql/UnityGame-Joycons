using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public float velocidad = 5;
    public float threshold = -24;
    public float resetDistance = 48;
    public bool ownSpeed = false;

    // Update is called once per frame
    void Update()
    {
        if (RunnerController.Instance.currentGameState == RunnerController.RunnerGameStates.Playing)
        {
            if (!ownSpeed)
            {
                velocidad = RunnerManager.speed;
            }

            transform.position += Vector3.left * velocidad * Time.deltaTime;
            if (transform.position.x <= threshold)
            {
                transform.position = transform.position + new Vector3(resetDistance, 0, 0);
            }
        }
    }
}
