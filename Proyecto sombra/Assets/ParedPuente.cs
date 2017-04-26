using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedPuente : MonoBehaviour {
    public GameObject Diana;
    public Collider2D collider;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Diana.GetComponent<Diana>().activated > 0)
        {
            collider.isTrigger = true;
        }
        else
        {
            collider.isTrigger = false;
        }
	}
}
