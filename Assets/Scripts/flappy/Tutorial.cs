using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public GameObject arm_left;
    public GameObject arm_right;

    public GameObject rot_left;
    public GameObject rot_right;

    public float angle_left;
    public float angle_right;
    
    public TextMeshProUGUI text;

    public bool raise, fly, inc_left_up, inc_right_up, inc_left_down, inc_right_down, end; 
    // Start is called before the first frame update
    void Start()
    {
        angle_right = -40;
        angle_left = 40;
        raise = true;
        fly = true;
        inc_left_up = false;
        inc_right_up =false;
        
        inc_left_down = false;
        inc_right_down =false;
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        rot_left.transform.localEulerAngles = new Vector3(0,0,angle_left);
        rot_right.transform.localEulerAngles = new Vector3(0,0,angle_right);

        arm_left.transform.localEulerAngles = new Vector3(0.044f,-120 + angle_left,-179.988f);
        arm_right.transform.localEulerAngles = new Vector3(-0.044f,120 + angle_right,-179.988f);

        if(raise && angle_right < 40 && angle_left >-40){
            text.text = "Para volar ambos brazos deben subir y bajar al mismo tiempo";
            angle_right += 30*1f/120f;
            angle_left -= 30*1f/120f;
        }

        else if(fly && angle_right>-40 && angle_left<40){
            raise = false;
            angle_right -= 50*1f/120f;
            angle_left += 50*1f/120f;
            transform.position = transform.position + new Vector3(0,1f/60f,0);
        }
        else if(transform.localPosition.y > 0){
            text.text = "Estas bajo los efectos de la gravedad, por lo que siempre caes";
            transform.position = transform.position + new Vector3(0,-0.3f*1f/60f,0);
        }
        else if(!inc_left_up && !inc_left_down && !inc_right_up && !inc_right_down){
            fly =false;
            inc_left_up = true;
            inc_left_down = true;
        }

        if(!fly && !raise && inc_left_up && angle_right <40 && angle_left>0){
            text.text = "Si tienes mas impulso de un lado que de otro te inclinas";
            angle_right += 50*1f/160f;
            angle_left -= 25*1f/160f;
        }
        else if(!fly && !raise && inc_left_down && angle_right>-40){
            inc_left_up = false;
            angle_right -= 50*1f/160f;
            transform.position = transform.position + new Vector3(-1f*1f/60f,0,0);
        }
        else if(!fly && !raise && !inc_right_up && !inc_right_down){
            inc_left_down = false;
            inc_right_up = true;
            inc_right_down = true;
        }

        if(!fly && !raise && !inc_left_up && !inc_left_down && inc_right_up && angle_left >-40 && angle_right<0){
            angle_left -= 50*1f/160f;
            angle_right += 50*1f/160f;
        }
        else if(!fly && !raise && !inc_left_up && !inc_left_down && inc_right_down && angle_left<40){
            inc_right_up = false;
            angle_left += 50*1f/160f;
            transform.position = transform.position + new Vector3(1f*1f/60f,0,0);
        }
        else if(!fly && !raise && !inc_left_up && !inc_left_down){
            inc_right_down = false;
            end = true;
        }
        if(end){
            angle_right = -40;
            angle_left = 40;
            raise = true;
            fly = true;
            end = false;
        }
    }
}
