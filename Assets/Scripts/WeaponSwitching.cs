using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    public int selectedWeapon = 0;
    private int previousSelectedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {

        previousSelectedWeapon = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedWeapon >= transform.childCount -1)
            {
                selectedWeapon = 0;
            }
            else 
            {
                selectedWeapon++;
            }
        }
        // if you scroll down it changes to the weapon that is below it (above it in the unity hirearchy),
        // e.g. the gun 1 has an index of 0 and gun 2 has index of 1 you scroll down from gun 1 it sets the index to 0.
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else 
            {
                selectedWeapon--;
            }
        }

        //if 1 is pressed it switches to the first weapon
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        //if 2 is pressed it switches to the second weapon
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }

        //making sure the weapon selected is showing
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0; 
        foreach(Transform weapon in transform)
        {
            if(i == selectedWeapon) 
            {
                weapon.gameObject.SetActive(true);
            }
            else 
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
