using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Calibration : MonoBehaviour
{
    private List<Joycon> joycons;
    public float[] stick_left;
    public float[] stick_right;
    public int jc_1 = 0;
    public int jc_2 = 1;

    Joycon joy_left;
    Joycon joy_right;

    public GameObject cube_left;
    public GameObject cube_right;

    public GameObject left_circle;
    public GameObject right_circle;
     
    public float prev_left;
    public float prev_right;

    public int pos_left, pos_right;

    public GameObject[] buttons;
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
        prev_left = 0;
        prev_right = 0;
        pos_left = 0;
        pos_right=0;
    }

    // Update is called once per frame
    void Update()
    {
        if (joycons.Count >= 2)
        {
            
            if (joy_left.GetButtonDown(Joycon.Button.SHOULDER_2) && joy_right.GetButtonDown(Joycon.Button.SHOULDER_2)){
                joy_right.Recenter();
                joy_left.Recenter();
            }
            Quaternion orient_left = joy_left.GetVector();
            Quaternion orient_right = joy_right.GetVector();

            cube_left.transform.rotation = orient_left;
            cube_left.transform.Rotate(90,0,0,Space.World);
            cube_right.transform.rotation = orient_right;
            cube_right.transform.Rotate(90,0,0,Space.World);

            Debug.DrawRay(cube_left.transform.position, cube_left.transform.forward*500, Color.blue);
            Debug.DrawRay(cube_right.transform.position, cube_right.transform.forward*500, Color.red);
        }

        RaycastHit hit_left;

        if (Physics.Raycast(cube_left.transform.position, cube_left.transform.forward,out hit_left, Mathf.Infinity))
        {   
            left_circle.transform.position = hit_left.point;

            Debug.DrawRay(cube_left.transform.position, cube_left.transform.forward * hit_left.distance, Color.yellow);

            GameObject colliderObject = hit_left.collider.gameObject;
            Button button = colliderObject.GetComponent<Button>();
            if(button != null && joy_left.GetButtonDown(Joycon.Button.SHOULDER_2)){
                button.onClick.Invoke();
            }
        }
        else{
            stick_left = joy_left.GetStick();
            if(prev_left<0.7 && stick_left[1]>0.7){
                left_circle.transform.position = new Vector3(1920/2,1080/2-300,-0.5f);
            }
            if(prev_left>-0.7 && stick_left[1]<-0.7){
                left_circle.transform.position = new Vector3(1920/2,1080/2-300,-0.5f);
            }
            prev_left = stick_left[1];
            if(joy_left.GetButtonDown(Joycon.Button.SHOULDER_2)){
                buttons[pos_left].GetComponent<Button>().onClick.Invoke();
            }
        }
        RaycastHit hit_right;
        if (Physics.Raycast(cube_right.transform.position, cube_right.transform.forward,out hit_right, Mathf.Infinity))
        {   
            right_circle.transform.position = hit_right.point;

            Debug.DrawRay(cube_right.transform.position, cube_right.transform.forward * hit_right.distance, Color.yellow);
            GameObject colliderObject = hit_right.collider.gameObject;
            Button button = colliderObject.GetComponent<Button>();
            if(button != null && joy_right.GetButtonDown(Joycon.Button.SHOULDER_2)){
                button.onClick.Invoke();
            }
        }
        else {
            stick_right = joy_right.GetStick();
            if(prev_right<0.7 && stick_right[1]>0.7){
                right_circle.transform.position = new Vector3(1920/2,1080/2-300,-0.5f);
            }
            if(prev_right>-0.7 && stick_right[1]<-0.7){
                right_circle.transform.position = new Vector3(1920/2,1080/2-300,-0.5f);
            }
            prev_right = stick_right[1];
            if(joy_right.GetButtonDown(Joycon.Button.SHOULDER_2)){
                buttons[pos_right].GetComponent<Button>().onClick.Invoke();
            }
        }


    }

    public void BackBtn() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}
