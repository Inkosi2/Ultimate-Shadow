using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaglosBehaving : MonoBehaviour
{
    public double EaglosSpeed, distX, distY, moduloDist, uniX, uniY;
    public bool Attacking, Dashing, AttackInstanciated;
    public int fase;
    double targetX, targetY; 
    public float time; 
    public GameObject player;
    public GameObject Eaglos;
    public GameObject flecha, flechaPrefab;
    public GameObject cono, conoInstanciado;
    public double angle;
    bool vulnerable;

    //Variables para determinar el movimiento de Eaglos en la 4t fase:
    public double distX0, distY0, moduloDist0, uniX0, uniY0, Xdestino, Ydestino, Xizquierda, Yizquierda, Xderecha, Yderecha;
    public int hp;

    //auxiliares.
    public double auxDashX, auxDashY, auxDashX2, auxDashY2;

    //Use this for initialization.
    void Start() {

        //Stats basicos:
        fase = 1;
        hp = 3;

        //Attacking:
        Attacking = false;
        //Dash:
        Dashing = false;
        auxDashX = 0;
        auxDashY = 0;
        auxDashX2 = 0;
        auxDashY2 = 0;
                
        EaglosSpeed = 3;

        //Fase 4:
        Xizquierda = -10;
        Xderecha = 10;
        Yizquierda = 4;
        Yderecha = 4;
        Xdestino = Xizquierda;
        Ydestino = Yizquierda;

        vulnerable = false;
    }

    // Update is called once per frame.
    void Update()
    {
        if (fase == 5)
        {
            Destroy(Eaglos);
        }

        if(hp <= 0)
        {
            fase++;
            hp = 3;
        }
        
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

        if (!AttackInstanciated)
        {
            cono = Instantiate(conoInstanciado);
            AttackInstanciated = true;
            cono.transform.position = new Vector2(System.Convert.ToSingle(transform.position.x - uniX), System.Convert.ToSingle(transform.position.y - uniY));
            angle = 405 - System.Convert.ToSingle(Math.Atan(uniY / uniX)) * (180 / System.Convert.ToSingle(Math.PI));
            cono.transform.rotation = Quaternion.Euler(0, 0, System.Convert.ToSingle(angle));

            /*
            //Norte
            if ((uniX < 0.2 && uniY < -0.8) || (uniX > 0.2 && uniY < -0.8))
            {
                cono.transform.rotation = Quaternion.Euler(0, 0, 360);
            }

            //Noroeste
            if ((uniX < -0.2 && uniY < -0.2) || (uniX < -0.2 && uniY < -0.2))
            {
                cono.transform.rotation = Quaternion.Euler(0, 0, 315);
            }

            //Oeste
            if ((uniX < -0.8 && uniY < 0.2) || (uniX < -0.8 && uniY > -0.2))
            {
                cono.transform.rotation = Quaternion.Euler(0, 0, 270);
            }

            //Sur
            if ((uniX < 0.2 && uniY > 0.8) || (uniX > 0.2 && uniY > 0.8))
            {
                cono.transform.rotation = Quaternion.Euler(0, 0, 180);
            }

            //Este
            if ((uniX > 0.8 && uniY < 0.2) || (uniX > 0.8 && uniY > -0.2))
            {
                cono.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            */
        }



        //Iniciar la segunda fase del atque en la que se recompone del golpe.
        if (time >= 0.75 && time< 1.25)
        {
            //(Provisional) Marcar la fase recomponerse tras atacar. A la espera de sprite.
            GetComponent<SpriteRenderer>().color = Color.green;
            Destroy(cono);
            vulnerable = true;
        }

        //Finalizar el ataque, reiniciando todos los valores.
        if (time >= 1.25 )
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            Attacking = false;
            AttackInstanciated = false;
            time = 0;
            vulnerable = false;
        }                         
    }

    //Función para dar ataque en dash en la fase 2
    void Dash()
    {
        //Primer condicional acticado solo una vez por dash para determinar las variables que va a usar.
        if (!Dashing)
        {
            //Detiene a Eaglos antes del dash.
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            
            //Da a Eaglos velocidad de dash.
            EaglosSpeed = 6;
            GetComponent<SpriteRenderer>().color = Color.yellow;

            //Determinar el timer para la duración de las fases del ataque. 
            time = 0;
            
            //Determinar que está dasheando.
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

    //Función para iniciar la cuarta fase en la que vuela.
    void Fly()
    {

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

        if (time>=1 && time<=1.5 && !AttackInstanciated)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            Shoot();
            AttackInstanciated = true;
        }

        if (time > 1.5)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            time = 0;
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
        flecha.GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(-targetX * 10), System.Convert.ToSingle(-targetY * 10));
    }

    void OnTriggerEnter2D(Collider2D cono)
    {        
        if ((cono.tag == "Attack" || cono.tag == "Arrow") & vulnerable)
        {
            hp--;
        }
    }
}