using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    float vel, velwalk, velrun;

    // Use this for initialization
    void Start()
    {
        velwalk = 1;
        velrun = 2;
        vel = velwalk;
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
        if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.A)))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-vel, vel);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        else if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.D)))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(vel, vel);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.A)))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-vel, -vel);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.D)))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(vel, -vel);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
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
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(vel, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}


