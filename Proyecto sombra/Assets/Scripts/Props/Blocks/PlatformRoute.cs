using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class PlatformRoute : MonoBehaviour {



    public double distX0, distY0, moduloDist0, uniX0, uniY0, Xdestino, Ydestino, X1, Y1, X2, Y2, X3, Y3, previousX, previousY;
    int Speed;
    public GameObject Diana;
    bool movimiento;
    public Collider2D box;

    // Use this for initialization
    void Start () {
        X1 = 0.8;
        Y1 = 3.4;
        X2 = 5.5;
        Y2 = 3.4;
        X3 = 5.5;
        Y3 = 24;
        Xdestino = X1;
        Ydestino = Y1;
        previousX = X2;
        previousY = Y2;
        Speed = 4;
        movimiento = false;
    }
	// Update is called once per frame

	void Update () {
        //Obtener vector hacia las esquinas del mapa.

        distX0 = transform.position.x - Xdestino;
        distY0 = transform.position.y - Ydestino;

        moduloDist0 = Math.Sqrt(Math.Pow(distX0, 2) + Math.Pow(distY0, 2));

        uniX0 = distX0 / moduloDist0;
        uniY0 = distY0 / moduloDist0;
        
        //movimiento     
        
        if (moduloDist0 > 0.1 )
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(Speed * -uniX0), System.Convert.ToSingle(Speed * -uniY0));
        }
        
        else if (Xdestino == X1 && Ydestino == Y1 && previousX == X2 && previousY == Y2 && movimiento == true )
        {   
            Xdestino = X2;
            Ydestino = Y2;
            previousX = X1;
            previousY = Y1;
        }
        
        else if (Xdestino == X2 && Ydestino == Y2 && previousX == X1 && previousY == Y1)
        {
            Xdestino = X3;
            Ydestino = Y3;
            previousX = X2;
            previousY = Y2;
        }
     
        else if (Xdestino == X3 && Ydestino == Y3 && previousX == X2 && previousY == Y2)
        {
            Xdestino = X2;
            Ydestino = Y2;
            previousX = X3;
            previousY = Y3;
        }
        
        else if (Xdestino == X2 && Ydestino == Y2 && previousX == X3 && previousY == Y3)
        {
            Xdestino = X1;
            Ydestino = Y1;
            previousX = X2;
            previousY = Y2;
        }
        
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
        
        if (Diana.GetComponent<Diana>().activated > 0)
        {
            movimiento = true;
        }
        else
        {
            movimiento = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (movimiento)
        {
            box.isTrigger = false;
        }
        else
        {
            box.isTrigger = true;
        }
    }

}



