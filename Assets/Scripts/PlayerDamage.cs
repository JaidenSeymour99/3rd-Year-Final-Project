using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDamage : MonoBehaviour
{
    //Use this to reference the text in the canvas
    public Text healthPanel;
    //Sets default health to 100
    public int health = 100;
    private void Start()
    {
        //Sets the health text at the start, we pass 0 as we don’t want to remove health.
        ApplyDamage(0);
    }
    void ApplyDamage(int damage)
    {
        //Checks we has attached a health panel and out health is greater than 0
        if (healthPanel != null && health > 0)
        {
            
            //Stores the current health and subtracts the damage value
            health = health - damage;
            //Debug.Log(health);
            //Sets the text on our panel.
            healthPanel.text = string.Format("Health: {0}", health);
            //changed for formatting 
            //health.ToString();
        }
    }
}