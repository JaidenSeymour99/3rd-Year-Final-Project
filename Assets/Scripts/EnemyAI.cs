using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //links the navMesh component 
   public NavMeshAgent agent;
   
   public Transform player;
   //public float ttl = 0.5f;

   public Transform shootPoint;

   public AudioSource laserSound;

    //so that the enemy and navmesh component knows what the ground layer and player layer are.
   public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States 
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public void Awake() 
    {
        player = GameObject.Find("FirstPersonPlayer").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling(); //calls Patroling if player enemy doesnt see the player in sightrange and attackrange
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();  //calls ChasePlayer if the player is in sight but not in attackrange
        if (playerInSightRange && playerInAttackRange) AttackPlayer();  //calls AttackPlayer if the plaer is in sight and attackrange
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //WalkPoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
    }

    private void AttackPlayer() 
    {
        //stops movement
        agent.SetDestination(transform.position);

        //makes object face player
        transform.LookAt(player);

        if(!alreadyAttacked) 
        {
            //spawns a projectile where shootPoint is set 
            Rigidbody rb = Instantiate(projectile, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            
            //takes the rigidboy components of the projectile and modifies them. adds force to the rigidbody and shoots it toward the player
            rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
            rb.AddForce(transform.up * -5f, ForceMode.Impulse);

            //destroys projectile 1s after instantiation 
            Destroy(rb.gameObject, 1f);

            laserSound.Play();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void SearchWalkPoint() 
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, - transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    
}
