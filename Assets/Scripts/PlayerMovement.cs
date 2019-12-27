using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Player speed
    public float speed = 9f;
    public int playerHealth = 100;
    public Text healthText;
    public AudioClip takeDamageSoundEffect;
    AudioSource AS;

    // Vector for moving the player
    Vector3 movement;
    // Rigidbody for player physics
    Rigidbody playerRigidbody;
    // Reference to the floor mask
    int floorMask;
    // Lenght of the ray for player rotation
    float camRayLength = 100f;

    int count = 0;
    //player animation values
    private float tempF;
    private float tempS;
    private float foward;
    private float sideway;
    private float tempEuler;
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        // Set floor mask and get reference to the player 
        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = GetComponent<Rigidbody>();

        AS = GetComponent<AudioSource>();
        AS.clip = takeDamageSoundEffect;
    }

    private void Update()

    {
        if(playerHealth <= 0)
        {
            Debug.Log("Player is dead");
            SceneManager.LoadScene("GameOver Scene");
        }
        healthText.text = "HEALTH: " + playerHealth;
    }


    private void FixedUpdate()
    { 
        // Get user input
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move and turn the player
        Move(h,v);
        Turning();
        //Animate player
        animatePlayer();

    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotatation);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Axe"))
        {
            playerHealth -= 10;
            AS.Play();
            Debug.Log("Player Hit");
        }
    }

    public void TakeDamage(int amount)
    {
        //Debug.Log("Damage was taken");

        playerHealth -= amount;

        /*
        if (playerHealth <= 0)
        {
            Destroy(gameObject, 0f);
        }
        */
    }

    public void AddHealth(int amount)
    {
        playerHealth += amount;
    }



    void animatePlayer()
    {
        foward = 0f;
        sideway = 0f;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float y = transform.eulerAngles.y;
        if (y > 180)
        {
            tempEuler = y - 180;
            y = -180 + tempEuler;
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            tempS = -1 / ((180f / y) / 2f);
            if (tempS < -1f)
            {
                tempS = (tempS + 2f) * -1f;
                tempF = (1f + tempS) * -1f;
            }
            else if (tempS > 1)
            {
                tempS = 2f - tempS;
                tempF = (1f - tempS) * -1f;
            }
            else if (tempS > 0 && tempS < 1f)
            {
                tempF = 1 - tempS;
            }
            else if (tempS < 0 && tempS > -1f)
            {
                tempF = 1f + tempS;

            }
        }
        foward = tempF;
        sideway = tempS;
        tempReset();
        if (Input.GetAxis("Vertical") < 0)
        {
            tempS = -1 / ((180f / y) / 2f);
            if (tempS < -1f)
            {
                tempS = (tempS + 2f) * -1f;
                tempF = (1f + tempS) * -1f;
            }
            else if (tempS > 1)
            {
                tempS = 2f - tempS;
                tempF = (1f - tempS) * -1f;
            }
            else if (tempS > 0 && tempS < 1f)
            {
                tempF = 1 - tempS;
            }
            else if (tempS < 0 && tempS > -1f)
            {
                tempF = 1f + tempS;

            }
            tempF = tempF * -1f;
            tempS = tempS * -1f;
        }
        foward = foward + tempF;
        sideway = sideway + tempS;
        tempReset();
        if (y < -90f)
        {
            y = (-360f - (y - 90f)) * -1f;
        }
        else y = y - 90;

        if (Input.GetAxis("Horizontal") > 0)
        {
            tempS = -1 / ((180f / y) / 2f);
            if (tempS < -1f)
            {
                tempS = (tempS + 2f) * -1f;
                tempF = (1f + tempS) * -1f;
            }
            else if (tempS > 1)
            {
                tempS = 2f - tempS;
                tempF = (1f - tempS) * -1f;
            }
            else if (tempS > 0 && tempS < 1f)
            {
                tempF = 1 - tempS;
            }
            else if (tempS < 0 && tempS > -1f)
            {
                tempF = 1f + tempS;

            }
        }

        foward = foward + tempF;
        sideway = sideway + tempS;
        tempReset();
        if (Input.GetAxis("Horizontal") < 0)
        {
            tempS = -1 / ((180f / y) / 2f);
            if (tempS < -1f)
            {
                tempS = (tempS + 2f) * -1f;
                tempF = (1f + tempS) * -1f;
            }
            else if (tempS > 1)
            {
                tempS = 2f - tempS;
                tempF = (1f - tempS) * -1f;
            }
            else if (tempS > 0 && tempS < 1f)
            {
                tempF = 1 - tempS;
            }
            else if (tempS < 0 && tempS > -1f)
            {
                tempF = 1f + tempS;

            }
            tempF = tempF * -1f;
            tempS = tempS * -1f;
        }
        foward = foward + tempF;
        sideway = sideway + tempS;
        if ((Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0) && (Input.GetAxis("Vertical") < 0 || Input.GetAxis("Vertical") > 0))
        {
            foward = foward / 2f;
            sideway = sideway / 2f;
        }

        tempReset();
        //Debug.Log("F " + foward);
        //Debug.Log("S " + sideway);
        anim.SetFloat("Foward", foward);
        anim.SetFloat("Sideways", sideway);

    }
    void tempReset()
    {
        tempS = 0;
        tempF = 0;
    }


}
