using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntajeJump : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") 
        {
            PlayerLife playerLife = other.gameObject.GetComponent<PlayerLife>();
            if (playerLife != null && playerLife.playerControl) 
            {
                PuntajeCanvas.puntaje += 200;
                ScoreManager.Instance.Score("200", other.gameObject.transform.position);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") 
        {
            PlayerLife playerLife = other.gameObject.GetComponent<PlayerLife>();
            if (playerLife != null && playerLife.playerControl)
            {
                PuntajeCanvas.puntaje += 200;
                ScoreManager.Instance.Score("200", other.gameObject.transform.position);
            }
        }
    }
}
