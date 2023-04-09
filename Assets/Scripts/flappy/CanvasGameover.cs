using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class CanvasGameover : MonoBehaviour
{


    public void Reiniciar(){
        SceneManager.LoadScene(2);
    }

    void Start() {
    }

    void update(){
        int puntaje = 2;
        Debug.Log(puntaje);
        GetComponent<TextMeshProUGUI>().text = puntaje.ToString();
    }
}
