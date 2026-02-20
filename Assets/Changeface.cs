using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Changeface : MonoBehaviour
{
    private Vector2 lastPosition;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {


        animator.SetBool("IsMoveing", false);
        Vector2 currentPosition = transform.position;
        Vector2 movement = currentPosition - lastPosition;

        if (true)
        {
            Vector2 direction = movement.normalized;
        }

        lastPosition = currentPosition;
        float x = movement.x;
        float y = movement.y;
        if ((!(x == 0)) || (!(y == 0)))
        {
            Debug.Log("x=" + x + "y=" + y);
            animator.SetBool("IsMoveing", true);
            if (x > 0)
            {
                animator.SetFloat("MoveX", 1);
            }
            if (x < 0)
            {
                animator.SetFloat("MoveX", -1);
            }
            if (x == 0)
            {
                animator.SetFloat("MoveX", 0);
            }
            if (y > 0)
            {
                animator.SetFloat("MoveY", 1);
            }
            if (y < 0)
            {
                animator.SetFloat("MoveY", -1);
            }
            if (y == 0)
            {
                animator.SetFloat("MoveY", 0);
            }
        }

    }
}
