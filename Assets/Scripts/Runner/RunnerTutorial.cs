using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunnerTutorial : MonoBehaviour
{
    public float strength;
    public GameObject arm_left;
    public GameObject arm_right;

    public ControllerTutorial player;

    public float angle_left;
    public float angle_right;
    
    public TextMeshProUGUI text;

    public bool raise, jump, down, shoot, run, runtwo, end; 
    // Start is called before the first frame update
    void Start()
    {
        angle_right = 90;
        angle_left = 90;
        raise = true;
        jump = true;
        down = false;
        shoot =false;
        run = false;
        runtwo = false;
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        arm_left.transform.localEulerAngles = new Vector3(0, 0, angle_left);
        arm_right.transform.localEulerAngles = new Vector3(0, 0, angle_right);

        if(raise && angle_right < 170 && angle_left < 170){
            text.text = "Para saltar ambos brazos deben subir";
            angle_right += 30*strength/120f;
            angle_left += 30*strength/120f;
        }

        else if(jump && angle_right>90 && angle_left>90){
            raise = false;
            angle_right -= 30*strength/120f;
            angle_left -= 30*strength/120f;
            player.Jump();
            //Jump player
        }
        else if(transform.localPosition.y > 0){
            text.text = "Estas bajo los efectos de la gravedad, por lo que siempre caes";
        }
        else if(!down && !shoot && !run && !runtwo){
            jump =false;
            down = true;
            shoot = true;
        }

        if(!jump && !raise && down && angle_right >30 && angle_left>30){
            text.text = "Al bajar ambos brazos disparas para eliminar el hielo";
            angle_right -= 30*strength/160f;
            angle_left -= 30*strength/160f;
        }
        else if(!jump && !raise && shoot && angle_right<90 && angle_left<90){
            player.Shoot();
            down = false;
            angle_right += 30*strength/160f;
            angle_left += 30*strength/160f;
        }
        else if(!jump && !raise && !run && !runtwo){
            shoot = false;
            run = true;
            runtwo = true;
        }

        if(!jump && !raise && !down && !shoot && run && angle_left >30 && angle_right<150){
            text.text = "corres moviendo un brazos hacia arriba y el otro hacia abajo, si te quedas sin energia dejas de correr.";
            angle_left -= 30*strength/160f;
            angle_right += 30*strength/160f;
            player.SetEnergy(6f);
        }
        else if(!jump && !raise && !down && !shoot && runtwo && angle_left <150 && angle_right>30){
            run = false;
            angle_left += 30*strength/160f;
            angle_right -= 30*strength/160f;
            player.SetEnergy(7f);
        }
        else if(!jump && !raise && !down && !shoot && !run){
            runtwo = false;
            end = true;
        }
        if(end){
            angle_right = 90;
            angle_left = 90;
            raise = true;
            jump = true;
            end = false;
            player.SetEnergy(5f);
        }
    }
}
