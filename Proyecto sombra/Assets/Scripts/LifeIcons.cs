using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIcons : MonoBehaviour {

    public GameObject player;
    public Camera myCam;
    public int hp, php, cont;
    public GameObject luz, sombra;
    public GameObject luzP, sombraP;
    public bool inst;

    public GameObject i1, i2, i3, i4;


    // Use this for initialization
    void Start() {
        hp = player.GetComponent<PlayerController>().HP;
        php = 0;
        cont = 0;
    }

    // Update is called once per frame
    void Update() {
        hp = player.GetComponent<PlayerController>().HP;

        if (hp != php)
        {
            Destroy(i1);
            Destroy(i2);
            Destroy(i3);
            Destroy(i4);
            lifeBar();
        }

        i1.transform.position = new Vector3(System.Convert.ToSingle(player.transform.position.x - 10.5), player.transform.position.y + 4, player.transform.position.z);
        i2.transform.position = new Vector3(System.Convert.ToSingle(player.transform.position.x - 9.5), player.transform.position.y + 4, player.transform.position.z);
        i3.transform.position = new Vector3(System.Convert.ToSingle(player.transform.position.x - 8.5), player.transform.position.y + 4, player.transform.position.z);
        i4.transform.position = new Vector3(System.Convert.ToSingle(player.transform.position.x - 7.5), player.transform.position.y + 4, player.transform.position.z);

        php = hp;
    }

    void lifeBar()
    {

        inst = false;

        if (hp == 1)
        {
            i1 = (GameObject)Instantiate(luz);

            i2 = (GameObject)Instantiate(sombra);

            i3 = (GameObject)Instantiate(sombra);

            i4 = (GameObject)Instantiate(sombra);

            inst = true;
        }
        else if (hp == 2)
        {
            i1 = (GameObject)Instantiate(luz);

            i2 = (GameObject)Instantiate(luz);

            i3 = (GameObject)Instantiate(sombra);

            i4 = (GameObject)Instantiate(sombra);

            inst = true;
        }
        else if (hp == 3)
        {
            i1 = (GameObject)Instantiate(luz);

            i2 = (GameObject)Instantiate(luz);

            i3 = (GameObject)Instantiate(luz);

            i4 = (GameObject)Instantiate(sombra);

            inst = true;
        }
        else if (hp == 4)
        {
            i1 = (GameObject)Instantiate(luz);

            i2 = (GameObject)Instantiate(luz);

            i3 = (GameObject)Instantiate(luz);

            i4 = (GameObject)Instantiate(luz);

            inst = true;
        }

        else 
        {
            i1 = (GameObject)Instantiate(sombra);

            i2 = (GameObject)Instantiate(sombra);

            i3 = (GameObject)Instantiate(sombra);

            i4 = (GameObject)Instantiate(sombra);

            inst = true;
        }
    }
}


