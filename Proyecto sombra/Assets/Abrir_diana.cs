using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abrir_diana : MonoBehaviour {
    public GameObject PP1;
    public GameObject PP2;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		if (PP1.GetComponent<Preassure_plate>().activated == true && PP2.GetComponent<Preassure_plate>().activated == true)
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
