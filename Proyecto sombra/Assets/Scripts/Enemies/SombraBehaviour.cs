using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SombraBehaviour : MonoBehaviour {

    public double EaglosSpeed, distX, distY, moduloDist, uniX, uniY; //Detectar al jugador.
    public int fase, HP, SombraSpeed;
    public GameObject player;

    // Use this for initialization
    void Start () {
        SombraSpeed = 6;
	}
	
	// Update is called once per frame
	void Update () {
        // Vectos hacia el jugador.
        distX = transform.position.x - player.transform.position.x;
        distY = transform.position.y - player.transform.position.y;
        
        moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));
        
        uniX = distX / moduloDist;
        uniY = distY / moduloDist;

    }
}
