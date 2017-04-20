using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaglosBehaving : MonoBehaviour {

    double EaglosSpeed, distX, distY, moduloDist, uniX, uniY;
    bool Attacking, DashCD;
    int fase;
    //double BossPlayerAngle;
    public GameObject player;

	// Use this for initialization
	void Start() {
        EaglosSpeed = 4.5d;
        Attacking = false;
	}
	
    void Stun ()
    {
        //Espera 4 segundos.
        if (fase == 4)
        {
            //muere
        }
        /*if ( Recibe daño ) 
        { 
        fase++;
        }*/
    }

    void Chase ()
    {
        //Movimiento de Eaglos: Desplazarse hacia el jugador
        GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(EaglosSpeed * -uniX), System.Convert.ToSingle(EaglosSpeed * -uniY));
    }

    void Attack ()
    {
        Attacking = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //Esperar medio segundo
        //Atacar (Crear Triggered 2D object en forma de cono que hace daño al jugador)
        //Esperar 2 segunos
        Attacking = false;
    }

    void Dash()
    {
        Attacking = true;
        EaglosSpeed = 9;
        //vector fijo. Tal vez haga falta igualar los unitarios a dos variables auxiliares y usarlas pera que el vector no se actualize. 
        GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(EaglosSpeed * -uniX), System.Convert.ToSingle(EaglosSpeed * -uniY));
        EaglosSpeed = 4.5;
    }

    void Fly()
    {
        Attacking = true;
        //Desplazarse de lado a lado
        //Disparar cada pocos segundos
    }

	// Update is called once per frame
	void Update()
    {

       
            //Velocidad de Eaglos
            EaglosSpeed = 4.5d;

            //Movimiento de Eaglos: Obtener vector hacia el jugador
            distX = transform.position.x - player.transform.position.x;
            distY = transform.position.y - player.transform.position.y;

            moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));

            uniX = distX / moduloDist;
            uniY = distY / moduloDist;

        //Perseguir el jugador.
        if (Attacking == false)
        {
            Chase();
        }

        //Atacar al llegar a cierta distancia al jugador
        if (moduloDist<2 && Attacking == false)
        {
            Attack();
        }

        if (moduloDist > 4 && moduloDist < 10 && Attacking == false && DashCD = true)
        {
            Dash();
        }
    }
}
