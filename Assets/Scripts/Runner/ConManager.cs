using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConManager : MonoBehaviour
{
    private List<Joycon> joycons;
    
    public int jc_1 = 0;
    public int jc_2 = 1;
    
    Joycon joy_left;
    Joycon joy_right;


    // Start is called before the first frame update
    void Start()
    {
                
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_2+1){
			Destroy(gameObject);
		}
        if(joycons[jc_1].isLeft){
            joy_left = joycons[jc_1];
            joy_right = joycons [jc_2];
        }
        else{
            joy_left = joycons[jc_2];
            joy_right = joycons [jc_1];
        }
    }

    // Update is called once per frame
    void Update() {
        if (joycons.Count >= 0)
        {
            if (joy_left.GetButton(Joycon.Button.SHOULDER_1) && joy_right.GetButton(Joycon.Button.SHOULDER_1)){
                SceneManager.LoadScene("RunnerType");
            }
        }
    }
}
