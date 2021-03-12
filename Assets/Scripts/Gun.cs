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
    //public GameObject impactEffect;

    
    private float nextTimeToFire = 0f;

    //sound - calls for the shoot audiosouce
    public AudioSource fireSound, reloadSound;

    //Ammo
    public int clipsLeft;
    //public int bulletsShot;
    public int maxAmmo = 20;
    public int currentAmmo;
    public float reloadTime = 1f;
    public bool isReloading = false;

    public Animator animator;

    public bool isFiring;
    public Text ammoDisplay;

    //particle
    public ParticleSystem muzzle;
    public GameObject impactEffect;

    void Start() 
    {
        clipsLeft = 1;
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
        if(!PauseMenu.GameIsComplete && !PauseMenu.GameIsOver && !PauseMenu.GameIsPaused)
        {
            //displaying the current guns ammo.
            ammoDisplay.text = ""+currentAmmo.ToString()+"/ "+clipsLeft.ToString()+" Clip";
            //if the player is reloading, return
            if(isReloading)
            {
                return;
            }
            //if the player has no ammo left in their gun, reload, then return.
            if (clipsLeft > 0) 
            {
                if (currentAmmo <= 0) 
                {
                    StartCoroutine(Reload());
                    return;
                }
                if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
                {
                    StartCoroutine(Reload());
                }
            }

            
            //if left mouse button is pressed and isFiring = true and currentAmmo is greater than 0 
            if (Input.GetButton("Fire1") && !isFiring && currentAmmo > 0 && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot(); //calls the shoot function

                //plays the shooting sound
                

                isFiring = true;
                currentAmmo--;
                isFiring = false;            
            }  
        }

    }

    void Shoot() 
    {
    
        fireSound.Play();
        muzzle.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            
            //muzzleFlash.Play();
            // Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            //instantatiates the impactEffect object at the point of ray impact then destroys the object after 2 seconds
            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 2f); 
        }
    }

    //Reload function that plays the reload animation then sets the ammo back to full.
    public IEnumerator Reload() 
    {
        //plays reloading sound
        reloadSound.Play();

        isReloading = true;
        Debug.Log("Reloading");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        clipsLeft -= 1;
        isReloading = false;
        
    }

    //called from the player script and runs when the player picks up an ammo box - this then adds 5 ammo to your clip
    public void pickUpAmmo() 
    {
        clipsLeft += 1;
    }
}
