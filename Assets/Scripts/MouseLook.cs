using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private GameObject player;
    private float minClamp = -45;
    private float maxClamp = 45;
    [HideInInspector]
    public  Vector2 rotation;
    private Vector2 currentLookRotation;
    private Vector2 rotationV = new Vector2(0,0);
    public float lookSensitivity = 2;
    public float lookSmoothDamp = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        //get the game Player object
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {   
        //if the game is playing you can look around.
        // if(!PauseMenu.GameIsPaused && !PauseMenu.GameIsOver)
        // {
        //Input from the mouse
        rotation.y += Input.GetAxis("Mouse Y") * lookSensitivity;

        //clamping the values
        rotation.y = Mathf.Clamp(rotation.y, minClamp, maxClamp);
        //rotating the character based on the x pos
        player.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * lookSensitivity);
        //smoothing for the y axis rotation
        currentLookRotation.y = Mathf.SmoothDamp(currentLookRotation.y, rotation.y, ref rotationV.y, lookSmoothDamp);

        //update the camera x rotation
        transform.localEulerAngles = new Vector3(-currentLookRotation.y, 0, 0);
        // }
    }
}

