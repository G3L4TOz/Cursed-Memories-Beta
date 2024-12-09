using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHealing : MonoBehaviour
{
    private Animator animator;
    public PlayerHP playerHP;
    public float runCooldownTime = 3.0f;

    void Start()
    {
        playerHP = FindObjectOfType<PlayerHP>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!playerHP.isHeal)
        {
            animator.SetBool("isHeal", false);
        }
        else if (playerHP.isHeal)
        {
            animator.SetBool("isHeal", true);
            runCooldownTime -= Time.deltaTime;
            if (runCooldownTime <= 0)
            {
                playerHP.isHeal = false;
                runCooldownTime = 3.0f;
            }
        }
    }
}
