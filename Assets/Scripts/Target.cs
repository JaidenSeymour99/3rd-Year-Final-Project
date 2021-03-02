using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public PlayerMovement playerMovement;
    public float health = 50f;

    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f) 
        {
            Die();
        }
    }

    void Die () 
    {
        Destroy(gameObject);
        Debug.Log("Yeet");
        playerMovement.kill();
    }
}
