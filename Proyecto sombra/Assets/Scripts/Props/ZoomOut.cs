using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour {


    public Camera miCamara;
    public GameObject referencia, player;
    public bool onSlate;
    public int zoom;

	// Use this for initialization
	void Start () {
        zoom = 15;
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Jugador")
        {
            miCamara.orthographicSize = zoom;
            //miCamara.transform.position = new Vector2(referencia.transform.position.x, referencia.transform.position.y);
            miCamara.GetComponent<CameraMove>().fixOnPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Jugador")
        {
            miCamara.orthographicSize = 5;
            miCamara.transform.position = player.transform.position;
            miCamara.GetComponent<CameraMove>().fixOnPlayer = true;
        }
    }

}
