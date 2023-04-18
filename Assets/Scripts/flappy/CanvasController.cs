using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject canvasCalibration;
    public GameObject canvasStartPoint;
    public GameObject canvasPuntaje;
    public GameObject canvasAngulo;
    public GameObject canvasPause;

    public GameObject Tutorial;
    
    public GameObject Camera;

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
        Camera.transform.position = new Vector3(595, 2, -10);
        Tutorial.SetActive(true);
    }

    public void TutorialEnd(){
        Camera.transform.position = new Vector3(10, 3, -2);
        Tutorial.SetActive(false);
        canvasStartPoint.SetActive(true);
    }

    public void StartPointEnd(){
        canvasStartPoint.SetActive(false);
        canvasPuntaje.SetActive(true);
        canvasAngulo.SetActive(true);
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
