using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffects : MonoBehaviour {

    AudioSource AS;
    float playerHealth;
    public AudioClip clip;
    float enemyHealth;

	// Use this for initialization
	void Start () {
        AS = GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update () {
        enemyHealth = GetComponent<EnemyHealth>().currentHealth;
        if (enemyHealth <= 0)
        {
            AS.clip = clip;
            AS.Play();
        }
    }
}
