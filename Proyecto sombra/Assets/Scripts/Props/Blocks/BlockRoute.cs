using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRoute : MonoBehaviour {

    public double distX0, distY0, moduloDist0, uniX0, uniY0, Xdestino, Ydestino, Xizquierda, Yizquierda, Xderecha, Yderecha;
    int Speed;
    public GameObject Diana;

    // Use this for initialization
    void Start () {
        transform.position = new Vector2(-13, 20); 
        Xderecha = -8;
        Xizquierda = -13;
        Xdestino = Xderecha;
        Ydestino = 20;   
        Speed = 4;

    }
	
	// Update is called once per frame
	void Update () {
       
        //Obtener vector hacia las esquinas del mapa.
        distX0 = transform.position.x - Xdestino;
        distY0 = transform.position.y - Ydestino;

        moduloDist0 = Math.Sqrt(Math.Pow(distX0, 2) + Math.Pow(distY0, 2));

        uniX0 = distX0 / moduloDist0;
        uniY0 = distY0 / moduloDist0;


        if (Diana.GetComponent<Diana>().activated > 0)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else { GetComponent<SpriteRenderer>().color = Color.white; }

        if (moduloDist0 > 0.1)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(Speed * -uniX0), System.Convert.ToSingle(Speed * -uniY0));
        }
        else if (Xdestino != Xderecha)
        {
            Xdestino = Xderecha;
            
        }

        else if (Xdestino != Xizquierda)
        {
            Xdestino = Xizquierda;
            
        }       
    }
}

