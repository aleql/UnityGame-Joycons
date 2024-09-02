using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuntajeCanvas : MonoBehaviour
{
    public static int puntaje
    {
        get
        {
            return _puntaje;
        }
        set
        {
            Debug.Log(value);
            _puntaje = value;
        }
    }
    private static int _puntaje;


    // Start is called before the first frame update
    void Start()
    {
        puntaje = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = puntaje.ToString();
    }
}
