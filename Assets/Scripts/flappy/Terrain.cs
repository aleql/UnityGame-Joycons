using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FlappyBirdController;

public class Terrain : MonoBehaviour
{
    public float velocidad;
    public Terrain m_terrain;  //reference to your terrain
    public float DrawDistance; // how far you want to be able to see the grass

    // Use this for initialization
    void Start()
    {
        if (m_terrain == null)
        {
            //m_terrain = GetComponent<Terrain>();
        }

        //m_terrain.GetComponentInChildren = DrawDistance;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FlappyBirdController.Instance.currentGameState == FlappyBirdGameStates.Playing)
        {
            if (transform.position.z < -210)
            {
                transform.position = transform.position + new Vector3(0, 0, 400);
            }
            transform.position += Vector3.back * velocidad * Time.deltaTime;
            velocidad = GameplayManager.speed;
        }
    }
}
