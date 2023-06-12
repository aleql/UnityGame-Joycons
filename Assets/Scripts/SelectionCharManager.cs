using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class SelectionCharManager : MonoBehaviour
{
    private int index;
    
    [SerializeField] private Image imageGame;

    [SerializeField] private TextMeshProUGUI TextName;

    public SelectCharManager selectCharManager;

    private void Start() {
        index = PlayerPrefs.GetInt("CharIndex", 0);

        UpdateScreen();
    }

    private void UpdateScreen() {
        PlayerPrefs.SetInt("CharIndex", index);
        imageGame.sprite = selectCharManager.players[index].image; 
        TextName.text = selectCharManager.players[index].NamePlayer;
    }

    public void NextGame(){
        if(index == selectCharManager.players.Count-1){
            index = 0;
        }
        else{
            index +=1;
        }
        UpdateScreen();
    }
    
    public void PreviusGame(){
        if(index == 0){
            index = selectCharManager.players.Count-1;
        }
        else{
            index -=1;
        }
        UpdateScreen();
    }

    public void StartGame() {
        SceneManager.LoadScene("RunnerTutorial");
    }
}
