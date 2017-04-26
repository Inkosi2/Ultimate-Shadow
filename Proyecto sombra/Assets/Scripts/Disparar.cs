using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{

    public int municion, lastShot;
    float nextFire, fireRateBullet, timer;
    public GameObject flecha;
    public GameObject player;
    public GameObject f1, f2, f3;
    public bool Pressed, previousPressed;

    // Use this for initialization
    void Start()
    {
        municion = 3;
        nextFire = 0;
        fireRateBullet = 1;
    }


    void Fire()
    {
        if (Time.time > nextFire && municion > 0 && player.GetComponent<PlayerController>().HP > 1)
        {
            nextFire = Time.time + fireRateBullet;
            //var bullet = (GameObject)Instantiate(flecha, player.transform.position, flecha.transform.rotation);

            if (municion == 3)
            {
                f1 = (GameObject)Instantiate(flecha, player.transform.position, flecha.transform.rotation);
                lastShot = 1;
            }
            else if (municion == 2)
            {
                f2 = (GameObject)Instantiate(flecha, player.transform.position, flecha.transform.rotation);
                lastShot = 2;
            }
            else if (municion == 1)
            {
                f3 = (GameObject)Instantiate(flecha, player.transform.position, flecha.transform.rotation);
                lastShot = 3;
            }
            municion--;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow))
        {
                Fire();
        }
        Pressed = Input.GetKey(KeyCode.Q);
        if (Input.GetKey(KeyCode.Q))
        {
            if (lastShot == 1 && municion == 2 && Pressed != previousPressed)
            {
                Destroy(f1);
                municion++;
                lastShot--;
            }
            else if (lastShot == 2 && municion == 1 && Pressed != previousPressed)
            {
                Destroy(f2);
                municion++;
                lastShot--;
            }
            else if (lastShot == 3 && municion == 0 && Pressed != previousPressed)
            {
                Destroy(f3);
                municion++;
                lastShot--;
            }
        }
        previousPressed = Pressed;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //if (coll.gameObject.tag == "Bloque movil")
        //{
            //GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y) ;
        //}
        if (coll.gameObject.tag == "Wall")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        
    }
}
