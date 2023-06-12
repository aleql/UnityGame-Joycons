using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicialPos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int indexPlayer = PlayerPrefs.GetInt("CharIndex");
        Instantiate(SelectCharManager.Instance.players[indexPlayer].player, transform.position, Quaternion.identity);
    }
}
