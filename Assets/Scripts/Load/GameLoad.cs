using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoad : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Wait for the loading scene manager to start
        yield return new WaitUntil(() => JoyconManager.Instance != null);
        
        SceneManager.LoadScene("MainMenu");
    }
}
