using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{

    //public GameManager gameManager; //Links the gamemanager script
    //public GenerateEnemies generateEnemies; // links the generateEnemies script



    public CharacterController controller; //looks for the CharacterController which a component that puts the usual seperate character movements such as Rigidbody and box collider into one component.
    //Public Character Variables
    public float speed = 12f;
    public float sprintSpeed = 18f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f; 

    public float health = 100;
    public float maxHealth = 100;
    public float stamina = 5;
    public float maxStamina = 5;

    public HealthBar healthBar;

    public StaminaBar staminaBar;

    //Used to check if the character is sprinting (holding down shift)
    private bool isRunning;

    

    public Gun gun; //Links the gun script for the Reload() function for ammo

    Camera cam;
    //public Gun gun; //Links the gun script for the Reload() function for ammo

    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    Vector3 velocity;
    bool isGrounded;

    //killScore
    public int killCount;
    public Text killScore;
    

    void Start()
    {
        

        // killScore.text = "10 / "+killCount.ToString();

        DoorController.doorIsOpening = false;
        Timer.startRace = false;
        WinButton.victory = false;

        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        stamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

        cam = Camera.main;


    }        




    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            health -= 1;
            healthBar.SetHealth(health);
        }
    //Checks to see if the player is on the ground so the yposition isnt infinitely increasing when a character moves y positions.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x +transform.forward * z;



        //i was getting an error where it didnt stop sprinting so i had to set a cut off of how much stamina you need to have to sprint.
        //this error was happening because it was gaining stamina once it hit 0 and then using it straight away so it was fluctuating between 0~ and 0.4~

        //if the shift key is pressed you sprint else you just walk at normal speed
        //once you are below 0.5 stamina you will stop sprinting.
        if(Input.GetKey(KeyCode.LeftShift)  && stamina > 0.5)
        {
            isRunning = true;
            controller.Move(move * sprintSpeed * Time.deltaTime);
            // Debug.Log("down");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || stamina <= 0)
        {
            controller.Move(move * speed * Time.deltaTime);
            isRunning = false;
            // Debug.Log("up");
        }
    	else
        {
            //move player at normal speed.
            controller.Move(move * speed * Time.deltaTime);
        }

        // if isRunning is true, stamina is decreased, if stamina is 0 isRunning is set to false.
        if(isRunning)
        {
            stamina -= Time.deltaTime;
            //this is to update the actual stamina bar not just the number
            staminaBar.SetStamina(stamina);
            if(stamina < 0)
            {
                stamina = 0;
                isRunning = false;
            }
        } 
        //if stamina is less than maxStamina the stamina will refill.
        else if (stamina < maxStamina)
        {
            stamina += Time.deltaTime;
            //this is to update the actual stamina bar not just the number
            staminaBar.SetStamina(stamina);
        }


    //Sets it so that the player can only jump when isGrounded is true
        if(Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        
        
        
        // Debug.Log(WinButton.victory);
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100))
            {
                //checking if the object hit has the interactable script on it.
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null) 
                {
                    //checking if the object hit has the win button script on it
                    WinButton winButton = hit.collider.GetComponent<WinButton>();
                    if (winButton != null) 
                    {
                        PauseMenu.GameIsComplete = true;
                        Timer.Win();
                        
                    }
                    DoorController doorSwitch = hit.collider.GetComponent<DoorController>();
                    if (doorSwitch != null)
                    {
                        DoorController.doorIsOpening = true;
                        Timer.startRace = true;
                    }
                }         
            }
        }
    }
        //When player collides with the a Game Object with the tag "PickUp" - which i set to Ammo Boxes - it calls the Reload function in the Gun script and destroy the game objects
    void OnTriggerEnter(Collider other) 
    {
        
        if(other.gameObject.CompareTag("PickUp"))
        {
        other.gameObject.SetActive(false);

        Debug.Log("More Ammo");
        
        gun.Reload();
        
        }
    }

    //This function is called from the EnemyDamage script that occurs when a player kills an enemy object
    //this increases the killCount so that when a player reaches a certain number of kills, this functions calls a function from another script (GenerateEnemies) to start another round by spawning new enemy game objects
    public void kill() 
    {
        killCount = killCount - 1;
        Debug.Log("Enemy Killed");
        killScore.text = "10 / "+killCount.ToString();
        if(killCount == 0) 
        {
            Debug.Log("boop");
            // round = round + 1; //increments the round number
            // roundNum.text = "Round: "+round.ToString(); //Displays the round number 
            // generateEnemies.newRound();
        }
    }

}
