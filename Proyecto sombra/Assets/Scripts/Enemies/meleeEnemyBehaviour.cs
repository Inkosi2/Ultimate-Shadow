using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeEnemyBehaviour : MonoBehaviour {


    public double Speed, distX, distY, moduloDist, uniX, uniY;
    //regresar a posición inicial
    public double distX0, distY0, moduloDist0, uniX0, uniY0, Xinicial, Yinicial, time;
    public bool Attacking;
    public double auxTime;
    public int hp;
    public bool vulnerable, AttackInstanciated;
    bool Chasing;
    public double angle;
    //auxiliares
    public int aux;
   
    public GameObject player;

    public GameObject Enemy;

    public GameObject cono, conoInstanciado;

    // Use this for initialization
    void Start () {
        Speed = 2;
        Attacking = false;        
        Chasing = false;
        vulnerable = false;
        Xinicial=transform.position.x;
        Yinicial=transform.position.y;
        //Inicializar valores de regreso a su posición inicial.
        moduloDist0 = 1;
        hp = 3;
    }
    

    // Update is called once per frame
    void Update ()
    {

        if (hp <= 0)
        {
            Destroy(Enemy);
        }

        time += Time.deltaTime;
       
        //Movimiento de el enemigo: Obtener vector hacia el jugador.
        distX = transform.position.x - player.transform.position.x;
        distY = transform.position.y - player.transform.position.y;

        moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));

        uniX = distX / moduloDist;
        uniY = distY / moduloDist;

        //Calcular datos para la ruta de vuelta a su posición inical.
        distX0 = transform.position.x - Xinicial;
        distY0 = transform.position.y - Yinicial;

        moduloDist0 = Math.Sqrt(Math.Pow(distX0, 2) + Math.Pow(distY0, 2));

        uniX0 = distX0 / moduloDist0;
        uniY0 = distY0 / moduloDist0;

        angle = System.Convert.ToSingle(Mathf.Atan(System.Convert.ToSingle(uniY / uniX)));

        if (moduloDist < 2 || Attacking)
        {
            Attack();
        }

        else if (moduloDist < 5 || Chasing)
        {
            Chase();
        }
        
        if (moduloDist > 10  && moduloDist0 > 0.1)
        {
            
            GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(Speed * -uniX0), System.Convert.ToSingle(Speed * -uniY0));
            Chasing = false;

        }       

        else if (!Chasing)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    //Función para que persiga al jugador.
    void Chase()
    {
        //Movimiento de Eaglos: Desplazarse hacia el jugador
        GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(Speed * -uniX), System.Convert.ToSingle(Speed * -uniY));
        Chasing = true;
    }

   
    

        //Función para hacer el ataque básico.
        void Attack()
     {

            //Comprovar si ha empzado a atacar o esta en proceso.
            if (!Attacking)
            {

                ///Determinar el timer para la duración de las fases del ataque.
                time = 0;
                //Indicar que el ataque se acaba de iniciar.
                Attacking = true;
            }

            //Dejar quieto a Eaglos
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            //(Provisional) Marcar la fase de carga del ataque. A la espera de sprite.
            GetComponent<SpriteRenderer>().color = Color.yellow;

            //Iniciar la segunda fase del atque en la que lanza el golpe.
            if (time >= 0.5 && time < 0.75 )
            {
                    //(Provisional) Marcar la fase de atacar. A la espera de sprite.
                    GetComponent<SpriteRenderer>().color = Color.red;
                    
             }

            if (time >= 0.5 && time < 0.75 && !AttackInstanciated)
            {
                cono = Instantiate(conoInstanciado);
                AttackInstanciated = true;

                cono.transform.position = new Vector2(System.Convert.ToSingle(transform.position.x - uniX / 3), System.Convert.ToSingle(transform.position.y - uniY / 3));

                if (uniY < 0)
                {
                    angle = ((2 * Math.PI - Math.Acos(uniX)) * Mathf.Rad2Deg) + 90;
                }

                else
                {
                    angle = ((Math.Acos(uniX)) * Mathf.Rad2Deg + 90);
                }

                cono.transform.rotation = Quaternion.Euler(0, 0, System.Convert.ToSingle(angle));

            }

            //Iniciar la segunda fase del atque en la que se recompone del golpe.
            if (time >= 0.75 && time < 1.25)
                {
                    //(Provisional) Marcar la fase recomponerse tras atacar. A la espera de sprite.
                    GetComponent<SpriteRenderer>().color = Color.green;
                    Destroy(cono);
                    vulnerable = true;
                }

            //Finalizar el ataque, reiniciando todos los valores.
            if (time >= 1.25)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                Attacking = false;
                time = 0;
                AttackInstanciated = false;
                vulnerable = false;
            }
        }

        void OnTriggerEnter2D(Collider2D cono)
    {
        if ((cono.tag == "Attack" || cono.tag == "Arrow") && vulnerable)
        {
            hp--;
        }
    }
}

