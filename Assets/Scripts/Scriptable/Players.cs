using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Players", menuName = "Pruebas/Players", order = 1)]
public class Players : ScriptableObject
{
    public Sprite image;
    public string NamePlayer;
    public GameObject player;
}