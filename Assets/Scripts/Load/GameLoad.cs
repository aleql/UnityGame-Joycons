using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoad : MonoBehaviour
{
    private float GameTime;
    // Start is called before the first frame update
    void Start()
    {
        GameTime = 0;
    }

    // Update is called once per frame

    void Update()
    {
        GameTime += Time.deltaTime;
        if(GameTime>1){
            SceneManager.LoadScene("MainMenu");
        }
    }
}
