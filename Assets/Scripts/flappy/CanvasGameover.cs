using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasGameover : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public TMP_InputField pathFile;

    public void Reiniciar(){
        SceneManager.LoadScene(PlayerPrefs.GetString("UltimoJuego"));
    }

    public void BackMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    void Start() {
        int puntaje = PlayerPrefs.GetInt("Puntaje");
        texto.text = "Puntaje Total: " + puntaje.ToString(); 
        string path = PlayerPrefs.GetString("Path");
        pathFile.text = path;
    }
}
