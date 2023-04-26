using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.SwitchToGameplayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
