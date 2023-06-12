using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerJoycon : MonoBehaviour
{
    private List<Joycon> joycons;
    
    public int jc_1 = 0;
    public int jc_2 = 1;
    
    Joycon joy_left;
    Joycon joy_right;

    public bool Tutorialbool, StartGame;

    public CanvasController canvasController;
    [SerializeField] private float angle_left;
    [SerializeField] private float angle_right;
    public float timeLeft=2f;

    public GameObject cube_left;
    public GameObject cube_right;

    public Healthbar loadbar;

    // Start is called before the first frame update
    void Start()
    {
        loadbar.SetMaxHealth(2);
        Tutorialbool=false;
        StartGame=true;
                
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
            Quaternion orient_left = joy_left.GetVector();
            Quaternion orient_right = joy_right.GetVector();

            cube_left.transform.rotation = orient_left;
            cube_left.transform.Rotate(90,0,0,Space.World);
            cube_right.transform.rotation = orient_right;
            cube_right.transform.Rotate(90,0,0,Space.World);
            
            angle_right = Vector3.Angle(cube_right.transform.forward*-1, Vector3.up);
            angle_left = Vector3.Angle(cube_left.transform.forward*-1, Vector3.up);

            if (Tutorialbool == false && joy_left.GetButton(Joycon.Button.SHOULDER_1) && joy_right.GetButton(Joycon.Button.SHOULDER_1)){
                Tutorialbool = true;
                canvasController.TutorialEnd();
                StartGame = false;
            }

            if (StartGame == false && joy_left.GetButton(Joycon.Button.SHOULDER_2) && joy_right.GetButton(Joycon.Button.SHOULDER_2)){
                canvasController.StartPointEnd();
            }
            if(StartGame == false && TimeToStart()){
                canvasController.StartPointEnd();
            }
        }
    }

    private bool TimeToStart(){
        if(angle_left>60 && angle_left<120 && angle_right>60 && angle_right<120){
            timeLeft -=Time.deltaTime;
        }
        else{
            timeLeft = 2f; 
        }
        loadbar.SetHealth(2-timeLeft);
        return timeLeft<0;
    }
}
