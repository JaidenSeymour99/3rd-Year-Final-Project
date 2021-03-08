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
    public int maxAmmo = 20;
    public int currentAmmo;
    public float reloadTime = 1f;
    public bool isReloading = false;

    public Animator animator;

    public bool isFiring;
    public Text ammoDisplay;

    void Start() 
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }


    // Update is called once per frame
    void Update()
    {   
        //displaying the current guns ammo.
        ammoDisplay.text = "Bullets: "+currentAmmo.ToString();
        //if the player is reloading, return
        if(isReloading)
        {
            return;
        }
        //if the player has no ammo left in their gun, reload, then return.
        if (currentAmmo <= 0) 
        {
            StartCoroutine(Reload());
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }
        if(!PauseMenu.GameIsPaused)
        {
            //if left mouse button is pressed and isFiring = true and currentAmmo is greater than 0 
            if (Input.GetButton("Fire1") && !isFiring && currentAmmo > 0 && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot(); //calls the shoot function

                isFiring = true;
                currentAmmo--;
                isFiring = false;            
            }
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

    //Reload function that plays the reload animation then sets the ammo back to full.
    public IEnumerator Reload() 
    {
        isReloading = true;
        Debug.Log("Reloading");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
        
    }
}
