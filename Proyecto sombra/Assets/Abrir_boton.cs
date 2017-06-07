using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abrir_boton : MonoBehaviour {
    public GameObject Diana;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Diana.GetComponent<Diana>().activated)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
