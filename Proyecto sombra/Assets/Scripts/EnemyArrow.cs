using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour {

    public GameObject player;
    public double distX, distY, moduloDist, uniX, uniY;
    

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter2D(Collision2D colli)
    {
        Debug.Log(colli.gameObject.tag);
        if (colli.gameObject.tag == "Jugador")
        {
            player.GetComponent<PlayerController>().HP--;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll.gameObject.tag);

        if (coll.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
