using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{
    
    public GameObject canvasPause;
    private bool pause;

    public void Pause(){
        // pause = !pause;
        // if(pause){
        //     canvasPause.SetActive(true);
        //     Time.timeScale = 0;
        // }
        // else{
        //     canvasPause.SetActive(false);
        //     Time.timeScale = 1;
        // }
    }

    
    public void Perdiste(){
        SceneManager.LoadScene("GameOver");
    }
}
