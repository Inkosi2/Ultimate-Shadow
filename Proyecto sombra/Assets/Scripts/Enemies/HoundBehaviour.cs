using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoundBehaviour : MonoBehaviour {

    public double EaglosSpeed, distX, distY, moduloDist, uniX, uniY, targetX, targetY; //Detectar al jugador.
    public GameObject player;
    int speed, chargingSpeed;
    double range;
    bool charging;

    // Use this for initialization
    void Start () {
        speed = 5;
        chargingSpeed = 12;
        range = 7;
        charging = false;
	}
	
	// Update is called once per frame
	void Update () {
        // Vector hacia el jugador.
        distX = player.transform.position.x - transform.position.x;
        distY = player.transform.position.y - transform.position.y;

        moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));

        uniX = distX / moduloDist;
        uniY = distY / moduloDist;

       
        if (moduloDist < range)
        {
            if (!charging)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                charging = true;
                targetX = uniX;
                targetY = uniY;
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2((float)targetX * chargingSpeed, (float)targetY * chargingSpeed);
        }
        else if(charging)
        {
            charging = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2((float)uniX * speed, (float)uniY * speed);
        }


    }
}
