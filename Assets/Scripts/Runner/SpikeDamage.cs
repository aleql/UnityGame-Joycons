using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Player")){
        if (collider.gameObject.TryGetComponent(out IDamagable pickable))
        {   
            // Health the object that collide with me
            pickable.Hit(damage, collider.GetContact(0).normal);
        }
        }
    }
}
