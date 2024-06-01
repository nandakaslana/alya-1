using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolHunt : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
    public float chaseSpeed; // Speed when chasing the player
    public float chaseRange; // Distance to start chasing the player
    public Collider2D enemyArea; // Collider representing the enemy's area

    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;
    private Transform player; // Reference to the player object

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = 0;
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Get the player object (assuming you have a player tagged "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is within the chase range
        if (Vector2.Distance(transform.position, player.position) <= chaseRange)
        {
            // Chase the player
            ChasePlayer();
        }
        else
        {
            // Patrol if player is outside chase range
            Patrol();
        }

        // Flip the sprite based on movement direction
        FlipSprite();
    }

    // Patrol behavior
    private void Patrol()
    {
        // Check if we've reached the target point
        if (transform.position == patrolPoints[targetPoint].position)
        {
            increaseTargetInt();
        }

        // Move towards the target point
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
    }

    // Chase behavior
    private void ChasePlayer()
    {
        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }

    // Flip the sprite based on movement direction
    private void FlipSprite()
    {
        // Check if the next target point is to the right
        if (patrolPoints[targetPoint].position.x > transform.position.x || (player != null && player.position.x > transform.position.x))
        {
            // Flip the sprite to face right
            spriteRenderer.flipX = false;
        }
        else if (patrolPoints[targetPoint].position.x < transform.position.x || (player != null && player.position.x < transform.position.x))
        {
            // Flip the sprite to face left
            spriteRenderer.flipX = true;
        }
    }

    // Increment the target point and loop back to 0 if needed
    private void increaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }

    // Check if the player has entered or exited the enemy area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Player entered the enemy area
            // Do something if needed (e.g., play sound effect, activate chase)
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Player exited the enemy area
            // Do something if needed (e.g., stop chase, return to patrol)
        }
    }
}