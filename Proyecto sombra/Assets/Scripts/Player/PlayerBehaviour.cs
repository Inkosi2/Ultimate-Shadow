using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviour : MonoBehaviour
{

    bool attacking; // Indica si el jugador se encuentra atacando o no
    int speed = 4; // Velocidad de movimento del jugador
    public int ammo; // Cantidad de acciones que puede usar un jugador (flechas o cajas)
    double arrowRotation;
    public int playerMode; // Equipo del jugador en ese momento    
                    // 1 = arco
                    // 2 = bloque

    public GameObject arrow; // Referencia para las flechas instanciadas
    public GameObject box; // Referencia para las cajas instanciadas

    public GameObject element; //

    public GameObject cam;

    public double distX, distY; // Vector entre el jugador y el cursor del ratón
    double uniX, uniY; // Vector unitario entre el jugador y el cursor del ratón
    double moduloDist;

    public double mouseX;
    public double mouseY;

    public double playerX;
    public double playerY;

    public float boxX, boxY; // Offset entre el jugador y la caja al instanciarla

    bool qPressed = false, pQPressed = false; // Estado de la tecla 'q' pulsada (para eliminar elementos)

    public Queue<GameObject> items = new Queue<GameObject>();

    // Use this for initialization
    void Start()
    {
        playerMode = 1;
        ammo = 3;
    }

    // Update is called once per frame
    void Update()
    {
        qPressed = Input.GetKey(KeyCode.Q);

        if (!attacking)
        {
            //----------------------------------------------------------------------------
            // ----------------------    Movimiento (Diagonales)    ----------------------
            //----------------------------------------------------------------------------

            if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.A)))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, speed);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            else if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.D)))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, speed);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            else if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.A)))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, -speed);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            else if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.D)))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, -speed);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            //-----------------------------------------------------------------------
            // ----------------------    Movimiento (X e Y)    ----------------------
            //-----------------------------------------------------------------------

            else if (Input.GetKey(KeyCode.W))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }

            else if (Input.GetKey(KeyCode.S))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            }

            else if (Input.GetKey(KeyCode.A))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            else if (Input.GetKey(KeyCode.D))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }

            // --------------------------------------------------------
            // ----------------------    Arco    ----------------------
            // --------------------------------------------------------


            // ------ Test zone

            // mouseX = Input.mousePosition.x;
            // mouseY = Input.mousePosition.y;

            mouseX = Input.mousePosition.x - Screen.width / 2;
            mouseY = Input.mousePosition.y - Screen.height / 2;

            playerX = transform.position.x;
            playerY = transform.position.y;

            distX = transform.position.x - mouseX;
            distY = transform.position.y - mouseY;


            moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));

            uniX = distX / moduloDist;
            uniY = distY / moduloDist;



            if (Input.GetMouseButtonDown(0) && playerMode == 1 && ammo > 0)
            {
                if (uniY < 0)
                {
                    arrowRotation = ((2 * Math.PI - Math.Acos(uniX)) * Mathf.Rad2Deg + 180);
                }

                else
                {
                    arrowRotation = ((Math.Acos(uniX)) * Mathf.Rad2Deg + 180);
                }

                element = (GameObject)Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, System.Convert.ToSingle(arrowRotation)));
                element.GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(-uniX) * 5, System.Convert.ToSingle(-uniY) * 5);
                items.Enqueue(element);
                ammo--;
            }
            if (Input.GetMouseButtonDown(0) && playerMode == 2 && ammo > 0)
            {

                if (Mathf.Abs((float)mouseY) > Mathf.Abs((float)mouseX) && mouseY > 0)
                {
                    boxY = (float)1.5;
                    boxX = 0;
                }
                else if (Mathf.Abs((float)mouseY) > Mathf.Abs((float)mouseX) && mouseY < 0)
                {
                    boxY = (float)-1.5;
                    boxX = 0;
                }
                else if (Mathf.Abs((float)mouseY) < Mathf.Abs((float)mouseX) && mouseX > 0)
                {
                    boxY = 0;
                    boxX = 1;
                }
                else if (Mathf.Abs((float)mouseY) < Mathf.Abs((float)mouseX) && mouseX < 0)
                {
                    boxY = 0;
                    boxX = -1;
                }

                element = (GameObject)Instantiate(box, new Vector2(transform.position.x + boxX, transform.position.y + boxY), Quaternion.Euler(0, 0, System.Convert.ToSingle(arrowRotation)));
                items.Enqueue(element);
                ammo--;
            }


                //// ---------------------------- ELIMINAR FLECHAS ----------------------------
                if (qPressed && qPressed != pQPressed)
            {
                if (ammo <= 3)
                {
                    Destroy(items.Dequeue());
                    ammo++;
                }

            }

        }
        pQPressed = qPressed;
    }
}


