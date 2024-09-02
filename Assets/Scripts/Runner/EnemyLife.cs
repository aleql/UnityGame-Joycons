using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour, IDamagable
{

    [SerializeField] private int life = 1;

    public void Hit(int damage)
    {
        // Update health var
        life -= damage;

        if (life <= 0)
        {
            Destroy(gameObject);
            PuntajeCanvas.puntaje += 200;
            ScoreManager.Instance.Score("200", gameObject.transform.position);
            StartCoroutine(HapticsControllerRunner.Instance.DestroyEffect(gameObject.transform.position));
        }
    }
    
    public void Hit(int damage, Vector2 _)
    {
        // Update health var
        life -= damage;

        if (life <= 0)
        {
            Destroy(gameObject);
            PuntajeCanvas.puntaje += 200;
            ScoreManager.Instance.Score("200", gameObject.transform.position);
            StartCoroutine(HapticsControllerRunner.Instance.DestroyEffect(gameObject.transform.position));

        }
    }

}
