using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health){
        slider.value = health;
    }

    public void SetMaxHealth(float health){
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health){
        slider.value = health/20;
        Debug.Log(slider.value);
    }
}
