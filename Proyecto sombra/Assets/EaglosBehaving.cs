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
   
    public float time;
    //double BossPlayerAngle;
    public GameObject player;
    //auxiliares
    public double auxDashX;
    public double auxDashY;



    // Use this for initialization
    void Start() {
        fase = 2;
        EaglosSpeed = 4.5d;
        Attacking = false;
        attackDone = true;
        dashDone = true;
        
       dashCD = true;
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

    //Función para hacer el ataque básico.
    void Attack ()
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

        //Para a Eaglos tras el dash completo.
        if (time >= 1.5 && auxAttack == 1)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);           
        }
        //Permitir otro bucle cuando el tiempo entre fases no ha pasado.
        else { Attacking = false; }

        //Restablece los valores y finaliza la función tras una pausa para recuperarse del dash.
        if (time >= 2 && auxAttack == 1)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            Attacking = false;
            time = 0;
            EaglosSpeed = 4.5;
            dashDone = true;
            dashCD = false;
        }
        //Permitir otro bucle cuando el tiempo entre fases no ha pasado.
        else { Attacking = false; }
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
        if ( Attacking == false && fase>=2 && dashCD == true && attackDone == true)
        {
           Dash();
        }
    }
}
