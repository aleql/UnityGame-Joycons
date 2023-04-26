using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject canvasCalibration;
    public GameObject canvasStartPoint;
    public GameObject Tutorial;
    public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Time.timeScale = 1;
        SceneManager.LoadScene("FlappyBirdType");
    }

    public void MainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
