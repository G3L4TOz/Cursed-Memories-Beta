using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float chaseSpeed = 2.5f;
    public float patrolRange = 5f;
    public float chaseRange = 3f;
    public float attackRange = 1.5f;
    public int damage = 1;
    public float attackCooldown = 3f;

    private bool isStunned = false;
    private float stunDuration = 3f;

    private Vector2 startingPosition;
    private Vector2 patrolDestination;
    private Transform player;

    private bool canAttack = true;
    private Rigidbody2D rb;

    private Animator animator;
    private Vector3 lastPosition;
    
    public float hearingDistance = 5f;
    public float maxVolume = 1f; 
    public AudioSource audioSource;
    public AudioClip ghostAttackSound; 
    private AudioSource audioSource2;
   
    void Start()
    {
        startingPosition = transform.position;
        animator = GetComponent<Animator>();
        SetNewPatrolDestination();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.volume = 0f;
        audioSource.loop = true;
        audioSource.Play(); 
        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource2.volume = 0.5f;
        audioSource2.clip = ghostAttackSound;
        audioSource2.playOnAwake = false;
    }

    void Update()
    {
        UpdateAnimation();
        AdjustGhostVolume();

        if (isStunned)
        {
            return; 
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        if (distanceToPlayer <= attackRange && canAttack)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    public void Stun()
    {
        if (!isStunned)
        {
            Debug.Log("Stun function called!");
            isStunned = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine(RecoverFromStun());
        }
    }

    private IEnumerator RecoverFromStun()
    {
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
        Debug.Log("Ghost has recovered from stun!");
    }

    void Patrol()
    {
        if (isStunned) return;

        Vector2 direction = (patrolDestination - (Vector2)transform.position).normalized;

        RaycastHit2D hitForward = Physics2D.Raycast(transform.position, direction, 0.5f, LayerMask.GetMask("Obstacle"));
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.Perpendicular(direction), 0.5f, LayerMask.GetMask("Obstacle"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, -Vector2.Perpendicular(direction), 0.5f, LayerMask.GetMask("Obstacle"));

        if (hitForward.collider == null)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolDestination, moveSpeed * Time.deltaTime);
        }
        else if (hitLeft.collider == null)
        {
            patrolDestination = (Vector2)transform.position + Vector2.Perpendicular(direction) * patrolRange;
        }
        else if (hitRight.collider == null)
        {
            patrolDestination = (Vector2)transform.position - Vector2.Perpendicular(direction) * patrolRange;
        }
        else
        {
            SetNewPatrolDestination();
        }

        if (Vector2.Distance(transform.position, patrolDestination) <= 0.1f)
        {
            SetNewPatrolDestination();
        }
    }

    void SetNewPatrolDestination()
    {
        float patrolX = startingPosition.x + Random.Range(-patrolRange, patrolRange);
        float patrolY = startingPosition.y + Random.Range(-patrolRange, patrolRange);
        patrolDestination = new Vector2(patrolX, patrolY);
    }

    void UpdateAnimation()
    {
        animator.SetBool("isMoveFront", false);
        animator.SetBool("isMoveBack", false);
        animator.SetBool("isMoveLeft", false);
        animator.SetBool("isMoveRight", false);

        float deltaY = transform.position.y - lastPosition.y;
        float deltaX = transform.position.x - lastPosition.x;

        if (Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
        {
            if (deltaY > 0)
            {
                animator.SetBool("isMoveBack", true);
            }
            else if (deltaY < 0)
            {
                animator.SetBool("isMoveFront", true);
            }
        }
        else
        {
            if (deltaX > 0)
            {
                animator.SetBool("isMoveRight", true);
            }
            else if (deltaX < 0)
            {
                animator.SetBool("isMoveLeft", true);
            }
        }
        lastPosition = transform.position;
    }

    void ChasePlayer()
    {
        if (!isStunned)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.1f, LayerMask.GetMask("Obstacle"));
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (hit.collider == null)
            {
                if (distanceToPlayer <= attackRange)
                {
                    rb.velocity = Vector2.zero;
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
                }
            }
            else
            {
                SetNewPatrolDestination();
            }
        }
    }

    IEnumerator AttackPlayer()
    {
        canAttack = false;

        PlayerHP playerHP = player.GetComponent<PlayerHP>();
        playerHP.TakeDamage(damage);
        audioSource2.Play();
        Stun();

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true; 
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void AdjustGhostVolume()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < hearingDistance)
        {
            float volume = Mathf.Lerp(0f, maxVolume, 1 - (distance / hearingDistance));
            audioSource.volume = volume;
        }
        else
        {
            audioSource.volume = 0f;
        }
    }
}