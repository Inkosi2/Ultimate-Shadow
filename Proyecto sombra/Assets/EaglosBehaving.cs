using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaglosBehaving : MonoBehaviour {

    double EaglosSpeed, distX, distY, moduloDist, uniX, uniY;
    bool Attacking;
 
    //double BossPlayerAngle;
    public GameObject player;

	// Use this for initialization
	void Start () {
        EaglosSpeed = 4.5d;
        Attacking = false;
	}
	
	// Update is called once per frame
	void Update ()
    {

       
            //Velocidad de Eaglos
            EaglosSpeed = 4.5d;

            //Movimiento de Eaglos: Obtener vector hacia el jugador
            distX = transform.position.x - player.transform.position.x;
            distY = transform.position.y - player.transform.position.y;

            moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));

            uniX = distX / moduloDist;
            uniY = distY / moduloDist;

        if (Attacking == false)
        {
            //Movimiento de Eaglos: Desplazarse hacia el jugador
            GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(EaglosSpeed * -uniX), System.Convert.ToSingle(EaglosSpeed * -uniY));
        }
        //Atacar al llegar a cierta distancia al jugador
        if (moduloDist<2)
        {
            Attacking = true;
            float auxTime = Time.time;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //EaglosSpeed = 0.0d;
            if (Time.time - auxTime == 1)
            {
                Debug.Log("SHWING!");
            }

            auxTime = Time.time;

            if (Time.time - auxTime == 3)
            {
                Attacking = false;
            }
        }
    }
}
