using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IPickable, IDamagable
{
    
    public int maxHealth =10;
    public int currentHealth = 10;
    
    public Canvas canvasController;

    public Healthbar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        healthbar.SetMaxHealth(currentHealth);
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
            canvasController.Perdiste();
        }
    }

    public void Hit(int damage, Vector2 position)
    {
        // Update health var
        currentHealth -= damage;
        SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            PlayerPrefs.SetInt("Puntaje", PuntajeCanvas.puntaje);
            canvasController.Perdiste();
        }
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
