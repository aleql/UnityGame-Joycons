using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MainMenu : MonoBehaviour
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

    [SerializeField]
    private int pos_left, pos_right;
    
    [SerializeField]
    private int initialPos;

    public GameObject[] buttons;
    
    public bool pressed_left = true;
    public bool pressed_right = true;

    [SerializeField] private float timePass;
    [SerializeField] public Healthbar loadbar;
    [SerializeField] private bool Complement_left = false;
    [SerializeField] private bool Complement_right = false;


    
    // Start is called before the first frame update


    void Start()
    {   
        loadbar.SetMaxHealth(2f);
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
        pos_left = -1;
        pos_right = -1;
        
        pressed_left = joy_left.GetButton(Joycon.Button.SHOULDER_2);
        pressed_right = joy_right.GetButton(Joycon.Button.SHOULDER_2);
    }

    // Update is called once per frame
    void Update()
    {   
        if(joy_right.GetButtonDown(Joycon.Button.SHOULDER_2) && joy_left.GetButtonDown(Joycon.Button.SHOULDER_2)){
            joy_right.Recenter();
            joy_left.Recenter();
        }
        if (joycons.Count >= 0)
        {
            Quaternion orient_left = joy_left.GetVector();
            Quaternion orient_right = joy_right.GetVector();

            cube_left.transform.rotation = orient_left;
            cube_left.transform.Rotate(90,0,0,Space.World);
            cube_right.transform.rotation = orient_right;
            cube_right.transform.Rotate(90,0,0,Space.World);

            Debug.DrawRay(cube_left.transform.position, cube_left.transform.forward*500, Color.blue);
            Debug.DrawRay(cube_right.transform.position, cube_right.transform.forward*500, Color.red);
        }
        else 
        {
            return;
        }
        
        PressBtnController(cube_left, 
        stick_left, 
        left_circle, 
        joy_left, 
        pos_left, 
        prev_left, 
        pressed_left, 
        SetPosLeft,
        SetPrevLeft,
        Complement_right,
        SetComplementLeft);
        if(joy_left.GetButtonDown(Joycon.Button.SHOULDER_2)){
            pressed_left = true;
        }
        else if(joy_left.GetButtonUp(Joycon.Button.SHOULDER_2)){
            pressed_left = false;
        }
        
        PressBtnController(cube_right, 
        stick_right, 
        right_circle, 
        joy_right, 
        pos_right, 
        prev_right, 
        pressed_right, 
        SetPosRight,
        SetPrevRight,
        Complement_left,
        SetComplementRight);
        if(joy_right.GetButtonDown(Joycon.Button.SHOULDER_2)){
            pressed_right = true;
        }
        else if(joy_right.GetButtonUp(Joycon.Button.SHOULDER_2)){
            pressed_right = false;
        }


    }

    public void PlayBtn()
    {
        Debug.Log("jugar presionado");
        SceneManager.LoadScene("Selection");
    }

    public void CalibrateBtn()
    {
        Debug.Log("calibrar presionado");
        SceneManager.LoadScene("Configure");
    }

    public void ExitBtn() 
    {
        Debug.Log("Salir presionado");
        Application.Quit();
    }

    public void BackBtn() 
    {
        Debug.Log("Volver presionado");
        SceneManager.LoadScene("MainMenu");
    }

    public void PressBtnController(GameObject cube, 
    float[] stick, 
    GameObject circle, 
    Joycon joy, 
    int pos, 
    float prev, 
    bool pressed, 
    Action<int> SetPos, 
    Action<float> SetPrev,
    bool Complement,
    Action<bool> SetComplement){
        RaycastHit hit;

        if (Physics.Raycast(cube.transform.position, cube.transform.forward,out hit, Mathf.Infinity))
        {   
            circle.transform.position = hit.point;
            Debug.Log(hit.collider);
            Debug.DrawRay(cube.transform.position, cube.transform.forward * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            GameObject colliderObject = hit.collider.gameObject;
            Button button = colliderObject.GetComponent<Button>();

            if(!pressed && button != null && joy.GetButton(Joycon.Button.SHOULDER_2)){
                button.onClick.Invoke();
            }

            if(!pressed && button != null){
                timePass += Time.deltaTime;
                SetComplement?.Invoke(true);
                if(timePass>2){
                    timePass=0;
                    SetComplement?.Invoke(false);
                    button.onClick.Invoke();
                }
            }
            else{
                SetComplement?.Invoke(false);
            }
            
            if(button == null && !Complement){
                timePass = 0;
            }
            loadbar.SetHealth(timePass);
            SetPos?.Invoke(-1);
        }
        else{
            stick = joy.GetStick();
            if(prev<0.7 && stick[1]>0.7){
                if(pos > 0){
                    pos -= 1;
                    SetPos?.Invoke(pos);
                }
                circle.transform.position = new Vector3(1920/2,1080/2-initialPos-300*pos,-0.5f);
            }
            if(prev>-0.7 && stick[1]<-0.7){
                if(pos < buttons.Length-1){
                    pos += 1;
                    SetPos?.Invoke(pos);
                }
                circle.transform.position = new Vector3(1920/2,1080/2-initialPos-300*pos,-0.5f);
            }
            prev = stick[1];
            SetPrev?.Invoke(prev);
            if(!pressed && joy.GetButton(Joycon.Button.SHOULDER_2) && pos>=0){
                buttons[pos].GetComponent<Button>().onClick.Invoke();
            }
        }
    }

    private void SetPosLeft(int pos)
    {
        pos_left = pos;
    }

    private void SetPosRight(int pos)
    {
        pos_right = pos;
    }

    private void SetPrevLeft(float prev)
    {
        prev_left = prev;
    }

    private void SetPrevRight(float prev)
    {
        prev_right = prev;
    }

    private void SetComplementLeft(bool data){
        Complement_left = data;
    }
    
    private void SetComplementRight(bool data){
        Complement_right = data;
    }
}
