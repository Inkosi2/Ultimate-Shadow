using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaglosBehaving : MonoBehaviour {

    double EaglosSpeed, distX, distY, moduloDist, uniX, uniY;
    public bool Attacking, dashCD;
    int fase;
    public int auxAttack;
    bool attackDone, dashDone;

    float targetX;
    float targetY;
   
    public float time;
   
    public GameObject player;

    //Variables para determinar el movimiento de Eaglos en la 4t fase:

    public double distX0, distY0, moduloDist0, uniX0, uniY0, Xdestino, Ydestino, Xizquierda, Yizquierda, Xderecha, Yderecha;

    int hits;

    //auxiliares
    public double auxDashX;
    public double auxDashY;



    // Use this for initialization
    void Start() {
        fase = 4;
        Attacking = false;
        attackDone = true;
        dashDone = true;

        Xizquierda = -10;
        Xderecha = 10;
        Yizquierda = 4;
        Yderecha = 4;

        dashCD = true;

        Xdestino = Xizquierda;
        Ydestino = Yizquierda;
    }


    // Update is called once per frame
    void Update()
    {

        //Contador de tiempo
        time += Time.deltaTime;

        //Velocidad de Eaglos
        EaglosSpeed = 2 + fase + time / 4;

        //Movimiento de Eaglos: Obtener vector hacia el jugador
        distX = transform.position.x - player.transform.position.x;
        distY = transform.position.y - player.transform.position.y;

        moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));

        uniX = distX / moduloDist;
        uniY = distY / moduloDist;

        //Obtener vector hacia las esquinas del mapa.
        distX0 = transform.position.x - Xdestino;
        distY0 = transform.position.y - Ydestino;

        moduloDist0 = Math.Sqrt(Math.Pow(distX0, 2) + Math.Pow(distY0, 2));

        uniX0 = distX0 / moduloDist0;
        uniY0 = distY0 / moduloDist0;
        if(fase!=4)
        { 
            //Perseguir el jugador.
            if (Attacking == false && attackDone == true && dashDone == true)
            {
                Chase();
            }

            //Poner el CD de el Dash en listo cuando pasen 3 segundos sin atacar.
            if (Attacking == false && attackDone == true && time >= 2)
            {
                dashCD = true;
            }

            //Atacar al llegar a cierta distancia al jugador
            if (Attacking == false && dashDone == true)
            {
                Attack();
            }



            //Relizar el ataque Dash cuando se está en fase 2 y no se ha atacado en 3 segundos.
            if (Attacking == false && fase >= 2 && dashCD == true && attackDone == true)
            {
                Dash();
            }
        }

        if (fase == 4)
        {
            Fly();
        }
    }

    

    //Función para que persiga al jugador
    void Chase()
    {
        //Movimiento de Eaglos: Desplazarse hacia el jugador
        GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(EaglosSpeed * -uniX), System.Convert.ToSingle(EaglosSpeed * -uniY));
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
            if (time >= 1.25d && auxAttack == 2)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                Attacking = false;
                time = 0;
                auxAttack++;
                attackDone = true;
            }
            //Permitir otro bucle cuando el tiempo entre fases no ha pasado.
            else { Attacking = false; }

        }

    }

    //Función para dar ataque en dash en la fase 2
    void Dash()
    {
        //Detiene a Eaglos antes del dash.
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //Evilta la ejecución de otras funciones.
        Attacking = true;
        //Da a Eaglos velocidad de dash.
        EaglosSpeed = 10;

        //Primer condicional acticado solo una vez por dash para determinar las variables que va a usar.

        if (dashDone == true)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
            auxDashX = 0;
            auxDashY = 0;
            auxAttack = 0;
            //Determinar el timer para la duración de las fases del ataque.
            time = 0;
            //Indicar que el ataque se acaba de iniciar.
            dashDone = false;
        }
        //Permitir otro bucle cuando el tiempo entre fases no ha pasado.
        else { Attacking = false; }

        //Segundo condicional, que determina la posición del jugador tras una pausa para cargar.
        if (time >= 0.5 && auxAttack == 0)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            auxDashX = uniX;
            auxDashY = uniY;
            auxAttack++;
        }
        //Permitir otro bucle cuando el tiempo entre fases no ha pasado.
        else { Attacking = false; }

        //Desplaza a Eaglos hacia dodne estaba el jugador cuando empezó el desplazamiento.
        GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(EaglosSpeed * -auxDashX), System.Convert.ToSingle(EaglosSpeed * -auxDashY));

        //Segundo condicional, que determina la posición del jugador para el segundo dash.
        if (time >= 1.5 && auxAttack == 1 && fase == 3)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            auxDashX = uniX;
            auxDashY = uniY;
            auxAttack++;
        }
        //Permitir otro bucle cuando el tiempo entre fases no ha pasado.
        else { Attacking = false; }

        //Desplaza a Eaglos hacia dodne estaba el jugador cuando empezó el desplazamiento.
        GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(EaglosSpeed * -auxDashX), System.Convert.ToSingle(EaglosSpeed * -auxDashY));

        //Para a Eaglos tras el dash completo.
        if (time >= 1.5 && auxAttack == 1 && fase == 2)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        //Permitir otro bucle cuando el tiempo entre fases no ha pasado.
        else { Attacking = false; }

        if (time >= 2.5 && auxAttack == 2 && fase == 3)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        //Permitir otro bucle cuando el tiempo entre fases no ha pasado.
        else { Attacking = false; }



        //Restablece los valores y finaliza la función tras una pausa para recuperarse del dash.
        if (time >= 2 && auxAttack == 1 && fase == 2)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            Attacking = false;
            time = 0;
            EaglosSpeed = 1 + time / 4;
            dashDone = true;
            dashCD = false;
        }
        //Permitir otro bucle cuando el tiempo entre fases no ha pasado.
        else { Attacking = false; }

        //Restablece los valores y finaliza la función tras una pausa para recuperarse del dash.
        if (time >= 3 && auxAttack == 2 && fase == 3)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            Attacking = false;
            time = 0;
            EaglosSpeed = 1 + time / 4;
            dashDone = true;
            dashCD = false;
        }
        //Permitir otro bucle cuando el tiempo entre fases no ha pasado.
        else { Attacking = false; }
    }

    //Función para iniciar la cuarta fase en la que vuela
    void Fly()
    {
        hits = 0;
        Attacking = true;
        int Speed = 8;
        

        if (moduloDist0 > 0.1)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(Speed * -uniX0), System.Convert.ToSingle(Speed * -uniY0));
        }
        else if (Xdestino!= Xderecha )
        {
            Xdestino = Xderecha;
            Ydestino = Yderecha;            
        }

        else if (Xdestino != Xizquierda)
        {
            Xdestino = Xizquierda;
            Ydestino = Yizquierda;            
        }

        if (time < 1)
        {
            targetX = player.transform.position.x;
            targetY = player.transform.position.y;
        }

        if (time>=1 && time<=1.5)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (time > 1.5)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            time = 0;
        }

    }
}