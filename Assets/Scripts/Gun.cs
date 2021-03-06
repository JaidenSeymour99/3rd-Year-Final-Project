using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 4f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

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
        if (Input.GetButton("Fire1") && !isFiring && currentAmmo > 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
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
            muzzleFlash.Play();

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 2f); 
        }
    }

    public void Reload() 
    {
        currentAmmo += maxAmmo;
        Debug.Log("Reloading");
    }
}
