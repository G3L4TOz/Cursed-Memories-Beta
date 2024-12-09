using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationShield : MonoBehaviour
{
    private Animator animator;
    public PlayerHP playerHP;

    void Start()
    {
        playerHP = FindObjectOfType<PlayerHP>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!PlayerHP.instance.isContainShield)
        {
            animator.SetBool("isContainShield", false);
        }
        else if (PlayerHP.instance.isContainShield)
        {
            animator.SetBool("isContainShield", true);
        }
    }
}
