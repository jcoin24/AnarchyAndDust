using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartScript : MonoBehaviour {

    public GameObject Panel;
	// Use this for initialization
	void Start () {
        BoxCollider myCollider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        int health = other.GetComponent<PlayerMovement>().playerHealth;
        Panel.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Panel.gameObject.SetActive(false);
    }
    
}

