using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMvt : MonoBehaviour
{
    //trying to get sound to work
    // public AudioSource audioWalk;

    //setting variables
    public float walkSpeed = 5;
    public float sprintSpeed = 8;
    public float JumpPower = 4;
    private float Horizontal;
    private float Vertical;
    Rigidbody rb;
    CapsuleCollider col;
    void Start()
    {
        //making the cursor invis.
        Cursor.lockState = CursorLockMode.Locked;

        //Assigning the components to the variables
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        //the function that makes the player move.
        Move();
        //the sound that happens when the player walks. (does not work)
        // PlayWalkSteps();
        // if (Input.GetKeyDown("escape"))
        //     {
                
        //     }
    }
    void Move(){
        
            //get input values from controllers. If shift is held, this is the sprint speed
            if(Input.GetKey (KeyCode.LeftShift)){
                Horizontal = Input.GetAxis("Horizontal") * sprintSpeed;
                Vertical = Input.GetAxis("Vertical") * sprintSpeed;
                
                //couldnt get both of the walk and sprint methods working without working at the same time.
                // PlayWalkSteps();
                
            }
            // this is the normal walk speed
            else {

                Horizontal = Input.GetAxis("Horizontal") * walkSpeed;
                Vertical = Input.GetAxis("Vertical") * walkSpeed;
                
                //couldnt get both of the walk and sprint methods working without working at the same time.
                //PlayWalkSteps();             
            }
            //move our character
            Horizontal *= Time.deltaTime;
            Vertical *= Time.deltaTime;
            transform.Translate(Horizontal, 0, Vertical);
        


            // this is to check if the player is on the ground and they have pressed space to jump
            if (isGrounded() && Input.GetButtonDown("Jump"))
            {
                //add upward force to the rigid body when we press jump
                rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            } 
        
    }

    private bool isGrounded()
    {
        //Test that we are grounded by drawing an invisible raycast line
        //if this hits a solid object we are grounded
        return Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);
    }


// attempted sound but it didnt work, it only worked in one direction and then when you walked forward it made a noise like all of the sounds were playing at once every frame.
    // private void PlayWalkSteps()
    // {
    //     if (Horizontal >= 0.1f && Horizontal <= walkSpeed + 0.1f)
    //     {
    //         audioWalk.enabled = true;
    //         // audioWalk.loop = true;
    //     }
    //     if (Horizontal < 0.1f)
    //     {
    //         audioWalk.enabled = false;
    //         // audioWalk.loop = false
    //     }
    //     if(Vertical >= 0.1f && Vertical <= walkSpeed + 0.1f)
    //     {
    //         audioWalk.enabled = true;
    //     }

    //     if (Vertical < 0.1f)
    //     {
    //         audioWalk.enabled = false;
    //     }

    // }

    // I was planning on making two different speeds of step but i couldnt get it working.

    // private void PlaySprintSteps()
    // {
    //     if (Horizontal > 0.1f && Horizontal < sprintSpeed + 0.1f)
    //     {
    //         audioWalk.enabled = true;
    //         // audioWalk.loop = true;
    //     }
    //     if (Horizontal < 0.1f)
    //     {
    //         audioWalk.enabled = false;
    //         // audioWalk.loop = false
    //     }
    //     if(Vertical > 0.1f && Vertical < sprintSpeed + 0.1f)
    //     {
    //         audioWalk.enabled = true;
    //     }

    //     if (Vertical < 0.1f)
    //     {
    //         audioWalk.enabled = false;
    //     }
    // }
}

