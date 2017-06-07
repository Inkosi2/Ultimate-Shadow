using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBehaviour : MonoBehaviour
{

    public GameObject player;
    public GameObject mori;
    double distX, distY, moduloDist, uniX, uniY, time;
    public bool hit, flying;

    // Use this for initialization
    void Start()
    {
        time = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D colli)
    {
        Debug.Log(colli.gameObject.tag);
        if (colli.gameObject.tag == "ShadowBox")
        {
            hit = true;
        }
        if (colli.gameObject.tag == "Player")
        { 
            player.GetComponent<PlayerController>().HP--;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll.gameObject.tag);
        
        if (coll.gameObject.tag == "Wall")
        {
            mori.GetComponent<SombraBehaviour>().flyingArrow = false;
            Destroy(this.gameObject);
        }
    }
}
