using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CompleteProject
{
    public class EnemyGun : MonoBehaviour
    {

        public int damagePerShot = 2;

        public float timeBetweenBullets = 0.15f;
        public float range = 100f;

        float timer;
        Ray shootRay;
        RaycastHit shootHit;
        int shootableMask;
        LineRenderer gunLine;
        Light gunLight;
        float effectsDisplayTime = 0.2f;

        AudioSource AS;
        // Use this for initialization
        void Awake()
        {
            AS = GetComponentInParent<AudioSource>();
            shootableMask = LayerMask.GetMask("ShootableByShooter");
            gunLine = GetComponent<LineRenderer>();
            gunLight = GetComponent<Light>();
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenBullets * effectsDisplayTime)
            {
                // ... disable the effects.
                DisableEffects();
            }
        }
        public void DisableEffects()
        {
            // Disable the line renderer and the light.
            gunLine.enabled = false;
            gunLight.enabled = false;
        }
        void aim()
        {

        }
        public void fire()
        {
            Debug.Log("Firing");
            // Reset the timer.
            timer = 0f;

            // Enable the light.
            gunLight.enabled = true;

            AS.Play(); //play shooty shooty noise
            // Enable the line renderer and set it's first position to be the end of the gun.
            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                Debug.Log("Player has been shot");
                // Try and find an PlayerHealth script on the gameobject hit.
                PlayerMovement playerHealth = shootHit.collider.GetComponent<PlayerMovement>();

                // If the HitTest component exist...
                if (playerHealth != null)
                {

                    // ... the enemy should take damage.
                    playerHealth.TakeDamage(damagePerShot);
                }

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                // ... set the second position of the line renderer to the fullest extent of the gun's range.
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }
    }
}
