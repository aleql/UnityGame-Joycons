using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTutorial : MonoBehaviour
{
    [SerializeField] public float playerSpeed = 10.0f;
    [SerializeField] public float jumpPower = 5.0f;
    [SerializeField] public Animator animator;
    [SerializeField] public bool grounded = false;
    
    [SerializeField] public ShootFire shootfire;
    
    public Rigidbody2D _playerRigidbody;

    public float maxEnergy = 10f;
    public float currentEnergy;

    [SerializeField] public float timeShoot = 0f;

    public Healthbar energybar;

    public bool canMove;

    [SerializeField] public Vector2 reboundSpeed;
    // Start is called before the first frame update
    void Start()
    {
        energybar.SetMaxHealth(maxEnergy);
        currentEnergy = maxEnergy/2;
        SetEnergy(currentEnergy);

        _playerRigidbody = GetComponent<Rigidbody2D>();
        if (_playerRigidbody == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
    }

    
    public void SetEnergy(float energy){
        currentEnergy = energy;
        energybar.SetHealth(currentEnergy);
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        IsGrounded();
        
        playerSpeed = RunnerManager.speed*2;
        
        timeShoot -= Time.deltaTime;
    }

    public bool IsGrounded()
    {
        grounded = Boots.IsGrounded;
        animator.SetBool("IsJumping", !grounded);
        return Boots.IsGrounded;
    }

    public void Shoot(){
        if(reloadShoot()){
            timeShoot = 2f;
            shootfire.Shoot();
        }
    } 

    public void Jump(){
        _playerRigidbody.velocity = new Vector2( 0, jumpPower);
        animator.SetBool("IsJumping", true);
    }


    public void Rebound(Vector2 PointHit){
        Debug.Log(PointHit);
         _playerRigidbody.velocity = new Vector2(- reboundSpeed.x * PointHit.x , reboundSpeed.y);
    }

    public bool reloadShoot(){
        if(timeShoot<0){
            return true;
        }
        return false;
    }
}
