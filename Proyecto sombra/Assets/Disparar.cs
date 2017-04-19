using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{

    int municion;
    float nextFire, fireRateBullet, timer;
    public GameObject flecha;
    public GameObject player;

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
            var bullet = (GameObject)Instantiate(flecha, player.transform.position, flecha.transform.rotation);
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
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
