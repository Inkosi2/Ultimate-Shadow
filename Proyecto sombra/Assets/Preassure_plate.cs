using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preassure_plate : MonoBehaviour {
    public int activated;
	// Use this for initialization
	void Start () {
        activated = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jugador" || collision.gameObject.tag == "ShadowBox")
        {
            activated++;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jugador" || collision.gameObject.tag == "ShadowBox")
        {
            activated--;
        }
    }
}
