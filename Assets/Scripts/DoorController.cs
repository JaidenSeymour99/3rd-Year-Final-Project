using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public GameObject Door;
    public static bool doorIsOpening;

    void Update()
    {
        //open door if true
        if (doorIsOpening == true) 
        {
            Door.transform.Translate (Vector3.up * Time.deltaTime * 5);
        }
        //when the y position of the door is bigger than 2.5 we want the door to stop.
        if (Door.transform.position.y > 11f) 
        {
            doorIsOpening = false;
        }
    }
    
}
