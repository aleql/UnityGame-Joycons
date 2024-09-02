using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour, IPickable, IDamagable
{
    
    public int maxHealth = 5;
    public int currentHealth;
    
    public Canvas canvasController;

    //public Healthbar healthbar;
    [SerializeField] private HealthPanelUI _healthPanelUI;
    [SerializeField] private SimpleFlash _simpleFlash;
    private PlayerController playerMove;

    [SerializeField] private float controlTimeLose;

    public bool playerControl;

    private Rigidbody2D _playerRigidbody;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
        //canvasController = GameObject.Find("LogicaCanvas").GetComponent<Canvas>();
        if (_healthPanelUI == null)
        {
            _healthPanelUI = GameObject.Find("Canvas/PlayPanel/TopPanel/HealthPanel").GetComponent<HealthPanelUI>();
        }
        if (_simpleFlash == null)
        {
            _simpleFlash = GetComponent<SimpleFlash>();
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(7, 6, false);
        _playerRigidbody = GetComponent<Rigidbody2D>();

        _healthPanelUI.SetHealth(maxHealth);
        currentHealth = maxHealth;
        
        playerMove = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        playerControl = true;
    }

    public void SetHealth(int health){
        currentHealth = health;
        _healthPanelUI.SetHealth(currentHealth);
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
        if (!playerMove.invincibleFrames)
        {
            currentHealth -= damage;
            playerControl = false;
            Debug.Log("DISABLED");
            SetHealth(currentHealth);

            // GAME OVER
            if (currentHealth <= 0)
            {
                PlayerPrefs.SetInt("Puntaje", PuntajeCanvas.puntaje);
                animator.SetTrigger("Die");
                _playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                Physics2D.IgnoreLayerCollision(7, 8, true);
                Physics2D.IgnoreLayerCollision(7, 6, true);
                if (RunnerController.Instance.currentGameState == RunnerController.RunnerGameStates.Playing)
                {
                    RunnerController.Instance.NextGameState();
                }
            }
            // STIL GOING
            else
            {
                _simpleFlash.Flash();
                animator.SetTrigger("Hit");
                StartCoroutine(LoseControl());
                StartCoroutine(HitEnemy());
                playerMove.Rebound(position);
                HapticsControllerRunner.Instance.OnDamageReceived();
            }
        }
    }

    private IEnumerator LoseControl()
    {
        playerMove.canMove = false;
        playerMove.invincibleFrames = true;
        yield return new WaitForSeconds(controlTimeLose);
        playerMove.canMove = true;
        playerMove.invincibleFrames = false;
        playerControl = true;
        Debug.Log("ENABLED");

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
