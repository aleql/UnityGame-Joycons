using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObs : MonoBehaviour
{
    
    public int damage = 1;

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Hola, choque con una rama");
        if (collider.TryGetComponent(out IDamagable pickable))
        {   
            // Health the object that collide with me
            pickable.Hit(damage);
            Destroy(gameObject);
        }
    }
}
