using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;



public class BrotherBehaviour : MonoBehaviour {

    public double distX, distY, moduloDist, uniX, uniY, mouthX, mouthY;
    float dirX, dirY;
    public int hp, fase, proyectilSpd;
    public GameObject player, RightEye, LeftEye;
    public GameObject proyectilPrefab;
    GameObject proyectil;
    public Slider HealthBar;

    // Use this for initialization
    void Start () {
        proyectilSpd = 5;
        hp = 2;
        mouthX = 0;
        mouthY = 16;
        fase = 1;
        dirX = 10;
        dirY = 10;
	}

    void OnCollisionEnter2D(Collision2D bounce)
    {
        if (bounce.gameObject.tag == "sideWall" && fase == 1)
        {
            dirX = -dirX;
            burst();
        }

        if (bounce.gameObject.tag == "horitzontalWall" && fase == 1)
        {
            dirY = -dirY;
            burst();
        }
    }

    // Update is called once per frame
    void Update () {
        //Obtener vector hacia el jugador.
        distX = mouthX - transform.position.x;
        distY = mouthY - transform.position.y;

        moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));

        uniX = distX / moduloDist;
        uniY = distY / moduloDist;

        if(RightEye.GetComponent<ShadowEyes>().hurt == true && LeftEye.GetComponent<ShadowEyes>().hurt == true)
        {
            Destroy(this.gameObject);
        }
            
        if(hp <= 0)
            {
                fase = 2;
            }
        

        if (fase == 1)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(dirX, dirY);
        }
        
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            if (moduloDist > 0.1) GetComponent<Rigidbody2D>().velocity = new Vector2((float)uniX * 10, (float)uniY * 10);

            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                darkWave();
            }
        }
    }

    void burst ()
    {
        proyectil = (GameObject)Instantiate(proyectilPrefab);
        proyectil.transform.position = new Vector2(transform.position.x, transform.position.y);
        proyectil.GetComponent<Rigidbody2D>().velocity = new Vector2(-proyectilSpd, 0);

        proyectil = (GameObject)Instantiate(proyectilPrefab);
        proyectil.transform.position = new Vector2(transform.position.x, transform.position.y);
        proyectil.GetComponent<Rigidbody2D>().velocity = new Vector2(proyectilSpd, 0);

        proyectil = (GameObject)Instantiate(proyectilPrefab);
        proyectil.transform.position = new Vector2(transform.position.x, transform.position.y);
        proyectil.GetComponent<Rigidbody2D>().velocity = new Vector2(0, proyectilSpd);

        proyectil = (GameObject)Instantiate(proyectilPrefab);
        proyectil.transform.position = new Vector2(transform.position.x, transform.position.y);
        proyectil.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -proyectilSpd);

        proyectil = (GameObject)Instantiate(proyectilPrefab);
        proyectil.transform.position = new Vector2(transform.position.x, transform.position.y);
        proyectil.GetComponent<Rigidbody2D>().velocity = new Vector2(proyectilSpd, proyectilSpd);

        proyectil = (GameObject)Instantiate(proyectilPrefab);
        proyectil.transform.position = new Vector2(transform.position.x, transform.position.y);
        proyectil.GetComponent<Rigidbody2D>().velocity = new Vector2(-proyectilSpd, proyectilSpd);

        proyectil = (GameObject)Instantiate(proyectilPrefab);
        proyectil.transform.position = new Vector2(transform.position.x, transform.position.y);
        proyectil.GetComponent<Rigidbody2D>().velocity = new Vector2(proyectilSpd, -proyectilSpd);

        proyectil = (GameObject)Instantiate(proyectilPrefab);
        proyectil.transform.position = new Vector2(transform.position.x, transform.position.y);
        proyectil.GetComponent<Rigidbody2D>().velocity = new Vector2(-proyectilSpd, -proyectilSpd);   

    }
    void darkWave ()
    {
        for (int i = 0; i < 6; i++)
        {
            float randomX1 = UnityEngine.Random.Range(0.7f, -0.7f);
            proyectil = (GameObject)Instantiate(proyectilPrefab);
            proyectil.transform.position = new Vector2(transform.position.x, transform.position.y);
            proyectil.GetComponent<Rigidbody2D>().velocity = new Vector2(randomX1 * 10, -10);
        }        
    }
    void OnTriggerEnter2D(Collider2D cono)
    {
        if ((cono.tag == "Attack" || cono.tag == "Arrow") & fase == 1)
        {
            hp--;
            HealthBar.value--;
        }
    }
}
