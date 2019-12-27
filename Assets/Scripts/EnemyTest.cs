using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour {
    //public float startHealth = 100f;
    //public float currentHealth;
    public Animator anim;
    public NavMeshAgent agent;
    private GameObject player;
    private bool Atacking = false;
    // Use this for initialization
    void Start () {
        Debug.Log("Setting health");
        //currentHealth = startHealth;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("player");
    }
	
	// Update is called once per frame
	void Update () {
        float dist = Mathf.Abs(Vector3.Distance(player.transform.position, transform.position));
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("AtRange"))
        {
            if (dist > 2.8f)
            {
                Atacking = false;
                anim.SetBool("Attack", false);
            }
            else anim.SetBool("AttackAgain", true);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("standing_melee_attack_horizontal"))
        {
            anim.SetBool("AttackAgain", false);
        }
            if (dist > 2.8f && Atacking == false)
        {
            agent.Resume();
            anim.SetBool("Moving", true);
            agent.SetDestination(player.transform.position);
        }
        if (dist <= 2.8f)
        {
            agent.Stop();
            Atacking = true;
            anim.SetBool("Attack", true);
            anim.SetBool("Moving", false);


        }
    }
    /*
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
        //Debug.Log("Damage was taken float");

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject, 0f);
        }
    }

    public void SetHealth(float amount)
    {
        startHealth = amount;
    }
    */
}
