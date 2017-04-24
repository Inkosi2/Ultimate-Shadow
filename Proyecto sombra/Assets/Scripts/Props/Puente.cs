using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puente : MonoBehaviour {
    public GameObject puente, puenteInstanciado, Diana;
    bool instanciado;
	// Use this for initialization
	void Start () {
        instanciado = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Diana.GetComponent<Diana>().activated)
        {
            if (!instanciado)
            {
                puente = (GameObject)Instantiate(puenteInstanciado);
                instanciado = true;
            }
        }
        else
        {
            Destroy(puente);
        }
	}
}
