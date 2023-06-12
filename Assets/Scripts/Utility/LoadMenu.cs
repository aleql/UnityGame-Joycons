using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour
{
    public string load = "MainMenu";
    IEnumerator Start()
    {
        // Wait for the loading scene manager to start
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(load);
    }
}
