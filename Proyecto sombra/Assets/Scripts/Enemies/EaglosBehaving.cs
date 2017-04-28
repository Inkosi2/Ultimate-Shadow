using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EaglosBehaving : MonoBehaviour

{
    public double EaglosSpeed, distX, distY, moduloDist, uniX, uniY; //Detectar al jugador.
    public bool Attacking, Dashing, Flying, AttackInstanciated; //Funciones de ataque.
    public int fase, HP;
    public double targetX, targetY; 
    public float time;     
    public double angle;
    bool vulnerable;
    public double distX0, distY0, moduloDist0, uniX0, uniY0, Xdestino, Ydestino, Xizquierda, Yizquierda, Xderecha, Yderecha;  //Variables para determinar el movimiento de Eaglos en la 4t fase.

    //Elementos externos:
    public GameObject player, Eaglos;
    public GameObject cono, conoPrefab, flecha, flechaPrefab, areaDash, areaDashPrefab;
    public Camera miCamara;
    public Slider HealthBar;



    //Use this for initialization.

    void Start() {

        //Stats basicos:
        fase = 1;
        HP = 3;
        vulnerable = false;

        //Attacking:
        Attacking = false;

        //Dash:
        Dashing = false;
        targetX = 0;
        targetY = 0; 
        EaglosSpeed = 3;

        //Fase 3:
        Xizquierda = -10;
        Xderecha = 10;
        Yizquierda = 7;
        Yderecha = 7;
        Xdestino = Xizquierda;
        Ydestino = Yizquierda;        
    }

    // Update is called once per frame.
    void Update()
    {
        //Contador de tiempo
        time += Time.deltaTime;

        //Vida de Eaglos:
        if (fase == 4)
        {
            Destroy(Eaglos);
            SceneManager.LoadScene("Victoria");
        }

        if(HP <= 0)
        {
            fase++;
            HP = 3;
        }        

           

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



        if(fase!=3)

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

        

        if (fase == 3)

        {

            Fly();

            Destroy(cono);

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

            targetX = uniX;
            targetY = uniY;    

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

            cono = Instantiate(conoPrefab);

            AttackInstanciated = true;

            cono.transform.position = new Vector2(System.Convert.ToSingle(transform.position.x - targetX/3 ), System.Convert.ToSingle(transform.position.y - targetY/3));            

            if (targetY < 0)
            {
                angle = ((2 * Math.PI - Math.Acos(targetX)) *Mathf.Rad2Deg) + 90;
            }
            
            else
            {
                angle = ((Math.Acos(targetX)) * Mathf.Rad2Deg + 90);
            }

            cono.transform.rotation = Quaternion.Euler(0, 0, System.Convert.ToSingle(angle));
        }







        //Iniciar la segunda fase del atque en la que se recompone del golpe.

        if (time >= 0.75 && time< 1.75)

        {

            //(Provisional) Marcar la fase recomponerse tras atacar. A la espera de sprite.

            GetComponent<SpriteRenderer>().color = Color.green;

            Destroy(cono);

            vulnerable = true;

        }



        //Finalizar el ataque, reiniciando todos los valores.

        if (time >= 1.75 )

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

            EaglosSpeed = 10;

            GetComponent<SpriteRenderer>().color = Color.yellow;



            //Determinar el timer para la duración de las fases del ataque. 

            time = 0;

            

            //Determinar que está dasheando.

            Dashing = true;

        }



        //Segundo condicional, que determina la posición del jugador tras una pausa para cargar.

       

        if (!AttackInstanciated && time >= 0.5 && time < 1.5)
        {
            targetX = uniX;
            targetY = uniY;
            areaDash = Instantiate(areaDashPrefab);
            areaDash.transform.position = transform.position;
            AttackInstanciated = true;

            //Desplaza a Eaglos hacia dodne estaba el jugador cuando empezó el desplazamiento.

            GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(EaglosSpeed * -targetX), System.Convert.ToSingle(EaglosSpeed * -targetY));

        }

        if (time >= 0.5 && time < 1.5)

        {

            GetComponent<SpriteRenderer>().color = Color.red;
            areaDash.transform.position = new Vector2(System.Convert.ToSingle(transform.position.x), System.Convert.ToSingle(transform.position.y));

        }

        //Para a Eaglos tras el dash completo.

        if (time >= 1.5 && time < 2.5 )

        {

            GetComponent<SpriteRenderer>().color = Color.green;

            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            Destroy(areaDash);
        }


        //Restablece los valores y finaliza la función tras una pausa para recuperarse del dash.

        if (time >= 2.5)

        {

            GetComponent<SpriteRenderer>().color = Color.white;

            Dashing = false;

           

            AttackInstanciated = false;

            

            time = 0;

            EaglosSpeed = 3;

            targetX = 0;

            targetY = 0;

        }


      

    }



    //Función para iniciar la cuarta fase en la que vuela.

    void Fly()

    {

        vulnerable = true;

        Flying = true;

        int Speed = 8;

        miCamara.orthographicSize = 9;

        GetComponent<BoxCollider2D>().isTrigger = true;

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

        flecha.transform.position = new Vector2(System.Convert.ToSingle(transform.position.x - uniX / 3), System.Convert.ToSingle(transform.position.y - uniY / 3));

        if (targetY < 0)
        {
            angle = ((2 * Math.PI - Math.Acos(targetX)) * Mathf.Rad2Deg + 180);
        }

        else
        {
            angle = ((Math.Acos(targetX)) * Mathf.Rad2Deg +180);
        }

        flecha.transform.rotation = Quaternion.Euler(0, 0, System.Convert.ToSingle(angle));        
        flecha.GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(-targetX * 10), System.Convert.ToSingle(-targetY * 10));
    }

    void OnTriggerEnter2D(Collider2D cono)
    {        
        if ((cono.tag == "Attack" || cono.tag == "Arrow") & vulnerable)
        {
            HP--;
            HealthBar.value--;
        }
    }
}