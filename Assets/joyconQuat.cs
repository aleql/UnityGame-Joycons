using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joyconQuat : MonoBehaviour
{
    private List<Joycon> joycons;
    public int jc_1 = 0;
    public int jc_2 = 1;
    Joycon joy_left;
    Joycon joy_right;
    Quaternion orient_left;
    Quaternion orient_right ;
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
    void Update()
    {
        orient_left = joy_left.GetVector();
        orient_right = joy_right.GetVector();

        gameObject.transform.rotation = orient_left;
        gameObject.transform.Rotate(90,0,0,Space.World);

        //Debug.Log(orient_left*Vector3.up);
    }
}
