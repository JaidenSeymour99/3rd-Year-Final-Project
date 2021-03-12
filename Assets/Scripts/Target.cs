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
        //if the target is out of health run Die()
        health -= amount;
        if (health <= 0f) 
        {
            Die();
        }
    }

    //destroys the object and adds score, if the scene is level 3, 4 ,5 or 6 it will also take away 1 from the kill count at the top left of the hud.
    void Die () 
    {
        Destroy(gameObject);
        //Debug.Log("test");
        Player.score += killPoints;
        if (Timer.current == 3 || Timer.current == 4 || Timer.current == 5 || Timer.current == 6)
        {       
            player.kill();
        }
    }
}
