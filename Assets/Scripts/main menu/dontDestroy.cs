using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour
{
    static dontDestroy instance;
 
     void Awake()
     {
        instance = this; // In first scene, make us the singleton.
        DontDestroyOnLoad(gameObject);
     }
}
