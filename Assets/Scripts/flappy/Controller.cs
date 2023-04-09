using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Controller : MonoBehaviour
{
    private List<Joycon> joycons;
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_1 = 0;
    public int jc_2 = 1;
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;
    public bool Calibration;
    public bool StartGame;  

    private Vector3 direction;
    public float inclination;
    public GameOver gameOver;
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

    private float prev_angle_left;
    private float prev_angle_right;

    void Start()
    {   
        startTime = Time.time;
        Calibration = false;
        StartGame = true;
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        joycons = JoyconManager.Instance.j;
        inclination = 0f;
        Vector3 position = transform.position;
        position.y = 2f;
        direction = Vector3.zero;

        prev_angle_left = 0f;
        prev_angle_right = 0f;

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
            //File.WriteAllText(path, "Tiempo;Angulo_izq;DeltaAngulo_izq;Angulo_der;DeltaAngulo_der;puntaje;manzanas\n");
            sw = File.CreateText(path);
            sw.WriteLine("Tiempo;Angulo_izq;DeltaAngulo_izq;Angulo_der;DeltaAngulo_der;puntaje;manzanas");
        }
        catch (System.Exception)
        {
            Debug.Log("No se pudo escribir el archivo en el sistema");
        }
    }

    void Update() {
        if (joycons.Count >= 0)
        {
            if (Calibration == false && joy_left.GetButton(Joycon.Button.SHOULDER_2) && joy_right.GetButton(Joycon.Button.SHOULDER_2)){
                joy_right.Recenter();
                joy_left.Recenter();
                Calibration = true;
                gameOver.CalibrationEnd();
                StartGame = false;
            }
            if (StartGame == false && joy_left.GetButton(Joycon.Button.SHOULDER_1) && joy_right.GetButton(Joycon.Button.SHOULDER_1)){
                gameOver.StartPointEnd();
                StartGame = true;
            }
            if ( joy_left.GetButtonDown(Joycon.Button.DPAD_DOWN) && joy_right.GetButtonDown(Joycon.Button.DPAD_DOWN)){
                gameOver.Pause();
            }
        }
    }

    void FixedUpdate()
    {   
        if (joycons.Count >= 0)
        {
            Quaternion orient_left = joy_left.GetVector();
            Quaternion orient_right = joy_right.GetVector();
            
            float angle_right = (orient_right.eulerAngles.y-180);
            float angle_left = (orient_left.eulerAngles.y-180);

            Angulos.angle_der =((int) angle_right)+90;
            Angulos.angle_izq = -(((int) angle_left)-90);

            float var_angle_der = -(angle_right - prev_angle_right);
            float var_angle_izq = angle_left - prev_angle_left;

            prev_angle_left = angle_left;
            prev_angle_right = angle_right;
            rot_der.transform.localEulerAngles = new Vector3(0,0,angle_right);
            rot_izq.transform.localEulerAngles = new Vector3(0,0,angle_left);
        
            Grad_force_der = var_angle_der * Mathf.Min(180,(angle_right+90))/300 * strength * Time.deltaTime;
            Grad_force_izq = var_angle_izq * Mathf.Min(180,(-angle_left+90))/300 * strength * Time.deltaTime;

            if(Grad_force_der <0) Grad_force_der = 0; //esta subiendo el brazo, no recibe castigo
            if(Grad_force_izq <0) Grad_force_izq = 0; //esta subiendo el brazo, no recibe castigo

            if(Grad_force_der > 1+Grad_force_izq){ //si un brazo realiza mas fuerza que el otro
                direction.x -= (Grad_force_der-Grad_force_izq);
            }
            else if(Grad_force_izq > 1+Grad_force_der){
                direction.x += 2*(Grad_force_izq-Grad_force_der);
            }
            else{ //amortigua la inclinación si esta balanceado
                direction.x = direction.x*0.9f;
            }
            if(Mathf.Abs(direction.x) <0.2){
                direction.x = 0;
            }
            direction.y += (Grad_force_der+Grad_force_izq)/2;
            
            
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
            sw.WriteLine( 
            gameTime.ToString("F2")
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
            +";manzanas");
        }
    }

    
    private void OnCollisionEnter(Collision other) {
        sw.Close();
        PlayerPrefs.SetInt("Puntaje", PuntajeCanvas.puntaje);
        gameOver.Perdiste();
    }
}
