using UnityEngine;

    public class PlayerShooting : MonoBehaviour
    {
        public bool AllWeaponsEnabled = false;
        public int damagePerShot = 20;                  // The damage inflicted by each bullet.
        public float timeBetweenBullets = 0.15f;        // The time between each shot.
        public float range = 100f;                      // The distance the gun can fire.


        float timer;                                    // A timer to determine when to fire.
        Ray shootRay = new Ray();                       // A ray from the gun end forwards.
        RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
        int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
        LineRenderer gunLine;                           // Reference to the line renderer.
        AudioSource gunAudio;                           // Reference to the audio source.
        Light gunLight;                                 // Reference to the light component.
		public Light faceLight;								// Duh
        float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
        public GameObject gun;
        public WeaponObject[] weapons;
        public int currentWeapon = 0;
        //public Mesh gunMesh;
        //public Mesh mesh = new Mesh();


        void Awake ()
        {
            // Create a layer mask for the Shootable layer.
            shootableMask = LayerMask.GetMask ("Shootable");

            // Set up the references.
            gunLine = GetComponent <LineRenderer> ();
            gunLine.material = weapons[currentWeapon].gunLine;
            
            gunAudio = GetComponent<AudioSource> ();
            gunAudio.clip = weapons[currentWeapon].gunAudio;

            gunLight = GetComponent<Light> ();
            gunLight = weapons[currentWeapon].gunLight;
			faceLight = GetComponentInChildren<Light> ();

            //gunMesh = GetComponent<Mesh>();
            //gunMesh = weapons[currentWeapon].gunMesh;
        }


        void Update ()
        {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            switchWeapon();

            // If the Fire1 button is being press and it's time to fire...
			if(Input.GetButton("Fire1") && timer >= weapons[currentWeapon].fireRate && Time.timeScale != 0)
            {
                // ... shoot the gun.
                Shoot ();
            }

            // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
            if(timer >= weapons[currentWeapon].fireRate * effectsDisplayTime)
            {
                // ... disable the effects.
                DisableEffects ();
            }
        }


        public void DisableEffects ()
        {
            // Disable the line renderer and the light.
            gunLine.enabled = false;
			faceLight.enabled = false;
            gunLight.enabled = false;
        }


        void Shoot ()
        {
            // Reset the timer.
            timer = 0f;

            // Play the gun shot audioclip.
            gunAudio.Play();

            // Enable the lights.
            gunLight.enabled = true;
			faceLight.enabled = true;

            // Enable the line renderer and set it's first position to be the end of the gun.
            gunLine.enabled = true;
            gunLine.SetPosition (0, transform.position);

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            // Perform the raycast against gameobjects on the shootable layer and if it hits something...
            if(Physics.Raycast(shootRay, out shootHit, weapons[currentWeapon].range, shootableMask))
            {
                // Try and find an EnemyHealth script on the gameobject hit.
                EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

                // If the EnemyHealth component exist...
                if (enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    enemyHealth.TakeDamage(weapons[currentWeapon].damage);                 
                }

                // Set the second position of the line renderer to the point the raycast hit.
                gunLine.SetPosition (1, shootHit.point);
            }
            // If the raycast didn't hit anything on the shootable layer...
            else
            {
                // ... set the second position of the line renderer to the fullest extent of the gun's range.
                gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
            }
        }

        public void SetWeapon(int num)
        {
            currentWeapon = num;
            gunAudio.clip = weapons[currentWeapon].gunAudio;
            gunLine.material = weapons[currentWeapon].gunLine;
            gunLight = weapons[currentWeapon].gunLight;
        }

        private void switchWeapon()
        {
            if (AllWeaponsEnabled == true)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    if (currentWeapon < weapons.Length - 1) //4 weapons
                    {
                        currentWeapon++;
                        gunAudio.clip = weapons[currentWeapon].gunAudio;
                        gunLine.material = weapons[currentWeapon].gunLine;
                        gunLight = weapons[currentWeapon].gunLight;
                        //gunMesh = weapons[currentWeapon].gunMesh;
                        //gun.GetComponent<MeshFilter>().mesh = gunMesh;
                    }
                    else
                    {
                        currentWeapon = 0;
                        gunAudio.clip = weapons[currentWeapon].gunAudio;
                        gunLine.material = weapons[currentWeapon].gunLine;
                        gunLight = weapons[currentWeapon].gunLight;
                        //gunMesh = weapons[currentWeapon].gunMesh;
                        //gun.GetComponent<MeshFilter>().mesh = gunMesh;
                    }
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    if (currentWeapon > 0)
                    {
                        currentWeapon--;
                        gunAudio.clip = weapons[currentWeapon].gunAudio;
                        gunLine.material = weapons[currentWeapon].gunLine;
                        gunLight = weapons[currentWeapon].gunLight;
                        //gunMesh = weapons[currentWeapon].gunMesh;
                        //gun.GetComponent<MeshFilter>().mesh = gunMesh;
                    }
                    else
                    {
                        currentWeapon = weapons.Length - 1; //4 weapons
                        gunAudio.clip = weapons[currentWeapon].gunAudio;
                        gunLine.material = weapons[currentWeapon].gunLine;
                        gunLight = weapons[currentWeapon].gunLight;
                        //gunMesh = weapons[currentWeapon].gunMesh;
                        //gun.GetComponent<MeshFilter>().mesh = gunMesh;
                    }
                }
            }
        }
    }
