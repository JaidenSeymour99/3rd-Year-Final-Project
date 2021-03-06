using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    // public PlayerMovement playerMovement;
    public float health = 50f;
    public float timeToDie = 2f;
    public float killPoints = 100f;
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
            anim.SetBool("IsDead", true);
            
            yield return new WaitForSeconds(timeToDie);
            Destroy(gameObject);
            Player.score += killPoints;
        }
    }



}
