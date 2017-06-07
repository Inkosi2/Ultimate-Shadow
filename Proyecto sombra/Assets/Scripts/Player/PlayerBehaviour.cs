using System.Collections;



using System.Collections.Generic;



using UnityEngine;



using System;



using UnityEngine.SceneManagement;







public class PlayerBehaviour : MonoBehaviour



{



    public int HP, maxHP;







    public bool attacking; // Indica si el jugador se encuentra atacando o no



    int speed = 6; // Velocidad de movimento del jugador



    public int ammo; // Cantidad de acciones que puede usar un jugador (flechas o cajas)



    double arrowRotation, swordRotation;



    public int playerMode; // Equipo del jugador en ese momento    



                           // 1 = arco



                           // 2 = bloque







    public double time;







    public GameObject arrow; // Referencia para las flechas instanciadas



    public GameObject box; // Referencia para las cajas instanciadas



    public GameObject cono;







    public GameObject element; // Elemento instanciado







    public GameObject LightBall1, LightBall2, LightBall3, LightBall4; //Vida







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



        HP = 4;



        maxHP = 4;



        playerMode = 1;



        ammo = 3;



    }







    // Update is called once per frame



    void Update()



    {



        time += Time.deltaTime;







        qPressed = Input.GetKey(KeyCode.Q);







        if (HP > maxHP)



        {



            HP = maxHP;



        }







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











            // ----------------------------------------------------------------



            // ----------------------    Cambiar modo    ----------------------



            // ----------------------------------------------------------------



            if (Input.GetKey(KeyCode.Alpha1))



            {



                playerMode = 1;



            }







            else if (Input.GetKey(KeyCode.Alpha2))



            {



                playerMode = 2;



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















            if (Input.GetMouseButtonDown(1) && playerMode == 1 && ammo > 0)



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

                if (HP > 1)

                {

                    HP--;

                }

                items.Enqueue(element);



                ammo--;



            }







            if (Input.GetMouseButtonDown(1) && playerMode == 2 && ammo > 0)



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







                element = (GameObject)Instantiate(box, new Vector2(transform.position.x + boxX, transform.position.y + boxY), Quaternion.Euler(0, 0, 0));

                if (HP > 1)

                {

                    HP--;

                }

                items.Enqueue(element);



                ammo--;



            }







            







            //// ---------------------------- ELIMINAR OBJETOS ----------------------------



            if (qPressed && qPressed != pQPressed)



            {



                if (ammo < 3)



                {



                    Destroy(items.Dequeue());



                    ammo++;



                    HP++;



                }







            }







        }







        if (Input.GetMouseButtonDown(0) && !attacking)



        {



            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);



            time = 0;



            if (uniY < 0)



            {



                swordRotation = ((2 * Math.PI - Math.Acos(uniX)) * Mathf.Rad2Deg + 90);



            }







            else



            {



                swordRotation = ((Math.Acos(uniX)) * Mathf.Rad2Deg + 90);



            }



            



            element = (GameObject)Instantiate(cono, transform.position, Quaternion.Euler(0, 0, System.Convert.ToSingle(swordRotation)));







            element.transform.position = transform.position;



            element.transform.rotation = Quaternion.Euler(0, 0, (float)swordRotation);



            attacking = true;







        }







        if (time >= 0.5 && attacking)



        {



            Destroy(element);



            attacking = false;



        }











        pQPressed = qPressed;







        //-----------------------------------------------



        //-------------------- HUD --------------------



        //-----------------------------------------------







        if (HP <= 3)



        {



            LightBall4.SetActive(false);



        }



        else



        {



            LightBall4.SetActive(true);



        }







        if (HP <= 2)



        {



            LightBall3.SetActive(false);



        }



        else



        {



            LightBall3.SetActive(true);



        }







        if (HP <= 1)



        {



            LightBall2.SetActive(false);



        }



        else



        {



            LightBall2.SetActive(true);



        }







        if (HP <= 0)



        {



            LightBall1.SetActive(false);



            SceneManager.LoadScene("Muerte");



        }



        else



        {



            LightBall1.SetActive(true);



        }



    }







    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "EnemyAttack")
        {
            maxHP--;
            HP--;
        }
    }
}











