using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRoute : MonoBehaviour {

    public double distX0, distY0, moduloDist0, uniX0, uniY0, Xdestino, Ydestino, X1, Y1, X2, Y2, X3, Y3, counter;
    int Speed;
    public GameObject Diana;

    // Use this for initialization
    void Start () {
        //transform.position = new Vector2 (System.Convert.ToSingle(X1), System.Convert.ToSingle(Y1)); 
        X1 = 0.8;
        Y1 = 3.4;
        X2 = 5.5;
        Y2 = 3.4;
        X3 = 5.5;
        Y3 = 24;
        Xdestino = X2;
        Ydestino = Y2;
        Speed = 10;
        counter = 1;

    }
	
	// Update is called once per frame
	void Update () {
       
        //Obtener vector hacia las esquinas del mapa.
        distX0 = transform.position.x - Xdestino;
        distY0 = transform.position.y - Ydestino;

        moduloDist0 = Math.Sqrt(Math.Pow(distX0, 2) + Math.Pow(distY0, 2));

        uniX0 = distX0 / moduloDist0;
        uniY0 = distY0 / moduloDist0;


       /* if (Diana.GetComponent<Diana>().activated)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }*/
        
        //movimiento
        if (moduloDist0 > 0.1 && counter == 1)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(Speed * -uniX0), System.Convert.ToSingle(Speed * -uniY0));
            counter++;
            Xdestino = X2;
            Ydestino = Y2;       
        }
        
        else if (moduloDist0 > 0.1 && counter == 2)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(Speed * -uniX0), System.Convert.ToSingle(Speed * -uniY0));
            counter++;
            Xdestino = X3;
            Ydestino = Y3;
        }

        else if (moduloDist0 > 0.1 && counter == 3)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(Speed * -uniX0), System.Convert.ToSingle(Speed * -uniY0));
            counter++;
            Xdestino = X2;
            Ydestino = Y2;
        }

        else if (moduloDist0 > 0.1 && counter == 4)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(Speed * -uniX0), System.Convert.ToSingle(Speed * -uniY0));
            counter = 1;
            Xdestino = X1;
            Ydestino = Y1;
        }

    }
}

