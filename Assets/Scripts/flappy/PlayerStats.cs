using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IPickable, IDamagable
{
    
    public int maxHealth = 5;
    public int currentHealth = 5;
    
    public Canvas canvasController;

    //public Healthbar healthbar;
    public HealthPanelUI healthPanelUI;
    // Start is called before the first frame update
    void Start()
    {
        SetHealth(currentHealth);
    }

    public void SetHealth(int health){
        currentHealth = health;
        healthPanelUI.SetHealth(currentHealth);
    }

    public void Hit(int damage)
    {
        // Update health var
        currentHealth -= damage;
        SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            PlayerPrefs.SetInt("Puntaje", PuntajeCanvas.puntaje);
            //canvasController.Perdiste();
            FlappyBirdController.Instance.NextGameState();
        }
        HapticsController.Instance.OnDamageReceived();
    }

    public void Hit(int damage, Vector2 position)
    {
        // Update health var
        currentHealth -= damage;
        SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            PlayerPrefs.SetInt("Puntaje", PuntajeCanvas.puntaje);
            //canvasController.Perdiste();
            FlappyBirdController.Instance.NextGameState();
        }
    }

    public void Pick(int health){
        currentHealth += health;

        if (currentHealth >= 5)
        {
            SetHealth(5);
        }
        else{
            SetHealth(currentHealth);
        }
    }
}
