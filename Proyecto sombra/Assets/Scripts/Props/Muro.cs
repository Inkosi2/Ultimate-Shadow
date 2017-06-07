using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muro : MonoBehaviour {
    public GameObject muro, muroInstanciado, Diana;
    bool instanciado;
	// Use this for initialization
	void Start () {
        instanciado = true;
        muro = (GameObject)Instantiate(muroInstanciado);
    }
	
	// Update is called once per frame
	void Update () {
        if (Diana.GetComponent<Diana>().activated)
        {
            Destroy(muro);
            instanciado = false;
        }
        else
        {
            if (!instanciado)
            {
                muro = (GameObject)Instantiate(muroInstanciado);
                instanciado = true;
            }
        }
	}
}
