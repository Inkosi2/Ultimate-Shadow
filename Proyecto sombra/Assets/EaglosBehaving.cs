using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaglosBehaving : MonoBehaviour {

    float EaglosSpeed;
    double BossPlayerAngle;
    public GameObject player;

	// Use this for initialization
	void Start () {
        EaglosSpeed = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
        BossPlayerAngle = Math.Atan((transform.position.y-player.transform.position.y)/(transform.position.x - player.transform.position.x));
        //GetComponent<Rigidbody2D>().velocity = new Vector2 ()
	}
}
