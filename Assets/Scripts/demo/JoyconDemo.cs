using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyconDemo : MonoBehaviour {

	private List<Joycon> joycons;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_ind = 0;
    public Quaternion orientation;

	//private Quaternion orientationAdjustment = Quaternion.Euler(0, 0, 0);
	

    void Start ()
    {	
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
		if (joycons.Count < jc_ind+1){
			Destroy(gameObject);
		}
	}
	

    // Update is called once per frame
    void FixedUpdate () {
		// make sure the Joycon only gets checked if attached
		

		if (joycons.Count > 0)
        {
			Joycon j = joycons [jc_ind];
			// GetButtonDown checks if a button has been pressed (not held)
            
			if (j.GetButtonDown (Joycon.Button.DPAD_DOWN)) {
				Debug.Log ("Rumble");
				j.SetRumble (160, 320, 0.6f, 200);
				j.Recenter();
			}

            stick = j.GetStick();

            gyro = j.GetGyro();

            accel = j.GetAccel();

            orientation = j.GetVector();
			


			if (j.GetButton(Joycon.Button.DPAD_UP)){
				
				Debug.Log(transform.rotation.eulerAngles);	
				gameObject.GetComponent<Renderer>().material.color = Color.red;
				string tex = " x: " +  
				//"\n gyro: "  + gyro[0].ToString() + 
				accel[0].ToString() + 
				" orient: " + orientation[0].ToString()+
				" y: " +  
				//"\n gyro: "  + gyro[1].ToString() + 
				accel[1].ToString() + 
				" orient: " + orientation[1].ToString()+
				" z: "+
				//"\n gyro: " + gyro[2].ToString() + 
				accel[2].ToString() +// + 
				" orient: "+ orientation[2].ToString();
				Debug.Log (tex, gameObject);
				
				
			} 
			else{
				gameObject.GetComponent<Renderer>().material.color = Color.blue;

			}

            transform.rotation = orientation;
			gameObject.transform.Rotate(90,0,0,Space.World);

			
			//Quaternion actual_orientation = new Quaternion(orientation.x, -orientation.z, orientation.y, -orientation.w);
			Debug.DrawRay(gameObject.transform.position, gameObject.transform.up, Color.yellow);
            // gameObject.transform.eulerAngles =new Vector3(orientation.eulerAngles.x, orientation.eulerAngles.z, -orientation.eulerAngles.y);//new Quaternion(-orientation.x,-orientation.z,-orientation.y,orientation.w);
			// if (j.GetButton(Joycon.Button.DPAD_RIGHT)){
			// 	save_orientation = gameObject.transform.rotation;
			// }

			// if (j.GetButton(Joycon.Button.DPAD_LEFT)){
			// 	actual_orientation = gameObject.transform.rotation;
			// 	float angle = Quaternion.Angle(save_orientation, actual_orientation);
			// 	Debug.Log("angulo entre 2 posiciones: "+angle.ToString());
			// }

        }
    }
}