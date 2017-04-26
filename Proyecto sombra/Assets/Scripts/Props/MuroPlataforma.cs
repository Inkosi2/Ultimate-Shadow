using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroPlataforma : MonoBehaviour {
    public Collider2D muro;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.tag = "Borde";
        
    }
    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Platform")
        {
            coll.isTrigger = true;
            muro.isTrigger = true;
        }
        /*else
        {
            coll.isTrigger = false;
            muro.isTrigger = false;
        }*/
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        coll.isTrigger = false;
        muro.isTrigger = false;
    }
}
