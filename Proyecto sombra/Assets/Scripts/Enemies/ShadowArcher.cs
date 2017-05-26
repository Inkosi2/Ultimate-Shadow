using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowArcher : MonoBehaviour {
    public float time;
    public bool Attacking, AttackInstanciated;
    public int hp;
    public double angle;
    public double distX, distY, moduloDist, uniX, uniY; //Variables para el vector hacia el jugador. 

    public double targetX, targetY; //Variables patra apuntar a su objetivo.

    public GameObject player;
    public GameObject Enemy;

    public GameObject flecha;
    public GameObject flechaPrefab;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Contador de tiempo.
        time += Time.deltaTime;

        //Obtener vector hacia el jugador.
        distX = transform.position.x - player.transform.position.x;
        distY = transform.position.y - player.transform.position.y;

        moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));

        uniX = distX / moduloDist;
        uniY = distY / moduloDist;
       

        angle = Mathf.Atan((transform.position.y - player.transform.position.y) / (transform.position.x - player.transform.position.x));
        //Función para ayacar al jugador.

        if (time > 1.5)
        {
            Attack();           
        }
        
    }

    void Attack()
    {



        //Iniciar el ataque, dando valores a las variables que no se verán modificados hasta empezar un nuevo ataque.
        if ( !Attacking)
        {           
            Attacking = true;           
            targetX = player.transform.position.x;
            targetY = player.transform.position.y;
        }
        
        if (!AttackInstanciated)
        {
            Shoot();
            Attacking = false;
            AttackInstanciated = false;
            time = 0;
        }
    }

    void Shoot()
    {
        targetX = distX / moduloDist;

        targetY = distY / moduloDist;       

        flecha = (GameObject)Instantiate(flechaPrefab);

        flecha.transform.position = transform.position;

        AttackInstanciated = true;

        flecha.transform.position = new Vector2(System.Convert.ToSingle(transform.position.x - uniX / 3), System.Convert.ToSingle(transform.position.y - uniY / 3));

        if (targetY < 0)
        {
            angle = ((2 * Math.PI - Math.Acos(targetX)) * Mathf.Rad2Deg + 180);
        }

        else
        {
            angle = ((Math.Acos(targetX)) * Mathf.Rad2Deg + 180);
        }

        flecha.transform.rotation = Quaternion.Euler(0, 0, System.Convert.ToSingle(angle));

        flecha.GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(-targetX * 20), System.Convert.ToSingle(-targetY * 20));
    }
}
