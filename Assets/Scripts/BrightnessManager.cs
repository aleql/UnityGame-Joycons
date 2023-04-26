using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BrightnessManager : SingletonPersistent<BrightnessManager>
{
    // Start is called before the first frame update
    
    [SerializeField]
    [Range(0f, 1f)]
    private float brightness;

    [SerializeField]
    public Image BrightnessPanel;

    void Start()
    {
        brightness = PlayerPrefs.GetFloat("brightness", 0.5f);
        BrightnessPanel.color = new Color(BrightnessPanel.color.r, BrightnessPanel.color.g, BrightnessPanel.color.b, brightness);
    }

    public void SetBrightness(float value){
        brightness = value;
        BrightnessPanel.color = new Color(BrightnessPanel.color.r, BrightnessPanel.color.g, BrightnessPanel.color.b, brightness);
    }
}
