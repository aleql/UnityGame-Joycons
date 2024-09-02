using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class SelectionManager : MonoBehaviour
{
    private int index;
    
    [SerializeField] private Image imageGame;

    [SerializeField] private TextMeshProUGUI TextName;

    public SelectManager selectManager;

    private void Start() {
        index = PlayerPrefs.GetInt("GameIndex", 0);

        UpdateScreen();
        StartGame();
    }

    private void UpdateScreen() {
        PlayerPrefs.SetInt("GameIndex", index);
        imageGame.sprite = selectManager.scenes[index].image; 
        TextName.text = selectManager.scenes[index].sceneName;
    }

    public void NextGame(){
        if(index == selectManager.scenes.Count-1){
            index = 0;
        }
        else{
            index +=1;
        }
        UpdateScreen();
    }
    
    public void PreviusGame(){
        if(index == 0){
            index = selectManager.scenes.Count-1;
        }
        else{
            index -=1;
        }
        UpdateScreen();
    }

    public void StartGame() {
        PlayerPrefs.SetString("UltimoJuego", selectManager.scenes[1].scene);
        SceneManager.LoadScene(selectManager.scenes[1].scene);
    }
}
