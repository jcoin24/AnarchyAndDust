using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MScript : MonoBehaviour {

    public AudioClip[] PlayableSounds;
    public AudioClip EndSound;
    public GameObject DetachedSoundPrefab;
    public GameObject CurrentTarget = null;

    float LifeTime = 30f;

    void Awake()
    {
        AudioSource AS = GetComponent<AudioSource>();
        if (AS)
        {
            AS.clip = PlayableSounds[Random.Range(0, PlayableSounds.Length - 1)];
            AS.Play();
        }
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    void Start()
    {
        StartCoroutine("Decay");
    }

    void Update()
    {
        AudioSource AS = GetComponent<AudioSource>();
        if (AS && !AS.isPlaying)
        {
            AS.clip = PlayableSounds[Random.Range(0, PlayableSounds.Length - 1)];
            AS.Play();
        }
        if (CurrentTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentTarget.transform.position, 6.0f * Time.deltaTime);
            transform.LookAt(CurrentTarget.transform);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();
            if (enemyHealth)
                enemyHealth.TakeDamage(int.MaxValue);
            if (DetachedSoundPrefab)
            {
                GameObject ds = Instantiate(DetachedSoundPrefab, Vector3.zero, Quaternion.identity);
                DetachedSoundScript dss = ds.GetComponent<DetachedSoundScript>();
                if (dss)
                    dss.ClipToPlay = EndSound;
            }
            Destroy(gameObject);
        }
    }

    IEnumerator Decay()
    {
        for (; ; )
        {
            if (LifeTime == 0)
                Destroy(gameObject);
            else
                LifeTime--;
            yield return new WaitForSeconds(1f);
        }
    }
}
