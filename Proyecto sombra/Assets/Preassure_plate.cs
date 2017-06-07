using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preassure_plate : MonoBehaviour {
    public bool activated;
	// Use this for initialization
	void Start () {
        activated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jugador" || collision.gameObject.tag == "ShadowBox")
        {
            activated = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jugador" || collision.gameObject.tag == "ShadowBox")
        {
            activated = false;
        }
    }
}
