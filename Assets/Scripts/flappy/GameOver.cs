using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject canvasPerdiste;
    public GameObject canvasCalibration;
    public GameObject canvasStartPoint;
    public GameObject ObstaculoGen;
    public GameObject canvasPuntaje;
    public GameObject canvasAngulo;
    public GameObject canvasPause;
    public GameObject canvasManzanas;

    private bool pause;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Perdiste(){
        SceneManager.LoadScene(3);
    }


    public void CalibrationEnd(){
        canvasCalibration.SetActive(false);
        canvasStartPoint.SetActive(true);
    }

    public void StartPointEnd(){
        canvasStartPoint.SetActive(false);
        ObstaculoGen.SetActive(true);
        canvasPuntaje.SetActive(true);
        canvasAngulo.SetActive(true);
        canvasManzanas.SetActive(true);
        Time.timeScale = 1;
    }

    public void Pause(){
        pause = !pause;
        if(pause){
            canvasPause.SetActive(true);
            Time.timeScale = 0;
        }
        else{
            canvasPause.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void MainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
