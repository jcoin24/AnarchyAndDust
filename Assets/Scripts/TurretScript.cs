using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TurretScript : MonoBehaviour {

    public Vector3 UpPoint = Vector3.zero;
    public Vector3 SpawnOffset = Vector3.zero;
    public bool ShouldRotate = true;
    public float Range;
    public float TimeBetweenFires;
    public GameObject LocationToFireFrom;
    public string WhatToFire;
    public GameObject PrefabToFire;
    public GameObject CurrentTarget;
    public float rotation = 10;

    public float Damage = 100f;
    bool CanFire = true;
    Vector3 SpawnPosition = Vector3.zero;
    float FireTimer;
    float Timer;
    float LineEffectDisplayTime = 0.2f;
    AudioSource AS;
    SpawnManager sm;

    void Awake()
    {
        GameObject SpawnManager = GameObject.Find("SpawnManager");
        if (SpawnManager)
            sm = SpawnManager.GetComponent<SpawnManager>();
    }

    public int cost;
    public string turretName;

    // Use this for initialization
    void Start() {
        if (gameObject.name.Contains("Thwomp"))
            CanFire = false;
        SpawnPosition = transform.position;
        AS = GetComponent<AudioSource>();
        StartCoroutine("ShootTimer");
    }

    // Update is called once per frame
    void Update() {
        Timer += Time.deltaTime;

        if (sm)
        {
            if (sm.healthRound > sm.mutexRound)
            {
                Damage *= 1.1f;
            }
        }

        if (Timer >= TimeBetweenFires * LineEffectDisplayTime)
        {
            LineRenderer LR = GetComponent<LineRenderer>();
            if (LR)
                LR.enabled = false;
        }

        if (CurrentTarget == null)
        {
            if (ShouldRotate)
            {
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                transform.Rotate(0, rotation * Time.deltaTime, 0);
            }

            // Find a target to aim at
            GameObject PotentialTarget = GameObject.FindGameObjectWithTag("enemy");
            if (PotentialTarget)
            {
                var distance = PotentialTarget.transform.position - transform.position;
                if (distance.magnitude <= Range)
                    CurrentTarget = PotentialTarget;
            }
        }
        else
        {
            // Check to make sure target is still within range
            var distance = CurrentTarget.transform.position - transform.position;
            if (distance.magnitude > Range)
            {
                CurrentTarget = null;
                return;
            }

            if (ShouldRotate)
                transform.LookAt(CurrentTarget.transform);
        }

        if (!CanFire && UpPoint != Vector3.zero)
        {
            Vector3 CalculatedUpPoint = SpawnPosition + UpPoint;
            if (CalculatedUpPoint != transform.position)
            {
                if (Vector3.Distance(CalculatedUpPoint, transform.position) <= 0.01f)
                {
                    transform.position = CalculatedUpPoint;
                    CanFire = true;
                }
                else
                    transform.position = Vector3.Lerp(transform.position, CalculatedUpPoint, Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (WhatToFire == "AOE")
        {
            if (collision.collider.CompareTag("floor"))
            {
                if (AS && !AS.isPlaying)
                    AS.Play();
                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("enemy");
                foreach (GameObject gameObject in gameObjects)
                {
                    var distance = gameObject.transform.position - transform.position;
                    if (distance.magnitude <= Range)
                    {
                        EnemyHealth enemyHealth = gameObject.GetComponent<EnemyHealth>();
                        if (enemyHealth != null)
                            enemyHealth.TakeDamage((int) (Damage / 2.0f));
                    }
                }
                CanFire = false;
            }
        }
    }

    IEnumerator ShootTimer()
    {
        for (;;)
        {
            if (CanFire)
            {
                if (FireTimer <= 0)
                {
                    if (WhatToFire != null && WhatToFire != "")
                    {
                        switch (WhatToFire)
                        {
                            case "Raycast":
                                {
                                    if (CurrentTarget)
                                    {
                                        RaycastHit hit;
                                        var heading = CurrentTarget.transform.position - transform.position;
                                        var direction = heading / heading.magnitude;

                                        LineRenderer LR = GetComponent<LineRenderer>();
                                        if (LR)
                                        {
                                            LR.enabled = true;
                                            LR.SetPosition(0, LocationToFireFrom != null ? LocationToFireFrom.transform.position : transform.position);

                                            if (Physics.Raycast(transform.position, direction, out hit))
                                            {
                                                if (hit.collider && hit.collider.gameObject && hit.collider.gameObject.CompareTag("enemy"))
                                                {
                                                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                                                    if (enemyHealth != null)
                                                        enemyHealth.TakeDamage((int) Damage);
                                                }
                                                LR.SetPosition(1, hit.point);
                                            }
                                            else
                                            {
                                                LR.SetPosition(1, (LocationToFireFrom != null ? LocationToFireFrom.transform.position : transform.position) + direction * Range);
                                            }    
                                        }
                                        else
                                        {
                                            if (Physics.Raycast(transform.position, direction, out hit))
                                                if (hit.collider && hit.collider.gameObject && hit.collider.gameObject.CompareTag("enemy"))
                                                {
                                                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                                                    if (enemyHealth != null)
                                                        enemyHealth.TakeDamage((int) Damage);
                                                }
                                        }
                                        if (AS && !AS.isPlaying)
                                            AS.Play();
                                    }
                                    break;
                                }
                            case "Bullet":
                                {
                                    if (CurrentTarget && PrefabToFire)
                                    {
                                        Quaternion RotationOfFiringObject = LocationToFireFrom != null ? LocationToFireFrom.transform.rotation : transform.rotation;
                                        RotationOfFiringObject.eulerAngles = new Vector3(RotationOfFiringObject.eulerAngles.x + 90f, RotationOfFiringObject.eulerAngles.y, RotationOfFiringObject.eulerAngles.z);
                                        GameObject Bullet = Instantiate(PrefabToFire, LocationToFireFrom != null ? LocationToFireFrom.transform.position : transform.position, RotationOfFiringObject);
                                        if (Bullet)
                                        {
                                            MScript M = Bullet.GetComponent<MScript>();
                                            if (M)
                                            {
                                                M.CurrentTarget = CurrentTarget;
                                            }
                                            else
                                            {
                                                Rigidbody rigidBody = Bullet.GetComponent<Rigidbody>();
                                                if (rigidBody)
                                                {
                                                    BulletBillScript bbs = Bullet.GetComponent<BulletBillScript>();
                                                    if (bbs)
                                                        bbs.Damage = Damage;
                                                    Vector3 FiringForce = LocationToFireFrom != null ? LocationToFireFrom.transform.forward * 20 : transform.forward * 20;
                                                    rigidBody.velocity = FiringForce;
                                                }
                                            }
                                        }
                                        if (AS && !AS.isPlaying)
                                            AS.Play();
                                    }
                                    break;
                                }
                            case "AOE":
                                {
                                    Rigidbody rb = GetComponent<Rigidbody>();
                                    if (rb)
                                    {
                                        rb.AddForce(transform.forward * -10, ForceMode.Impulse);
                                    }
                                    break;
                                }
                        }
                    }
                    FireTimer = TimeBetweenFires;
                }
                else
                {
                    // Decrement timer
                    FireTimer -= 0.2f;
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
