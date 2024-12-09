using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float runSpeed = 3f;
    public float stamina = 100;
    public float maxStamina = 100;
    public float staminaDrainRate = 10;
    public float staminaRechargeRate = 10;
    public float CurrentStamina => currentStamina;
    private float currentStamina;
    public float runCooldownTime = 3f;
    private bool isRunning = false;

    public PlayerHP playerHP;
    private Rigidbody2D rb;
    private Animator animator;
    public float killRange = 2f;
    public Inventory inventory;
    public float attackRange = 1f;

    public AudioClip walkSound; 
    private AudioSource audioSourceWalk;

    public bool isDialouge = false;
    public bool isSlash = false;

    Vector2 movement;

    void Start()
    {
        audioSourceWalk = gameObject.AddComponent<AudioSource>();
        audioSourceWalk.clip = walkSound;
        audioSourceWalk.volume = 0.1f;
        audioSourceWalk.playOnAwake = false;
        audioSourceWalk.loop = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentStamina = maxStamina;
        playerHP = FindObjectOfType<PlayerHP>();
        if (Inventory.instance == null)
        {
            Debug.LogError("No Inventory instance found. Please ensure Inventory is set up correctly.");
        }
    }

    void Update()
    {
        if (!playerHP.isDead)
        {
            if (!isDialouge)
            {
                Move();
            }
            if (isDialouge)
            {
                Freeze();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (HasSword())
                {
                    Attack();
                }
                else if (HasCrucifix())
                {
                    CrucifixProtect();
                }
                else if (HasHolyWater())
                {
                    HolyWaterHeal();
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                var Inventory = GetComponent<Inventory>();
                if (HasSword() || HasCrucifix() || HasHolyWater())
                {
                    Inventory.instance.DropItem();
                    Debug.Log("Item Drop");
                }
            }
        }
        HandleStamina();
        UpdateAnimatorSpeed();
    }

    public void Freeze()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("isWalkRight", false);
        animator.SetBool("isWalkLeft", false);
        animator.SetBool("isWalkBack", false);
        animator.SetBool("isWalkFront", false);
        isRunning = false;
        audioSourceWalk.Stop();
    }

    public void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX > 0)
        {
            moveY = 0;
            animator.SetBool("isWalkRight", true);
            animator.SetBool("isWalkLeft", false);
            animator.SetBool("isWalkBack", false);
            animator.SetBool("isWalkFront", false);
            if (!audioSourceWalk.isPlaying)
            {
                audioSourceWalk.Play();
            }
        }
        else if (moveX < 0)
        {
            moveY = 0;
            animator.SetBool("isWalkRight", false);
            animator.SetBool("isWalkLeft", true);
            animator.SetBool("isWalkBack", false);
            animator.SetBool("isWalkFront", false);
            if (!audioSourceWalk.isPlaying)
            {
                audioSourceWalk.Play();
            }
        }
        else if (moveY > 0)
        {
            moveX = 0;
            animator.SetBool("isWalkRight", false);
            animator.SetBool("isWalkLeft", false);
            animator.SetBool("isWalkBack", true);
            animator.SetBool("isWalkFront", false);
            if (!audioSourceWalk.isPlaying)
            {
                audioSourceWalk.Play();
            }
        }
        else if (moveY < 0)
        {
            moveX = 0;
            animator.SetBool("isWalkRight", false);
            animator.SetBool("isWalkLeft", false);
            animator.SetBool("isWalkBack", false);
            animator.SetBool("isWalkFront", true);
            if (!audioSourceWalk.isPlaying)
            {
                audioSourceWalk.Play();
            }
        }
        else
        {
            animator.SetBool("isWalkRight", false);
            animator.SetBool("isWalkLeft", false);
            animator.SetBool("isWalkBack", false);
            animator.SetBool("isWalkFront", false);
            audioSourceWalk.Stop();
        }

        movement = new Vector2(moveX, moveY).normalized;

        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            rb.velocity = movement * runSpeed;
            isRunning = true;
        }
        else
        {
            rb.velocity = movement * moveSpeed;
            isRunning = false;
        }
    }

    void HandleStamina()
    {
        if (isRunning)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }
        else if (!isRunning && currentStamina >= 5)
        {
            currentStamina += staminaRechargeRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }
        else if (!isRunning && currentStamina < 5)
        {   
            runCooldownTime -= Time.deltaTime;
            if (runCooldownTime <= 0)
            {
                currentStamina = 5;
                runCooldownTime = 3;
            }
        }   
    }

    void PickUpItem(Item item)
    {
        if (Inventory.instance != null)
        {
            Inventory.instance.AddItem(item.itemType, item.itemSprite);
            Destroy(item.gameObject);
        }
    }
    void Attack()
    {
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");

        foreach (GameObject ghost in ghosts)
        {
            float distanceToGhost = Vector2.Distance(transform.position, ghost.transform.position);

            if (distanceToGhost <= killRange)
            {
                isSlash = true;
                Inventory.instance.UseItem();
                Destroy(ghost);
                Debug.Log("Ghost killed!");
                return;
            }
        }

        GameObject[] undeadGhosts = GameObject.FindGameObjectsWithTag("UndeadGhost");

        foreach (GameObject undeadghost in undeadGhosts)
        {
            float distanceToGhost = Vector2.Distance(transform.position, undeadghost.transform.position);

            if (distanceToGhost <= killRange)
            {
                isSlash = true;
                Inventory.instance.UseItem();
                Debug.Log("Attempting to stun ghost...");
                undeadghost.GetComponent<GhostAI>().Stun();
                Debug.Log("Ghost stunned!");
                return;
            }
        }
    }

    void CrucifixProtect()
    {
        playerHP.CrucifixProtect();
        if (playerHP.isContainShield)
        {
            Inventory.instance.UseItem();
        }
    }

    void HolyWaterHeal()
    {
        playerHP.Heal(1);
        if (playerHP.isHeal)
        {
            currentStamina = 100;
            Inventory.instance.UseItem();
        }
    }

    bool HasSword()
    {
        ItemType? currentItem = Inventory.instance.GetCurrentItem();
        return currentItem.HasValue && currentItem.Value == ItemType.Sword;
    }

    bool HasCrucifix()
    {
        ItemType? currentItem = Inventory.instance.GetCurrentItem();
        return currentItem.HasValue && currentItem.Value == ItemType.Crucifix;
    }

    bool HasHolyWater()
    {
        ItemType? currentItem = Inventory.instance.GetCurrentItem();
        return currentItem.HasValue && currentItem.Value == ItemType.HolyWater;
    }

    void UpdateAnimatorSpeed()
    {
        if (isRunning)
        {
            animator.speed = 1.5f;
            audioSourceWalk.pitch  = 1.5f;
        }
        else
        {
            animator.speed = 1.0f;
            audioSourceWalk.pitch = 1.0f;
        }
    }
}