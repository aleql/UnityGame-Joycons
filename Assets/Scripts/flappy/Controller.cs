using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Controller : MonoBehaviour
{
    private List<Joycon> joycons;
    public float[] stick;
    public int jc_1 = 0;
    public int jc_2 = 1;
    public float strength = 3f;
    public float gravity = -9.81f;
    
    private Vector3 direction;
    
    public Canvas canvasController;

    public GameObject ala_der;
    public GameObject rot_der;
    public GameObject ala_izq;
    public GameObject rot_izq;

    public float Grad_force_der;
    public float Grad_force_izq;

    StreamWriter sw;

    Joycon joy_left;
    Joycon joy_right;
    string path;

    private float startTime;

    private float angle_left;
    private float angle_right;
    private float prev_angle_left;
    private float prev_angle_right;

    public GameObject cube_left;
    public GameObject cube_right;

    void Start()
    {   
        gravity = -8f;
        startTime = Time.time;
        joycons = JoyconManager.Instance.j;
        Vector3 position = transform.position;
        position.y = 2f;
        direction = Vector3.zero;

        prev_angle_left = 90f;
        prev_angle_right = 90f;

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
        rot_der = transform.GetChild(0).gameObject;
        rot_izq = transform.GetChild(1).gameObject;

        try
        {
            Debug.Log(Application.persistentDataPath);
            string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = "Angulos-" + timestamp + ".csv";
            path = Application.persistentDataPath +"/"+ fileName;
            sw = File.CreateText(path);
            sw.WriteLine("Tiempo;Angulo_izq;DeltaAngulo_izq;Angulo_der;DeltaAngulo_der;puntaje;manzanas");
            
            PlayerPrefs.SetString("Path", path);
        }
        catch (System.Exception)
        {
            Debug.Log("No se pudo escribir el archivo en el sistema");
        }
    }

    void Update() {
        gravity = GameplayManager.Gravity;
        if (joycons.Count >= 0)
        {
            if ( joy_left.GetButtonDown(Joycon.Button.DPAD_DOWN) && joy_right.GetButtonDown(Joycon.Button.DPAD_DOWN)){
                canvasController.Pause();
            }
        }
    }

    void FixedUpdate()
    {   
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
            
            Angulos.angle_der = ((int) angle_right);
            Angulos.angle_izq = ((int) angle_left);

            float var_angle_der = -(angle_right - prev_angle_right);
            float var_angle_izq = -(angle_left - prev_angle_left);

            prev_angle_left = angle_left;
            prev_angle_right = angle_right;
            rot_der.transform.localEulerAngles = new Vector3(0,0,angle_right-90);
            rot_izq.transform.localEulerAngles = new Vector3(0,0,-angle_left+90);

            float log_der = Mathf.Pow(Mathf.Max(1,angle_right), 1f / 8f) -1;
            float log_izq = Mathf.Pow(Mathf.Max(1,angle_left), 1f/ 8f) -1;
            Grad_force_der = var_angle_der * log_der * strength * Time.deltaTime;
            Grad_force_izq = var_angle_izq * log_izq * strength * Time.deltaTime;

            if(Grad_force_der <0) Grad_force_der = 0; //esta subiendo el brazo, no recibe castigo
            if(Grad_force_izq <0) Grad_force_izq = 0; //esta subiendo el brazo, no recibe castigo

            if(Grad_force_der > 0.3+Grad_force_izq){ //si un brazo realiza mas fuerza que el otro
                direction.x -= 2f*(Grad_force_der-Grad_force_izq);
            }
            else if(Grad_force_izq > 0.3+Grad_force_der){
                direction.x += 2f*(Grad_force_izq-Grad_force_der);
            }
            else{ //amortigua la inclinación si esta balanceado
                direction.x = direction.x*0.9f;
            }
            if(Mathf.Abs(direction.x) <0.2){
                direction.x = 0;
            }
            direction.y += (Grad_force_der+Grad_force_izq);
            
            
            direction.y -= gravity*Time.deltaTime;
            if(direction.y >0 && transform.position.y>=6){
                direction.y = 0;
            }
            if(direction.y <0 && transform.position.y<=0){
                direction.y = 0;
            }
            if(direction.x >0 && transform.position.x>=15){
                direction.x = 0;
            }
            if(direction.x <0 && transform.position.x<=5){
                direction.x = 0;
            }
            transform.position += direction * Time.deltaTime;
            float gameTime = Time.time - startTime;
            string text = gameTime.ToString("F2")
            +";"
            +(Angulos.angle_izq).ToString()
            +";"
            +var_angle_izq.ToString()
            +";"
            +(Angulos.angle_der).ToString()
            +";"
            +var_angle_der.ToString()
            +";"
            +PuntajeCanvas.puntaje
            +";"
            +PuntajeManzana.manzanas;
            sw.WriteLine(text);
        }
    }



}
