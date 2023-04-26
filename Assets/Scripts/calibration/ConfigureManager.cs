using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfigureManager : MonoBehaviour
{

    [SerializeField]
    private Toggle fullscreen;

    [SerializeField]
    private Slider volumen;

    [SerializeField]
    private Slider brightness;

    [SerializeField]
    private TMP_Dropdown quality;

    [SerializeField]
    private int qualityValue;

    [SerializeField]
    private TMP_Dropdown resolutionsDropDown;

    [SerializeField]
    private Resolution[] resolutions;


    // Start is called before the first frame update
    void Start()
    {
        //Fullscreen
        if (Screen.fullScreen)
        {
            fullscreen.isOn = true;
        }
        else
        {
            fullscreen.isOn = false;
        }

        //Volumen 
        volumen.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioManager.Instance.SetMaxVolume(volumen.value);

        //brillo 
        brightness.value = PlayerPrefs.GetFloat("brightness", 0.5f);
        BrightnessManager.Instance.SetBrightness(brightness.value);

        //calidad
        qualityValue = PlayerPrefs.GetInt("quality", 3);
        quality.value = qualityValue;
        UpdateQuality();

        //resolution

        UpdateResolution();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateFullScreen(bool FullScreen){
        Screen.fullScreen = FullScreen;
    }

    public void ChangeVolumen(float value){
        float sliderValue = value;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioManager.Instance.SetMaxVolume(sliderValue);
    }

    public void ChangeBrightness(float value){
        float sliderValue = value;
        PlayerPrefs.SetFloat("brightness", sliderValue);
        BrightnessManager.Instance.SetBrightness(sliderValue);
    }

    public void UpdateQuality(){
        QualitySettings.SetQualityLevel(quality.value);
        PlayerPrefs.SetInt("quality", quality.value);
        qualityValue = quality.value;
    }

    public void UpdateResolution()
    {
        resolutions = Screen.resolutions;
        resolutionsDropDown.ClearOptions();
        List<string> options = new List<string>();
        int actualResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);


            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                actualResolution = i;
            }

        }

        resolutionsDropDown.AddOptions(options);
        resolutionsDropDown.value = actualResolution;
        resolutionsDropDown.RefreshShownValue();


        //
        resolutionsDropDown.value = PlayerPrefs.GetInt("resolution", 0);
        //
    }

    public void ChangeResolution(int res)
    {
        //
        PlayerPrefs.SetInt("resolution", resolutionsDropDown.value);
        //


        Resolution resolution = resolutions[res];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
