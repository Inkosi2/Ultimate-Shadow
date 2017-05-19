using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxBehaviour : MonoBehaviour {

    public GameObject Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Jugador")
        {
            if (Player.transform.position.x > transform.position.x && (Player.transform.position.y >= transform.position.y + 0.5 || Player.transform.position.y <= transform.position.y - 0.5))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
            }
            if (Player.transform.position.x < transform.position.x && (Player.transform.position.y >= transform.position.y + 0.5 || Player.transform.position.y <= transform.position.y - 0.5))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
            }
            if (Player.transform.position.y > transform.position.y && (Player.transform.position.x >= transform.position.x + 0.5 || Player.transform.position.x <= transform.position.x - 0.5))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
            }
            if (Player.transform.position.y < transform.position.y && (Player.transform.position.x >= transform.position.x + 0.5 || Player.transform.position.x <= transform.position.x - 0.5))
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
            }
        }
        if (coll.transform.tag == "Wall")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

}
