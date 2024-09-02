using UnityEngine;

public class ObstacleBarrier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject obstacleGameObject = other.gameObject;
        if (obstacleGameObject.tag == "Obstacle")
        {
            Obstaculo obstaculo = obstacleGameObject.GetComponent<Obstaculo>();
            obstaculo.multiplicator += 2;
            Destroy(obstacleGameObject, 4f);
            StartCoroutine(obstaculo.FadeOutOpacity());
            obstaculo.setForDestruction = true;
        }
    }
}
