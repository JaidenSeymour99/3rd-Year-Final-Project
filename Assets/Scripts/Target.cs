using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    // public PlayerMovement playerMovement;
    public float health = 50f;
    public float timeToDie = 2f;
    public float killPoints = 100f;
    public static int killCount;
    public Animator anim; 

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
        StartCoroutine(ExecuteAfterTime(timeToDie));
        IEnumerator ExecuteAfterTime(float timeToDie)
        {
            // anim.SetBool("IsDead", true);
            
            yield return new WaitForSeconds(timeToDie);
            Destroy(gameObject);
            Player.score += killPoints;
            killCount = killCount + 1;
        }
    }

    //This function is called from the EnemyDamage script that occurs when a player kills an enemy object
    //this increases the killCount so that when a player reaches a certain number of kills, this functions calls a function from another script (GenerateEnemies) to start another round by spawning new enemy game objects
    // public void kill() 
    // {
    //     killCount = killCount + 1;
    //     Debug.Log("Enemy Killed");
    //     // KillText.enemiesKilled.text = "10 / "+killCount.ToString();
    //     if(killCount == 0)  
    //     {            
    //         Debug.Log("boop");
    //         // round = round + 1; //increments the round number
    //         // roundNum.text = "Round: "+round.ToString(); //Displays the round number 
    //         // generateEnemies.newRound();
    //     }
    // }

}
