using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(other.gameObject);
    }
}
