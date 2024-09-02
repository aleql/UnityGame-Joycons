using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public GameObject floatingTextPrefab;
    //public Transform textSpawnLocation;

    private void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this;
        }
    }
    public void Score(string message, Vector3 position)
    {
        // Instantiate the floating text
        Vector3 randomJitter = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f), 0f);
        GameObject temp = Instantiate(floatingTextPrefab, position + randomJitter, Quaternion.identity, transform);
        temp.GetComponent<TextMeshProUGUI>().text = message; // Customize your message here

        // Optionally, adjust parameters like moveSpeed and fadeSpeed
        FloatingText floatingText = temp.GetComponent<FloatingText>();
        floatingText.moveSpeed = 3f;
        floatingText.fadeSpeed = 0.5f;
    }
}