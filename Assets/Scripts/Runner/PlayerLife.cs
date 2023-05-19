using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour, IPickable, IDamagable
{
    
    public int maxHealth =10;
    public int currentHealth;
    
    public Canvas canvasController;

    public Healthbar healthbar;

    private PlayerController playerMove;

    [SerializeField] private float controlTimeLose;

    private Rigidbody2D _playerRigidbody;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(7, 6, false);
        _playerRigidbody = GetComponent<Rigidbody2D>();
        healthbar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        
        playerMove = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    public void SetHealth(int health){
        currentHealth = health;
        healthbar.SetHealth(currentHealth);
    }


    public void Hit(int damage)
    {
        // Update health var
        currentHealth -= damage;
        SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            PlayerPrefs.SetInt("Puntaje", PuntajeCanvas.puntaje);
            animator.SetTrigger("Die");
        }
    }

    public void Dead(){
        canvasController.Perdiste();
    }

    public void Hit(int damage, Vector2 position)
    {
        // Update health var
        currentHealth -= damage;
        SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            PlayerPrefs.SetInt("Puntaje", PuntajeCanvas.puntaje);
            animator.SetTrigger("Die");
            _playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll; 
            Physics2D.IgnoreLayerCollision(7, 8, true);
            Physics2D.IgnoreLayerCollision(7, 6, true);
        }
        else{
            animator.SetTrigger("Hit");
            StartCoroutine(LoseControl());
            StartCoroutine(HitEnemy());
            playerMove.Rebound(position);
        }
    }

    private IEnumerator LoseControl()
    {
        playerMove.canMove = false;
        yield return new WaitForSeconds(controlTimeLose);
        playerMove.canMove = true;
    }

    private IEnumerator HitEnemy()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        yield return new WaitForSeconds(controlTimeLose+1);
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }


    public void Pick(int health){
        currentHealth += health;

        if (currentHealth >= 10)
        {
            SetHealth(10);
        }
        else{
            SetHealth(currentHealth);
        }
    }
}
