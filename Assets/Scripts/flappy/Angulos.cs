using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Angulos : MonoBehaviour
{
    public static int angle_der;
    public static int angle_izq;
    // Start is called before the first frame update
    void Start()
    {
        angle_der = 0;
        angle_izq = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = angle_der.ToString();
        transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = angle_izq.ToString();
    }
}
