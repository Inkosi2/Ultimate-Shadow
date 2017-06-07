using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignBehaivour : MonoBehaviour {

    public Text signText;
    public Image signImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        signText.enabled = true;
        signImage.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        signText.enabled = false;
        signImage.enabled = false;
    }
}
