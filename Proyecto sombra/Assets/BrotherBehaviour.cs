using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherBehaviour : MonoBehaviour {

    float dirX, dirY;
    int fase;
	// Use this for initialization
	void Start () {
        dirX = 10;
        dirY = 10;
	}

    void OnCollisionEnter2D(Collision2D bounce)
    {
        if (bounce.gameObject.tag == "sideWall")
        {
            dirX = -dirX;
        }

        if (bounce.gameObject.tag == "horitzontalWall")
        {
            dirY = -dirY;
        }
    }

    // Update is called once per frame
    void Update () {
        if (fase == 1)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(dirX, dirY);
        }
        else
        {

        }
    }
}
