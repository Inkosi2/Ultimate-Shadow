using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    float vel, velwalk, velrun;
    public int rotation;


    public GameObject cono;
    public GameObject player;
    public GameObject conoInstanciado;
    public double time;
    public bool attacking, pressedE, previousPressedE;

    // Use this for initialization
    void Start()
    {
        velwalk = 4;
        velrun = 2;
        vel = velwalk;

        time = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //Para correr
        
            
        if (Input.GetKey(KeyCode.LeftShift))
        {
            vel = velrun;
        }
        else
        {
            vel = velwalk;
        }

        //Para desplazarse por los ejes de la X e Y (incluído en diagonal)
        if (!attacking)
        {
            //---------------------------------------------------------------
            // ----------------------    Diagonales    ----------------------
            //---------------------------------------------------------------
            if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.A)))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-vel, vel);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                rotation = 135;
            }

            else if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.D)))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(vel, vel);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                rotation = 360 - 135;
            }
            else if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.A)))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-vel, -vel);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                rotation = 45;
            }
            else if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.D)))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(vel, -vel);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                rotation = 360 - 45;
            }

            //---------------------------------------------------------------
            // ----------------------    Ejes X e Y    ----------------------
            //---------------------------------------------------------------

            else if (Input.GetKey(KeyCode.W))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, vel);
                rotation = 180;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -vel);
                rotation = 0;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-vel, 0);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                rotation = 90;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(vel, 0);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                rotation = 270;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }

        //--------------------------------------------------------------
        //------------------------    Attack    ------------------------
        //--------------------------------------------------------------

        pressedE = Input.GetKey(KeyCode.E);
        time += Time.deltaTime;
        if (Input.GetKey(KeyCode.E) && !attacking)
        {
            time = 0;
            cono = (GameObject)Instantiate(conoInstanciado);
            cono.transform.position = new Vector2(player.transform.position.x, System.Convert.ToSingle(player.transform.position.y - 0.8));
            attacking = true;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        else if (time >= 1 && attacking)
        {
            Destroy(cono);
            attacking = false;
        }
        previousPressedE = pressedE;
    }

}