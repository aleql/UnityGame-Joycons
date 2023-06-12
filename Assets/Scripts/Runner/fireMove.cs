using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireMove : MonoBehaviour
{

    [SerializeField] private float velocidad;
    [SerializeField] private int damage = 1;

    void Start(){
        velocidad = RunnerManager.speed*1.02f;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);       
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out IDamagable pickable))
        {   
            pickable.Hit(damage);
            Destroy(gameObject);
        }
    }
}
