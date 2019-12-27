using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBillScript : MonoBehaviour {

    float LifeTime = 10f;
    public float Damage = 100f;

    void Start()
    {
        StartCoroutine("Decay");
    }

    void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
            enemyHealth.TakeDamage((int) Damage);
        Destroy(gameObject);
    }

    IEnumerator Decay()
    {
        for (; ;)
        {
            if (LifeTime == 0)
                Destroy(gameObject);
            else
                LifeTime--;
            yield return new WaitForSeconds(1f);
        }
    }
}
