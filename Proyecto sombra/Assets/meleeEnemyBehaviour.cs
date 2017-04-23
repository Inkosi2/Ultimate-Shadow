using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeEnemyBehaviour : MonoBehaviour {
    public double Speed, distX, distY, moduloDist, uniX, uniY;
    //regresar a posición inicial
    public double distX0, distY0, moduloDist0, uniX0, uniY0, Xinicial, Yinicial;
    public bool Attacking;
    public int auxAttack;
    public int hp;
    public bool attackDone, aggro, vulnerability;
    //auxiliares
    public int aux;
    public float time;
   
    public GameObject player;

    public GameObject Enemy;

    // Use this for initialization
    void Start () {
        Speed = 2;
        Attacking = false;
        attackDone = true;
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

        //Contador de tiempo
        if (aggro)
        {
            time += Time.deltaTime;
        }  

        //Movimiento de el enemigo: Obtener vector hacia el jugador.
        distX = transform.position.x - player.transform.position.x;
        distY = transform.position.y - player.transform.position.y;

        moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));

        uniX = distX / moduloDist;
        uniY = distY / moduloDist;

        //El enemigo pilla aggro del jugador a cierta distancia.
        if(Attacking == false && attackDone == true && moduloDist < 5)
        {
            aggro = true;
        }

        //Perseguir el jugador si tiene aggro.
        if (aggro )
        {
            
            Chase();
        }
        //Deja de perseguirlo a cierto punto.
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        //Pierde el aggro si el jugador está lejos suficiente tiempo
        if (moduloDist > 7 && aggro==true && time > 3 && Attacking == false)
        {
            aggro = false;
        }
 

        //Atacar al llegar a cierta distancia al jugador
        if (Attacking == false )
        {
            Attack();
        }

        //Calcular datos para la ruta de vuelta a su posición inical.
        distX0 = transform.position.x - Xinicial;
        distY0 = transform.position.y - Yinicial;

        moduloDist0 = Math.Sqrt(Math.Pow(distX0, 2) + Math.Pow(distY0, 2));

        uniX0 = distX0 / moduloDist0;
        uniY0 = distY0 / moduloDist0;

        //El enemigo regresa a su puesto tras perder de vista al jugador.
        if (!aggro && !Attacking && moduloDist0 > 0.1 )
        {
            time = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(Speed * -uniX0), System.Convert.ToSingle(Speed * -uniY0));
        }

    }

    //Función para que persiga al jugador.
    void Chase()
    {
        //Movimiento de Eaglos: Desplazarse hacia el jugador
        GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(Speed * -uniX), System.Convert.ToSingle(Speed * -uniY));
    }

    //Función para hacer el ataque básico.
    void Attack()
    {

        //Asegurarsse que el jugador está cerca para iniciar el ataque.
        if (moduloDist < 2)
        {
            //Comprovar si ha empzado a atacar o esta en proceso.
            if (attackDone == true)
            {
                //Determinar el timer para la duración de las fases del ataque.
                time = 0;
                //Indicar que el ataque se acaba de iniciar.
                attackDone = false;
                vulnerability = true;
            }
        }
        //Continuar el ataque ya iniciado inclusi si el jugador no está cerca.
        if (attackDone == false)
        {
            //Auxiliar para que no se solapen las fases.
            auxAttack = 0;
            //Indicar que está atacando para evitar llamar otras funciones.
            Attacking = true;

            //Dejar quieto a Eaglos
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //(Provisional) Marcar la fase de carga del ataque. A la espera de sprite.
            GetComponent<SpriteRenderer>().color = Color.yellow;

            //Iniciar la segunda fase del atque en la que lanza el golpe.
            if (time >= 0.5f && auxAttack == 0)
            {
                //(Provisional) Marcar la fase de atacar. A la espera de sprite.
                GetComponent<SpriteRenderer>().color = Color.red;
                auxAttack++;
            }
            //Permitir otro bucle cuando el tiempo entre fases no ha pasado.
            else { Attacking = false; }

            //Iniciar la segunda fase del atque en la que se recompone del golpe.
            if (time >= 0.75d && auxAttack == 1)
            {
                //(Provisional) Marcar la fase recomponerse tras atacar. A la espera de sprite.
                GetComponent<SpriteRenderer>().color = Color.green;

                auxAttack++;
            }
            //Permitir otro bucle cuando el tiempo entre fases no ha pasado.
            else { Attacking = false; }

            //Finalizar el ataque, reiniciando todos los valores.
            if (time >= 1.5d && auxAttack == 2)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                Attacking = false;
                time = 0;
                auxAttack++;
                attackDone = true;
                vulnerability = false;
            }
            //Permitir otro bucle cuando el tiempo entre fases no ha pasado.
            else { Attacking = false; }

        }

    }

    void OnTriggerEnter2D(Collider2D cono)
    {
        if (cono.tag == "Attack" || cono.tag == "Arrow")
        {
            hp--;
        }
    }
}

