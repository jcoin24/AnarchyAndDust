using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTest : MonoBehaviour {
    private int counter=0;
    public float startHealth = 100f;
    public float currentHealth;
    // Use this for initialization
    void Start () {
        currentHealth = startHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Axe"))
        {
            counter += 1;
            Debug.Log(counter);
        }
    }
    public void TakeDamage(int amount)
    {
        //Debug.Log("Damage was taken");

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject, 0f);
        }
    }
    public void TakeDamage(float amount)
    {
        //Debug.Log("Damage was taken");

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject, 0f);
        }
    }
}
