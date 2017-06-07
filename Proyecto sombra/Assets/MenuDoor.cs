using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuDoor : MonoBehaviour
{

    public GameObject D1, D2, D3, D4, D5, D6;
    public GameObject player;


    public int output;

    // Use this for initialization
    void Start()
    {
        player.GetComponent<PlayerBehaviour>().ammo = 1;
    }

    // Update is called once per frame
    void Update()
    {







    }

    void OnTriggerEnter2D(Collider2D collision)   // <------------------------------------------------------ Faltan nombres de las escenas
    {
        if (collision.gameObject.tag == "Jugador")
        {
            if (D1.GetComponent<Diana>().activated)
            {
                SceneManager.LoadScene("Eaglos");
                GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else if (D2.GetComponent<Diana>().activated)
            {
                SceneManager.LoadScene("La sombra de los bosques");
                GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else if (D3.GetComponent<Diana>().activated)
            {
                SceneManager.LoadScene("Sombra Maestra");
                GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else if (D4.GetComponent<Diana>().activated)
            {
                SceneManager.LoadScene("Puzzle");
                GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else if (D5.GetComponent<Diana>().activated)
            {
                SceneManager.LoadScene("Creditos");
                GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else if (D6.GetComponent<Diana>().activated)
            {
                Application.Quit();
                GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
    }
}


