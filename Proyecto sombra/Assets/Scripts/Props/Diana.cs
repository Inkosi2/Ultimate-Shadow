using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour {
    public int activated;
	// Use this for initialization
	void Start () {
        activated = 0;
        GetComponent<SpriteRenderer>().color = Color.red;
	}

    // Update is called once per frame
    void Update()
    {
        if (activated > 0)
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
            activated++;
        }
    }

   /* void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            activated--;
        }
    }*/
}
