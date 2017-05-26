using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaglosBoss : MonoBehaviour {

    //Estadisticas básicas:
    public int phase, hp, speed;

    //Calcular ruta al jugador:
    public float distX, distY, moduloDist, uniX, uniY;

    //Condicional fase pre boss:
    bool awake;

    // Use this for initialization
    void Start () {        
        awake = false;
        hp = 10;
        speed = 3;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D obj)
    {

        if (obj.tag == "Arrow" && !awake)
        {
            awake = true;
            phase = 1;
        }
        else if (phase == 1 && (obj.tag == "Arrow" || obj.tag == "Attack"))
        {
            hp--;
        }
        else if (phase == 2 &&  obj.tag == "Attack")
        {
            hp--;
        }
        else if (phase == 3 && obj.tag == "Arrow")
        {
            hp--;
        }
    }
}
