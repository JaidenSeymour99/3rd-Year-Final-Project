using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;

    //Ammo
    public int maxAmmo = 5;
    public int currentAmmo;
    public bool isFiring;
    public Text ammoDisplay;

    void Start() 
    {
        currentAmmo = maxAmmo;
    }


    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = "Bullets: "+currentAmmo.ToString(); //Displays the players current ammo.
        if (Input.GetButtonDown("Fire1") && !isFiring && currentAmmo > 0)
        {

            Shoot(); //calls the shoot function

            //ammos
            isFiring = true;
            currentAmmo--;
            isFiring = false;            
        }  
    }

    void Shoot() 
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    public void Reload() 
    {
        currentAmmo += maxAmmo;
        Debug.Log("Reloading");
    }
}
