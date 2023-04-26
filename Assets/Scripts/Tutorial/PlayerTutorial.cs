using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorial : MonoBehaviour
{
    private List<Joycon> joycons;
    
    public int jc_1 = 0;
    public int jc_2 = 1;
    
    Joycon joy_left;
    Joycon joy_right;

    public bool Calibration, Tutorialbool, StartGame;

    public CanvasController canvasController;

    // Start is called before the first frame update
    void Start()
    {
        Calibration=false;
        Tutorialbool=true;
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
            if (Calibration == false && joy_left.GetButton(Joycon.Button.SHOULDER_2) && joy_right.GetButton(Joycon.Button.SHOULDER_2)){
                joy_right.Recenter();
                joy_left.Recenter();
                Calibration = true;
                canvasController.CalibrationEnd();
                Tutorialbool = false;
            }
            if (Tutorialbool == false && joy_left.GetButton(Joycon.Button.SHOULDER_1) && joy_right.GetButton(Joycon.Button.SHOULDER_1)){
                Tutorialbool = true;
                canvasController.TutorialEnd();
                StartGame = false;
            }

            if (StartGame == false && joy_left.GetButton(Joycon.Button.SHOULDER_2) && joy_right.GetButton(Joycon.Button.SHOULDER_2)){
                canvasController.StartPointEnd();
            }
        }
    }
}
