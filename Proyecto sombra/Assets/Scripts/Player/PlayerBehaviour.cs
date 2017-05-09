using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    using System;

public class PlayerBehaviour : MonoBehaviour {

    bool attacking; // Indica si el jugador se encuentra atacando o no
    int speed = 4; // Velocidad de movimento del jugador
    int ammo; // Cantidad de acciones que puede usar un jugador (flechas, arcos o activar objetos)
    double arrowRotation;
    int playerMode; // Equipo del jugador en ese momento    
        // 1 = arco
        // 2 = bloque
        // 3 = corazón (activador)

    public GameObject arrow; // Referencia para las flechas instanciadas

    GameObject element1; //
    GameObject element2; // Elementos instanciadas
    GameObject element3; //

    public GameObject cam;

    public double distX, distY; // Vector entre el jugador y el cursor del ratón
    double uniX, uniY; // Vector unitario entre el jugador y el cursor del ratón
    double moduloDist;

    public double mouseX;
    public double mouseY;

    // Use this for initialization
    void Start () {
        playerMode = 1;
        ammo = 3;
	}
	
	// Update is called once per frame
	void Update () {
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

            mouseX =  Input.mousePosition.x - Screen.width / 2;
            mouseY = Input.mousePosition.y - Screen.width / 2;

            distX = transform.position.x - mouseX;
            distY = transform.position.y - mouseY;


            moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));

            uniX = distX / moduloDist;
            uniY = distY / moduloDist;

            if (Input.GetMouseButtonDown(0) && playerMode == 1 && ammo == 3)
            {
                if (uniY < 0)
                {
                    arrowRotation = ((2 * Math.PI - Math.Acos(uniX)) * Mathf.Rad2Deg + 180);
                }

                else
                {
                    arrowRotation = ((Math.Acos(uniX)) * Mathf.Rad2Deg + 180);
                }
                
                element1 = (GameObject)Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, System.Convert.ToSingle(arrowRotation)));

                element1.GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(-uniX), System.Convert.ToSingle(-uniY));
            }


        }
    }
}
