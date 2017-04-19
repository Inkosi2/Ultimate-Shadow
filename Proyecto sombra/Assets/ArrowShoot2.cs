/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot2 : MonoBehaviour
{

    bool shot2;
    ArrowShoot disparo1;

    // Use this for initialization
    void Start()
    {
        shot2 = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Seguimiento al jugador
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
        if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.A)))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-vel, vel);
        }
        else if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.D)))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(vel, vel);
        }
        else if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.A)))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-vel, -vel);
        }
        else if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.D)))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(vel, -vel);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, vel);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -vel);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-vel, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(vel, 0);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }



        //Lanzamiento de flecha
        if (!shot2 && shot)
        {

            if (Input.GetKey(KeyCode.UpArrow))
            {
                up = true;
                down = false;
                left = false;
                right = false;
                transform.localRotation = Quaternion.Euler(0, 0, 90);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.localRotation = Quaternion.Euler(0, 0, 270);
                up = false;
                down = true;
                left = false;
                right = false;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                up = false;
                down = false;
                left = true;
                right = false;
                transform.localRotation = Quaternion.Euler(0, 0, 180);
            }

            else if (Input.GetKey(KeyCode.RightArrow))
            {
                up = false;
                down = false;
                left = false;
                right = true;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (up)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, velVuelo);

            shot = true;
        }
        else if (left)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-velVuelo, 0);

            shot = true;
        }
        else if (down)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velVuelo);

            shot = true;
        }
        else if (right)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(velVuelo, 0);

            shot = true;
        }

    }
}
*/