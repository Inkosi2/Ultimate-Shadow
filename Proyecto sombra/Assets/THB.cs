using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class THB : MonoBehaviour
{

    public GameObject lado;
    public GameObject box;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Jugador")
        {
            if (lado.name == "THB")
            {
                box.GetComponent<Rigidbody2D> ().velocity = new Vector2(0, -1);
            }
            if (lado.name == "LHB")
            {
                box.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
            }
            if (lado.name == "RHB")
            {
                box.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
            }
            if (lado.name == "DHB")
            {
                box.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
            }
        }

        if (coll.gameObject.tag == "ShadowBox")
        {
            coll.gameObject.GetComponent<Rigidbody2D>().velocity = box.GetComponent<Rigidbody2D>().velocity;
        }
    }
    
}
