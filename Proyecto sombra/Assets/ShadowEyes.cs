using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEyes : MonoBehaviour {
   public  bool hurt;
	// Use this for initialization
	void Start ()
    {
        hurt = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
       if (hurt)
            GetComponent<SpriteRenderer>().color = Color.gray;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")       
            hurt = true;
        
    }
}
