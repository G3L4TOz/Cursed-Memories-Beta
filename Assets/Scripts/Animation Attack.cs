using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAttack : MonoBehaviour
{
    private Animator animator;
    public PlayerMovement playerMovement;

    public float runCooldownTime = 1.0f;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!playerMovement.isSlash)
        {
            animator.SetBool("isSlash", false);
        }
        else if (playerMovement.isSlash)
        {
            animator.SetBool("isSlash", true);
            runCooldownTime -= Time.deltaTime;
            if (runCooldownTime <= 0)
            {
                playerMovement.isSlash = false;
                runCooldownTime = 0.6f;
            }
        }
    }
}
