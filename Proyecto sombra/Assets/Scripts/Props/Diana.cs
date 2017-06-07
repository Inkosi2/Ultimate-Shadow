using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour {
    public bool activated;
	// Use this for initialization
	void Start () {
        activated = false;
        GetComponent<SpriteRenderer>().color = Color.red;
	}

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }

        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            activated=true;
        }
    }
  
}
