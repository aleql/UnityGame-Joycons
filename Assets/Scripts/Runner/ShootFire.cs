using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFire : MonoBehaviour
{
    [SerializeField] private Transform controllerShoot;

    [SerializeField] private GameObject fire;

    [SerializeField] private Animator animator;

    
    // Update is called once per frame

    public void Shoot(){
        StartCoroutine(Attack());
        GameObject newFire = Instantiate(fire, controllerShoot.position, controllerShoot.rotation);
        Destroy(newFire, 2);
    }

    private IEnumerator Attack()
    {
        
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Attack", false);
    }
}
