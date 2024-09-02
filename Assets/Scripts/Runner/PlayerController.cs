using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private List<Joycon> joycons;
    public float[] stick;
    public int jc_1 = 0;
    public int jc_2 = 1;
    
    StreamWriter sw;

    Joycon joy_left;
    Joycon joy_right;
    string path;

    private float startTime;

    private float angle_left;
    private float angle_right;
    private float prev_angle_left;
    private float prev_angle_right;

    [SerializeField] private Vector3 prevAccelLeft;
    [SerializeField] private Vector3 prevAccelRight;
    
    [SerializeField] private float deltaAngleLeft;
    [SerializeField] private float deltaAngleRight;

    public GameObject cube_left;
    public GameObject cube_right;

    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float jumpPower = 50.0f;
    [SerializeField] private Animator animator;
    [SerializeField] private bool grounded = false;
    
    [SerializeField] private ShootFire shootfire;

    [SerializeField] private Rigidbody2D _playerRigidbody;

    public float maxEnergy = 10f;
    public float currentEnergy;

    [SerializeField] private float timeShoot = 0f;

    public Healthbar energybar;
    //public GameObject energybar;

    public bool canMove;
    [SerializeField] public bool invincibleFrames;

    [SerializeField] private Vector2 reboundSpeed;

    [SerializeField] private float magCompare; 
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        invincibleFrames = false;
        magCompare = 0.1f;
        if (energybar == null)
        {
            energybar = GameObject.Find("Canvas/PlayPanel/TopPanel/Energybar").GetComponent<Healthbar>();
        }

        cube_left = GameObject.Find("Left");
        cube_right = GameObject.Find("Right");
        startTime = Time.time;
        Time.timeScale = 0.8f;
        energybar.SetMaxHealth(maxEnergy);
        currentEnergy = maxEnergy/2;

        _playerRigidbody = GetComponent<Rigidbody2D>();
        if (_playerRigidbody == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
        
        startTime = Time.time;
        joycons = JoyconManager.Instance.j;

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

        try
        {
            Debug.Log(Application.persistentDataPath);
            string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = "Angulos-" + timestamp + ".csv";
            path = Application.persistentDataPath +"/"+ fileName;
            sw = File.CreateText(path);
            sw.WriteLine("Tiempo;Angulo_izq;DeltaAngulo_izq;Angulo_der;DeltaAngulo_der;puntaje");
            PlayerPrefs.SetString("Path", path);
        }
        catch (System.Exception)
        {
            Debug.Log("No se pudo escribir el archivo en el sistema");
        }
    }

    
    public void SetEnergy(float energy){
        energybar.SetHealth(currentEnergy);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (RunnerController.Instance.currentGameState == RunnerController.RunnerGameStates.Playing && joycons != null && joycons.Count >= 0)
        {
            magCompare = RunnerManager.Mag;
            Quaternion orient_left = joy_left.GetVector();
            Quaternion orient_right = joy_right.GetVector();

            Vector3 accelLeft = joy_left.GetAccel();

            Vector3 deltaAccelLeft = accelLeft - prevAccelLeft;

            prevAccelLeft = accelLeft;

            Vector3 accelRight = joy_right.GetAccel();

            Vector3 deltaAccelRight = accelRight - prevAccelRight;

            prevAccelRight = accelRight;

            float MagLeft = deltaAccelLeft.magnitude;
            float MagRight = deltaAccelRight.magnitude;

            cube_left.transform.rotation = orient_left;
            cube_left.transform.Rotate(90,0,0,Space.World);
            cube_right.transform.rotation = orient_right;
            cube_right.transform.Rotate(90,0,0,Space.World);
            
            angle_right = Vector3.Angle(cube_right.transform.forward*-1, Vector3.up);
            angle_left = Vector3.Angle(cube_left.transform.forward*-1, Vector3.up);
            
            deltaAngleLeft = angle_left - prev_angle_left;
            deltaAngleRight = angle_right - prev_angle_right;

            if (IsGrounded() && Jumped() && MagLeft>magCompare && MagRight>magCompare){
                Debug.Log("Jump");
                Jump();
                animator.SetBool("IsJumping", true);
            }

            if(canMove){
                MovePlayer();
            }

            if(reloadShoot() && MagLeft>magCompare && MagRight>magCompare && Shoot()){
                shootfire.Shoot();
            }

            EnergyDecrease();
            SetEnergy(currentEnergy);
            Debug.Log(currentEnergy);
            
            playerSpeed = RunnerManager.speed*2;

            timeShoot -= Time.deltaTime;

            prev_angle_right = angle_right;
            prev_angle_left = angle_left;

            float gameTime = Time.time - startTime;
            string text = gameTime.ToString("F2")
            +";"
            +(180-angle_left).ToString()
            +";"
            +deltaAngleLeft.ToString()
            +";"
            +(180-angle_right).ToString()
            +";"
            +deltaAngleRight.ToString()
            +";"
            +PuntajeCanvas.puntaje;
            sw.WriteLine(text);
        }
    }

    private void EnergyDecrease(){
        if(currentEnergy>maxEnergy/2){
            currentEnergy -= 1.5f*Time.deltaTime;
        }
        else if(currentEnergy>0){
            currentEnergy -= 0.5f*Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        grounded = Boots.IsGrounded;
        animator.SetBool("IsJumping", !grounded);
        return Boots.IsGrounded;
    }

    private bool Shoot(){
        if(deltaAngleLeft<-10 && deltaAngleRight<-10){
            timeShoot = 2;
            return true;
        }
        return false;
    } 

    private bool Jumped(){
        if(deltaAngleLeft>10 && deltaAngleRight>10){
            return true;
        }
        return false;
    } 

    private void Jump() => _playerRigidbody.velocity = new Vector2( 0, jumpPower);

    private void MovePlayer()
    {
        if((deltaAngleLeft > 0 && deltaAngleRight<0) || (deltaAngleLeft<0 && deltaAngleRight>0)){
            var horizontalInput = (Mathf.Abs(deltaAngleLeft) + Mathf.Abs(deltaAngleRight));
            if(currentEnergy <maxEnergy){
                currentEnergy += 0.08f/magCompare*Mathf.Abs(horizontalInput)*Time.deltaTime;
            }
        }
        if(currentEnergy > maxEnergy/5){
            animator.SetFloat("Speed", 1);
        }
        else{
            animator.SetFloat("Speed", 0);
        }
        _playerRigidbody.velocity = new Vector2(currentEnergy/maxEnergy * playerSpeed - 3*playerSpeed/4, _playerRigidbody.velocity.y);
    }

    public void Rebound(Vector2 PointHit){
        Debug.Log(PointHit);
         _playerRigidbody.velocity = new Vector2(- reboundSpeed.x * PointHit.x , reboundSpeed.y);
    }

    private bool reloadShoot(){
        if(timeShoot<0){
            return true;
        }
        return false;
    }
}
