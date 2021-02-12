using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    //public GameManager gameManager; //Links the gamemanager script
    //public GenerateEnemies generateEnemies; // links the generateEnemies script

    public CharacterController controller; //looks for the CharacterController which a component that puts the usual seperate character movements such as Rigidbody and box collider into one component.
    //Public Character Variables
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f; 

    
    //public Gun gun; //Links the gun script for the Reload() function for ammo

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    Vector3 velocity;
    bool isGrounded;
    
    // public int ammoCount;

    //killScore
    // public int killCount;
    // public Text killScore;

    //roundText
    // private int round = 1;
    // public Text roundNum;

    void Start()
    {
        // killScore.text = "Kills: "+killCount.ToString();
        // roundNum.text = "Round: "+round.ToString();
    }        
    // Update is called once per frame
    void Update()
    {

    //Checks to see if the player is on the ground so the yposition isnt infinitely increasing when a character moves y positions.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

    
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x +transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

    //Sets it so that the player can only jump when isGrounded is true
        if(Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    //Loads the game over screen if character falls off the ship.
        // if (velocity.y < -60f)
        // {
        //     gameManager.EndGame();
        // }
    }

    //When player collides with the a Game Object with the tag "PickUp" - which i set to Ammo Boxes - it calls the Reload function in the Gun script and destroy the game objects
    // void OnTriggerEnter(Collider other) 
    // {
        
    //     if(other.gameObject.CompareTag("PickUp"))
    //     {
    //     other.gameObject.SetActive(false);

    //     Debug.Log("More Ammo");
        
    //     gun.Reload();
        
    //     }
    // }

    //This function is called from the EnemyDamage script that occurs when a player kills an enemy object
    //this increases the killCount so that when a player reaches a certain number of kills, this functions calls a function from another script (GenerateEnemies) to start another round by spawning new enemy game objects
    // public void kill() 
    // {
    //     killCount = killCount + 1;
    //     Debug.Log("Enemy Killed");
    //     killScore.text = "Kills: "+killCount.ToString();
    //     if(killCount == 5) 
    //     {
    //         Debug.Log("boop");
    //         round = round + 1; //increments the round number
    //         roundNum.text = "Round: "+round.ToString(); //Displays the round number 
    //         generateEnemies.newRound();
    //     }

    //     if(killCount == 10) 
    //     {
    //         Debug.Log("boop");
    //         round = round + 1;
    //         roundNum.text = "Round: "+round.ToString();
    //         generateEnemies.newRound3();
    //     }

    //     if(killCount == 14) 
    //     {
    //         Debug.Log("boop");
    //         round = round + 1;
    //         roundNum.text = "Round: "+round.ToString();
    //         generateEnemies.newRound4();
    //     }

    //     if(killCount == 18) 
    //     {
    //        gameManager.WinGame(); //calls a function in the GenerateEnemies script that loads the WinScene Screen.
    //     }

    // }

}
