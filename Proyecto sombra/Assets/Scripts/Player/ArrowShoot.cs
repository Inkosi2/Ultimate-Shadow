using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour {

    float velVuelo;
    bool up, down, left, right;
    public bool shot;
    public bool collided;
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        velVuelo = 10;
        shot = false;
        collided = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!collided)
        {
            if (!shot)
            {
                //transform.position = player.transform.position;

                //Lanzamiento de flecha
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    up = true;
                    down = false;
                    left = false;
                    right = false;
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 270);
                    up = false;
                    down = true;
                    left = false;
                    right = false;
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    up = false;
                    down = false;
                    left = true;
                    right = false;
                    transform.localRotation = Quaternion.Euler(0, 0, 180);
                }

                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    up = false;
                    down = false;
                    left = false;
                    right = true;
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }

            if (up)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, velVuelo);

                shot = true;
            }
            else if (left)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-velVuelo, 0);

                shot = true;
            }
            else if (down)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velVuelo);

                shot = true;
            }
            else if (right)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(velVuelo, 0);

                shot = true;
            }

        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        
        if (coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Diana" || coll.gameObject.tag == "Bloque movil" || coll.gameObject.tag == "ShadowBox")
        {
            Debug.Log("Collide with " + coll.gameObject.tag);
            if (coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Diana" || coll.gameObject.tag == "Bloque movil" || coll.gameObject.tag == "ShadowBox")
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                collided = true;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Diana" || collision.gameObject.tag == "Bloque movil" || collision.gameObject.tag == "ShadowBox")
        {
            Debug.Log("Collide with " + collision.gameObject.tag);
            if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Diana" || collision.gameObject.tag == "Bloque movil" || collision.gameObject.tag == "ShadowBox")
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                collided = true;
            }
        }
    }
}





//collisionEnter con el tag de la pared, si chocan fuerza contraria
//Objeto vacío, cuando se pulse la tecla, "instanciate" (prefab, posición, rotación)