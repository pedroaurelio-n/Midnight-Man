using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] [Range(0f, 0.5f)] private float minimumSpeed;
    [SerializeField] private List<AnimationClip> animations;

    private bool isMovingRight;
    private bool isMovingUp;
    private bool isMovingDown;

    private string currentState;



    [SerializeField] private GameObject spriteObject;
    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = spriteObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isMovingUp = Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x) && rb.velocity.y > 0;
        isMovingRight = Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y);
        isMovingDown = Mathf.Abs(rb.velocity.y) > Mathf.Abs(rb.velocity.x) && rb.velocity.y < 0;


        if (rb.velocity.x < 0)
        {
            //spriteObject.transform.localScale = new Vector3(-1, 1, 1);
            spriteObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (rb.velocity.x > 0)
        {
            //spriteObject.transform.localScale = new Vector3(1, 1, 1);
            spriteObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        ApplyAnimationDirection();
    }

    private void ApplyAnimationDirection()
    {
        if (isMovingUp)
            ChangeAnimationState(animations[0].name);

        else if (isMovingDown)
            ChangeAnimationState(animations[2].name);

        else if (isMovingRight)
            ChangeAnimationState(animations[1].name);
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
            return;

        animator.Play(newState);
        currentState = newState;
    }
}
