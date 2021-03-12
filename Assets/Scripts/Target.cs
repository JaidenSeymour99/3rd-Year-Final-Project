using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public Player player;
    public float health = 50f;
    public float killPoints = 100f;   

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
        Debug.Log("hi");
        Player.score += killPoints;
        if (Timer.current == 3 || Timer.current == 4 || Timer.current == 5 || Timer.current == 6)
        {       
            player.kill();
        }
    }
}
