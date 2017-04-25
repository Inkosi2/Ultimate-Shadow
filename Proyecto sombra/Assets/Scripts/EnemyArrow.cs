using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour {

    public GameObject player;
    public double distX, distY, moduloDist, uniX, uniY;
    int Speed;

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Jugador")
        {
            player.GetComponent<PlayerController>().HP--;
        }
    }
}
