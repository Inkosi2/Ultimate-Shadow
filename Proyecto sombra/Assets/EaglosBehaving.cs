using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaglosBehaving : MonoBehaviour {

    double EaglosSpeed, distX, distY, moduloDist, uniX, uniY;
    public bool Attacking, DashCD;
    int fase;
    int auxAttack;
    bool attackDone;
    public float time;
    //double BossPlayerAngle;
    public GameObject player;
    //auxiliares
    


    // Use this for initialization
    void Start() {
        fase = 1;
        EaglosSpeed = 4.5d;
        Attacking = false;
        attackDone = true;
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

    //Función para que persiga al jugador
    void Chase ()
    {
        //Movimiento de Eaglos: Desplazarse hacia el jugador
        GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(EaglosSpeed * -uniX), System.Convert.ToSingle(EaglosSpeed * -uniY));
    }

    //Función para hacer el ataque básico
    void Attack ()
    {
       
        auxAttack = 0;
        Attacking = true;
            
        if (attackDone == true)
        {
            time = 0;
            attackDone = false;
        } 
       
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<SpriteRenderer>().color = Color.yellow;

        if (time >= 0.5f && auxAttack == 0)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            auxAttack++;
        }
        else { Attacking = false; }

        if (time >= 1.0d && auxAttack == 1)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            auxAttack++;
        }
        else { Attacking = false; }

        if (time >= 3.0d && auxAttack == 2)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            Attacking = false;
            time = 0;
            auxAttack++;
        }
        else { Attacking = false; }

       
    }

    //Función para dar ataque en dash en la fase 2
    void Dash()
    {
        double auxDashX = uniX;
        double auxDashY = uniY;

        Attacking = true;
        EaglosSpeed = 15;

        GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(EaglosSpeed * -auxDashX), System.Convert.ToSingle(EaglosSpeed * -auxDashY));
        //GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        EaglosSpeed = 4.5;
        Attacking = false;
    }

    //Función para iniciar la cuarta fase en la que vuela
    void Fly()
    {
        int hits = 0;
        Attacking = true;
        //Desplazarse de lado a lado
        //Disparar cada pocos segundos
        /*if (flecha clavada) 
         {
         hits--;
         } 
         
         */
    }

	// Update is called once per frame
	void Update()
    {
       
        //Contador de tiempo
        time+=Time.deltaTime;

        //Velocidad de Eaglos
        EaglosSpeed = 4.5d;

        //Movimiento de Eaglos: Obtener vector hacia el jugador
        distX = transform.position.x - player.transform.position.x;
        distY = transform.position.y - player.transform.position.y;

        moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));

        uniX = distX / moduloDist;
        uniY = distY / moduloDist;

        //Perseguir el jugador.
        if (Attacking == false && attackDone==true)
        {
            Chase();
        }

    //Atacar al llegar a cierta distancia al jugador
    if (Attacking == false && moduloDist < 2)
    {
        Attack();
    }
        if (moduloDist > 4 && moduloDist < 15 && Attacking == false && fase>=2 && DashCD == true)
        {
            Dash();
        }
    }
}
