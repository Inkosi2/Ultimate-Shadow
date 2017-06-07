using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuenteQueAparece : MonoBehaviour {
    public GameObject diana, bridge;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (diana.GetComponent<Diana>().activated)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            bridge.GetComponent<SpriteRenderer>().enabled = true;            
        }
	}
}
