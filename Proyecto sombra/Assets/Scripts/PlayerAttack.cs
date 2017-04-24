using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public GameObject cono;
    public GameObject player;
    public double time;
    public bool attacking, pressedE, previousPressedE;

	// Use this for initialization
	void Start () {
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        pressedE = Input.GetKey(KeyCode.E);
        time += Time.deltaTime;
        if (Input.GetKey(KeyCode.E) && !attacking)
        {
            time = 0;
            cono = (GameObject)Instantiate(cono);
            cono.transform.position = new Vector2(player.transform.position.x, System.Convert.ToSingle(player.transform.position.y - 0.8));
            //cono.transform.position = player.transform.position;
            attacking = true;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        else if (time >= 0.2 && attacking)
        {
            Destroy(cono);
            attacking = false;
        }
        previousPressedE = pressedE;
	}
}
