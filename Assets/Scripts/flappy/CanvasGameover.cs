using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class CanvasGameover : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public TMP_InputField pathFile;

    public void Reiniciar(){
        SceneManager.LoadScene("FlappyBirdType");
    }

    public void BackMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    void Start() {
        int puntaje = PlayerPrefs.GetInt("Puntaje");
        texto.text = "Puntaje Total: " + puntaje.ToString(); 
        string path = PlayerPrefs.GetString("Path");
        pathFile.text = path;
    }

    void Update(){  
    }
}
