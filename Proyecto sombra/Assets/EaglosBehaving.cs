using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaglosBehaving : MonoBehaviour {

    double EaglosSpeed, distX, distY, moduloDist, uniX, uniY;
    public bool Attacking, Dashing;
    int fase;
    
    float targetX;
    float targetY;
   
    public float time;
   
    public GameObject player;

    //Variables para determinar el movimiento de Eaglos en la 4t fase:

    public double distX0, distY0, moduloDist0, uniX0, uniY0, Xdestino, Ydestino, Xizquierda, Yizquierda, Xderecha, Yderecha;

    public int hp;

    //auxiliares
    public double auxDashX, auxDashY, auxDashX2, auxDashY2;
   



    // Use this for initialization
    void Start() {
        fase = 4;
        Attacking = false;
        Dashing = false;
        auxDashX = 0;
        auxDashY = 0;
        auxDashX2 = 0;
        auxDashY2 = 0;

        //Velocidad de Eaglos
        EaglosSpeed = 3;

        Xizquierda = -10;
        Xderecha = 10;
        Yizquierda = 4;
        Yderecha = 4;
        hp = 3;
        

        Xdestino = Xizquierda;
        Ydestino = Yizquierda;
    }


    // Update is called once per frame
    void Update()
    {

        //Contador de tiempo
        time += Time.deltaTime;

        

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

            //Atacar al llegar a cierta distancia al jugador
            if ((moduloDist < 2 || Attacking) && !Dashing) 
            {
                Attack();
            }

            //Poner el CD de el Dash en listo cuando pasen 3 segundos sin atacar.
            else if (fase >1 && time > 2 || Dashing)
            {
                Dash();
            }

            //Perseguir el jugador.
            else
            {
                Chase();
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
        
            //Comprovar si ha empzado a atacar o esta en proceso.
            if (!Attacking)
            {
                //Determinar el timer para la duración de las fases del ataque.
                time = 0;
                //Indicar que el ataque se acaba de iniciar.
                Attacking = true;
            }
       
        //Continuar el ataque ya iniciado inclusi si el jugador no está cerca.
        
            //Auxiliar para que no se solapen las fases.            
            //Indicar que está atacando para evitar llamar otras funciones.           

            //Dejar quieto a Eaglos
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //(Provisional) Marcar la fase de carga del ataque. A la espera de sprite.
            GetComponent<SpriteRenderer>().color = Color.yellow;

            //Iniciar la segunda fase del atque en la que lanza el golpe.
            if (time >= 0.5 && time < 0.75)
            {
                //(Provisional) Marcar la fase de atacar. A la espera de sprite.
                GetComponent<SpriteRenderer>().color = Color.red;               
            }
           
            //Iniciar la segunda fase del atque en la que se recompone del golpe.
            if (time >= 0.75 && time< 1.25)
            {
                //(Provisional) Marcar la fase recomponerse tras atacar. A la espera de sprite.
                GetComponent<SpriteRenderer>().color = Color.green;
               
            }
            
            //Finalizar el ataque, reiniciando todos los valores.
            if (time >= 1.25 )
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                Attacking = false;
                time = 0;                
            }                         

    }

    //Función para dar ataque en dash en la fase 2
    void Dash()
    {
           

        //Primer condicional acticado solo una vez por dash para determinar las variables que va a usar.
        if (!Dashing)
        {
            hp++;
            //Detiene a Eaglos antes del dash.
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);             
            //Da a Eaglos velocidad de dash.
            EaglosSpeed = 6;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            //Determinar el timer para la duración de las fases del ataque.      
            time = 0;
            //Determinar que está dasheando
            Dashing = true;
        }

        //Segundo condicional, que determina la posición del jugador tras una pausa para cargar.
        if (time >= 0.5 && time < 1.5 && auxDashX == 0 && auxDashY == 0 )
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            auxDashX = uniX;
            auxDashY = uniY;
            
        }

        //Desplaza a Eaglos hacia dodne estaba el jugador cuando empezó el desplazamiento.
        GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(EaglosSpeed * -auxDashX), System.Convert.ToSingle(EaglosSpeed * -auxDashY));

        //Segundo condicional, que determina la posición del jugador para el segundo dash.
        if (time >= 1.5 && time < 2.5 && auxDashX2 == 0 && auxDashY2 == 0 && fase == 3) 
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            auxDashX = uniX;
            auxDashY = uniY;
            auxDashX2 = uniX;
            auxDashY2 = uniY;

        }

        //Desplaza a Eaglos hacia dodne estaba el jugador cuando empezó el desplazamiento.
        GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(EaglosSpeed * -auxDashX), System.Convert.ToSingle(EaglosSpeed * -auxDashY));


        //Para a Eaglos tras el dash completo.
        if (time >= 1.5 && time < 2 && fase == 2)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        if (time >= 2.5 && time > 3 && fase == 3)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        



        //Restablece los valores y finaliza la función tras una pausa para recuperarse del dash.
        if (time >= 2 && fase == 2)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            Dashing = false;
            time = 0;
            EaglosSpeed = 3;
            auxDashX = 0;
            auxDashY = 0;
        }
       

        //Restablece los valores y finaliza la función tras una pausa para recuperarse del dash.
        if (time >= 3 && fase == 3)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            Dashing = false;
            time = 0;
            EaglosSpeed = 3;

            auxDashX = 0;
            auxDashY = 0;
            auxDashX2 = 0;
            auxDashY2 = 0;
        }
        
    }

    //Función para iniciar la cuarta fase en la que vuela
    void Fly()
    {
        hp = 0;
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

    void OnTriggerEnter2D(Collider2D cono)
    {        
        if (cono.tag == "Attack")
        {
            hp--;
        }
    }
}