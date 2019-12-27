using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float startHealth = 100f;
    public float currentHealth;
    public int points = 10;
    public GameObject player;


    // Use this for initialization
    void Start () {
        currentHealth = startHealth;
        player = GameObject.FindGameObjectWithTag("player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int amount)
    {
        //Debug.Log("Damage was taken");

       
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            if (gameObject.name.Contains("Enemy3"))
            {
                points = 1000;
            }
            Destroy(gameObject, 0f);
            PlayerScore score = player.GetComponent<PlayerScore>();
            score.AddScore(points);
        }
    }

    public void SetHealth(float amount)
    {
        startHealth = amount;
    }
}
