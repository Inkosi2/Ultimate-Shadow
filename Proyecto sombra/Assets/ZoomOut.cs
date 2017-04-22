using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour {


    public Camera miCamara;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        miCamara.orthographicSize = 9;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Jugador")
        {
            miCamara.orthographicSize = 9;
        }
    }
}
