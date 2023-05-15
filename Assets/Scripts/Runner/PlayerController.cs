using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerController : MonoBehaviour
{
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

    
    [SerializeField] private float deltaAngleLeft;
    [SerializeField] private float deltaAngleRight;

    public GameObject cube_left;
    public GameObject cube_right;

    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private Animator animator;
    [SerializeField] private bool grounded = false;
    
    [SerializeField] private ShootFire shootfire;
    
    private Rigidbody2D _playerRigidbody;

    public float maxEnergy = 10f;
    public float currentEnergy;

    [SerializeField] private float timeShoot = 2f;

    public Healthbar energybar;

    public bool canMove;

    [SerializeField] private Vector2 reboundSpeed;
    // Start is called before the first frame update
    void Start()
    {

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
            sw.WriteLine("Tiempo;Angulo_izq;DeltaAngulo_izq;Angulo_der;DeltaAngulo_der;puntaje;manzanas");
            
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

            if (IsGrounded() && Jumped()){
                Jump();
                animator.SetBool("IsJumping", true);
            }

            if(canMove){
                MovePlayer();
            }

            if(reloadShoot() && Shoot()){
                shootfire.Shoot();
            }

            EnergyDecrease();
            SetEnergy(currentEnergy);
            
            playerSpeed = RunnerManager.speed*2;

            timeShoot -= Time.deltaTime;

            prev_angle_right = angle_right;
            prev_angle_left = angle_left;
        }
    }

    private void EnergyDecrease(){
        if(currentEnergy>maxEnergy/2){
            currentEnergy -= 1.4f*Time.deltaTime;
        }
        else if(currentEnergy>0){
            currentEnergy -= 0.3f*Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        grounded = Boots.IsGrounded;
        animator.SetBool("IsJumping", !grounded);
        return Boots.IsGrounded;
    }

    private bool Shoot(){
        deltaAngleLeft = angle_left - prev_angle_left;
        deltaAngleRight = angle_right - prev_angle_right;

        if(deltaAngleLeft <-10 && deltaAngleRight<-10){
            timeShoot = 2;
            return true;
        }
        return false;
    } 

    private bool Jumped(){
        deltaAngleLeft = angle_left - prev_angle_left;
        deltaAngleRight = angle_right - prev_angle_right;

        if(deltaAngleLeft > 20 && deltaAngleRight>20){
            return true;
        }
        return false;
    } 

    private void Jump() => _playerRigidbody.velocity = new Vector2( 0, jumpPower);

    private void MovePlayer()
    {
        if((deltaAngleLeft > 0 && deltaAngleRight<0) || (deltaAngleLeft<0 && deltaAngleRight>0)){
            var horizontalInput = (Mathf.Abs(deltaAngleLeft) + Mathf.Abs(deltaAngleRight))/2;
            if(currentEnergy <maxEnergy){
                currentEnergy += 0.8f*Mathf.Abs(horizontalInput)*Time.deltaTime;
            }
        }
        if(currentEnergy > maxEnergy/5){
            animator.SetFloat("Speed", 1);
        }
        else{
            animator.SetFloat("Speed", 0);
        }
        _playerRigidbody.velocity = new Vector2(currentEnergy/maxEnergy * playerSpeed - playerSpeed/2, _playerRigidbody.velocity.y);
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
