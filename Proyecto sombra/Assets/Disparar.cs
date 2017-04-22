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

    // Use this for initialization
    void Start()
    {
        municion = 3;
        nextFire = 0;
        fireRateBullet = 1;
    }


    void Fire()
    {
        if (Time.time > nextFire && municion > 0)
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
        if (Input.GetKey(KeyCode.Q))
        {
            if (lastShot == 1)
            {
                Destroy(f1);
                municion++;
            }
            else if (lastShot == 2)
            {
                Destroy(f2);
                municion++;
            }
            else if (lastShot == 3)
            {
                Destroy(f3);
                municion++;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
