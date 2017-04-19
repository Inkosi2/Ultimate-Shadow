using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaglosBehaving : MonoBehaviour {

    double EaglosSpeed, distX, distY, moduloDist, uniX, uniY;
 
    //double BossPlayerAngle;
    public GameObject player;

	// Use this for initialization
	void Start () {
        EaglosSpeed = 3.5d;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //BossPlayerAngle = Math.Atan((transform.position.y-player.transform.position.y)/(transform.position.x - player.transform.position.x));
        //GetComponent<Rigidbody2D>().velocity = new Vector2 ()

        /*if (transform.position.x < (player.transform.position.x + 2)&& transform.position.y < (player.transform.position.x + 2))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-EaglosSpeed, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -EaglosSpeed);
             Debug.Log("1");
        }

        if (transform.position.x < (player.transform.position.x + 2) && transform.position.y > (player.transform.position.x + 2))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-EaglosSpeed, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, EaglosSpeed);
             Debug.Log("2"); 
        }

        if (transform.position.x > (player.transform.position.x + 2)&& transform.position.y < (player.transform.position.x + 2))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(EaglosSpeed, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -EaglosSpeed);
            Debug.Log("2");
        }

        if (transform.position.x > (player.transform.position.x + 2) && transform.position.y > (player.transform.position.x + 2))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(EaglosSpeed, 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, EaglosSpeed);
            Debug.Log("2");
        }


        else { Debug.Log("Eaglos no ve nada"); }*/

        distX = transform.position.x - player.transform.position.x;
        distY = transform.position.y - player.transform.position.y;

        moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));

        uniX = distX / moduloDist;
        uniY = distY / moduloDist;

        GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle( EaglosSpeed * uniX), System.Convert.ToSingle( EaglosSpeed * uniY));
    }
}
