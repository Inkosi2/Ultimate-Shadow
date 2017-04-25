using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArcherBehaviour : MonoBehaviour {

    int Speed;    
    public double distX, distY, moduloDist, uniX, uniY; //Variables para el vector hacia el jugador.   
    public double distX0, distY0, moduloDist0, uniX0, uniY0, Xinicial, Yinicial; //Variables para el vector hacia la posición inicial.
    public double angle;   
    public double targetX, targetY; //Variables patra apuntar a su objetivo.
    
    public float time;
    public bool Attacking, AttackInstanciated;
    public int hp;

    public GameObject player;
    public GameObject Enemy;

    public GameObject flecha;
    public GameObject flechaPrefab;

    // Use this for initialization
    void Start () {
        Speed = 4;
        Attacking = false;
        time = 0;
        hp = 3;
        Xinicial = transform.position.x;
        Yinicial = transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (hp <= 0)
        {
            Destroy(Enemy);
        }
        //Contador de tiempo.
        time += Time.deltaTime;

        //Obtener vector hacia el jugador.
        distX = transform.position.x - player.transform.position.x;
        distY = transform.position.y - player.transform.position.y;

        moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));

        uniX = distX / moduloDist;
        uniY = distY / moduloDist;

        //Obtener vector hacia la posición inicial.
        distX0 = transform.position.x - Xinicial;
        distY0 = transform.position.y - Yinicial;

        moduloDist0 = Math.Sqrt(Math.Pow(distX0, 2) + Math.Pow(distY0, 2));

        uniX0 = distX0 / moduloDist0;
        uniY0 = distY0 / moduloDist0;

        angle = Mathf.Atan((transform.position.y - player.transform.position.y) / (transform.position.x - player.transform.position.x));

        //DETERMINAR LAS ACCIONES DEL ENEMIGO:

        //Huir si el jugador está demasiado cerca y el enemigo no está en medio de un ataque.
        if (moduloDist < 5 && !Attacking)
        {
            Flee();
        }
        //Atacar si el jugador esta dentro de su rango pero fuera de el area de amenaza.
        else if (moduloDist < 10 || Attacking)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Attack();
        }
        //Regresar a la posición inicial si se ha perdido de vista al jugador
        else if (moduloDist0 > 0.1)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(Speed * -uniX0), System.Convert.ToSingle(Speed * -uniY0));
        }
        //Detenerse al llegar lo bastante cerca de la posición inicial.
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    //Función para huir del jugador.
    void Flee()
    {        
        //Se desplaze en dirección opuesta al vector entre el y el jugador.
        GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(Speed * uniX), System.Convert.ToSingle(Speed * uniY));        
    }

    //Función para ayacar al jugador.
    void Attack()
    {
        


        //Iniciar el ataque, dando valores a las variables que no se verán modificados hasta empezar un nuevo ataque.
        if (moduloDist < 10 && !Attacking)
        {
            time = 0;
            Attacking = true;
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }       
      
        //Determinar la dirección de la flecha en el momento de dispararla.
        if (time<1)
        {
            targetX = player.transform.position.x;
            targetY = player.transform.position.y;
        }
           
        //Para de cargar y dispara la flecha.
        if (time >= 1 && time < 1.25)
        {
            if (!AttackInstanciated)
            {
                Shoot();
            }
        }
        
        //Pausa tras el atque.
        if (time >= 1.25 && time < 2)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        
        //Acabar de atacar.
        if (time > 2)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            Attacking = false;
            AttackInstanciated = false;
        }
       
    }

    void Shoot()
    {
        targetX = distX / moduloDist;
        targetY = distY / moduloDist;

        GetComponent<SpriteRenderer>().color = Color.red;
        flecha = (GameObject)Instantiate(flechaPrefab);
        flecha.transform.position = transform.position;
        AttackInstanciated = true;

        flecha.transform.rotation = Quaternion.Euler(180, 0, 0);
        flecha.transform.position = new Vector2(System.Convert.ToSingle(transform.position.x - uniX), System.Convert.ToSingle(transform.position.y - uniY));
        flecha.GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(-targetX*10), System.Convert.ToSingle(-targetY*10));
    }

    void OnTriggerEnter2D(Collider2D cono)
    {
        
        if (cono.tag == "Attack" || cono.tag == "Arrow")
        {
            hp--;
        }
    }
}
