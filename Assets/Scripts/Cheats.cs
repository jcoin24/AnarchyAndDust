using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

    GameObject player;
    CheatManager state;

	void Start () {
        player = GameObject.FindGameObjectWithTag("player");
    }
	
	// Update is called once per frame
	void Update () {
            KillAllCheat();
            InvincibilityCheat();
            GetMoneyCheat();
            UnlockAllWeaponsCheat();
            SkipRoundCheat();
        
	}

    void KillAllCheat()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Killing enemies");
            GameObject[] enemiesOnMap = GameObject.FindGameObjectsWithTag("enemy");
            foreach (GameObject e in enemiesOnMap)
            {
                e.GetComponent<EnemyHealth>().currentHealth = 0;
                e.GetComponent<EnemyHealth>().TakeDamage(1);
            }
        }
    }

    void InvincibilityCheat()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Invincibility");
            player.GetComponent<PlayerMovement>().playerHealth = 999999999;
        }
    }

    void GetMoneyCheat()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Add Money");
            player.GetComponent<PlayerScore>().AddScore(1000);
        }
    }

    void UnlockAllWeaponsCheat()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("All weapons unlocked");
            player.GetComponentInChildren<PlayerShooting>().AllWeaponsEnabled = true; //toggle
        }
    }

    void SkipRoundCheat()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            GameObject.Find("SpawnManager").GetComponent<SpawnManager>().WaveComplete();
        }
    }
}
