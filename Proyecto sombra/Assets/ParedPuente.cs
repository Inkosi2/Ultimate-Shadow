using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedPuente : MonoBehaviour {
    public GameObject Diana;
    public Collider2D coll;
	// Use this for initialization
	void Start () {
        coll.isTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Diana.GetComponent<Diana>().activated > 0)
        {
            coll.isTrigger = true;
        }
        else
        {
            coll.isTrigger = false;
        }
	}
}
