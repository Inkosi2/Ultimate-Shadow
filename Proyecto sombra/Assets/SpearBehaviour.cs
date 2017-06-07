using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBehaviour : MonoBehaviour
{

    public GameObject player;
    public GameObject mori;
    
    public bool hit;

    // Use this for initialization
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }

    void OnCollisionEnter2D(Collision2D colli)
    {
        if (colli.gameObject.tag == "Wall" || colli.gameObject.tag == "ShadowBox")
        {
            Destroy(this.gameObject);
        }
    }    
}
