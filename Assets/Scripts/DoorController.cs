using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public GameObject[] door;
    public GameObject[] button;
    public static bool doorIsOpening;

    public Animator anim1;
    //for adding multiple doors to  open at the same time.
    // public Animator anim2;


    void start()
    {
        anim1 = GetComponent<Animator>();
        // anim2 = GetComponent<Animator>();

        doorIsOpening = false;
    }

    void Update()
    {

        //open door if true
        if (doorIsOpening == true) 
        {
            anim1.SetBool("doorOpen", true);
            // anim2.SetBool("doorOpen", true);        
            doorIsOpening = false;
        }
    }
    
}
