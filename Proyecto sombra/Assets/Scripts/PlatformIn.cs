using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIn : MonoBehaviour {

    public GameObject player;
    Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        offset = player.transform.position - transform.position;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Jugador")
        {
            player.transform.position = transform.position + offset;
        }
    }
}
