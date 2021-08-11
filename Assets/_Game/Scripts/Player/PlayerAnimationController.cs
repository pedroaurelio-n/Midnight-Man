using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float delay;
    [SerializeField] [Range(0f, 0.5f)] private float minimumSpeed;
    [SerializeField] private List<AnimationClip> OFF_Animations;
    [SerializeField] private List<AnimationClip> ON_Animations;

    private List<AnimationClip> animations;

    private int animationDirection;

    private Vector2 movementDirection;

    private bool isMovingLeft;
    private bool isMovingRight;
    private bool isMovingUp;
    private bool isMovingDown;
    private bool isMovingDiagonalUpLeft;
    private bool isMovingDiagonalUpRight;
    private bool isMovingDiagonalDownLeft;
    private bool isMovingDiagonalDownRight;

    private string currentState;
    private bool hasCandle;


    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private GameObject spriteObject;
    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        hasCandle = player.hasCandle;
        animationDirection = 1;

        animations = new List<AnimationClip>();

        animator = spriteObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (hasCandle != player.hasCandle)
            hasCandle = player.hasCandle;

        movementDirection = playerMovement.GetMovementDirection();

        isMovingLeft = movementDirection.x < 0 && movementDirection.y == 0;
        isMovingRight = movementDirection.x > 0 && movementDirection.y == 0;
        isMovingUp = movementDirection.x == 0 && movementDirection.y > 0;
        isMovingDown = movementDirection.x == 0 && movementDirection.y < 0;

        isMovingDiagonalUpLeft = movementDirection.x < 0 && movementDirection.y > 0;
        isMovingDiagonalUpRight = movementDirection.x > 0 && movementDirection.y > 0;
        isMovingDiagonalDownLeft = movementDirection.x < 0 && movementDirection.y < 0;
        isMovingDiagonalDownRight = movementDirection.x > 0 && movementDirection.y < 0;

        if (hasCandle && animations != ON_Animations)
        {
            animations = ON_Animations;
        }

        else if (!hasCandle && animations != OFF_Animations)
        {
            animations = OFF_Animations;
        }


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


        if (isMovingUp)
            animationDirection = 1;

        if (isMovingDiagonalUpRight)
            animationDirection = 2;

        if (isMovingRight)
            animationDirection = 3;

        if (isMovingDiagonalDownRight)
            animationDirection = 4;

        if (isMovingDown)
            animationDirection = 5;

        if (isMovingDiagonalDownLeft)
            animationDirection = 6;

        if (isMovingLeft)
            animationDirection = 7;

        if (isMovingDiagonalUpLeft)
            animationDirection = 8;


        ApplyAnimationDirection();
    }

    private void ApplyAnimationDirection()
    {
        switch (animationDirection)
        {
            //case 1:
            //    if (rb.velocity.magnitude <= minimumSpeed)
            //        ChangeAnimationState("arrow_i_Up");

            //    else
            //        ChangeAnimationState("arrow_m_Up");

            //    break;

            //case 2:
            //    if (rb.velocity.magnitude <= minimumSpeed)
            //        ChangeAnimationState("arrow_id_UpRight");

            //    else
            //        ChangeAnimationState("arrow_d_UpSide");

            //    break;

            //case 3:
            //    if (rb.velocity.magnitude <= minimumSpeed)
            //        ChangeAnimationState("arrow_i_Right");

            //    else
            //        ChangeAnimationState("arrow_m_Side");

            //    break;

            //case 4:
            //    if (rb.velocity.magnitude <= minimumSpeed)
            //        ChangeAnimationState("arrow_id_DownRight");

            //    else
            //        ChangeAnimationState("arrow_d_DownSide");

            //    break;

            //case 5:
            //    if (rb.velocity.magnitude <= minimumSpeed)
            //        ChangeAnimationState("arrow_i_Down");

            //    else
            //        ChangeAnimationState("arrow_m_Down");

            //    break;

            //case 6:
            //    if (rb.velocity.magnitude <= minimumSpeed)
            //        ChangeAnimationState("arrow_id_DownRight");

            //    else
            //        ChangeAnimationState("arrow_d_DownSide");

            //    break;

            //case 7:
            //    if (rb.velocity.magnitude <= minimumSpeed)
            //        ChangeAnimationState("arrow_i_Right");

            //    else
            //        ChangeAnimationState("arrow_m_Side");

            //    break;

            //case 8:
            //    if (rb.velocity.magnitude <= minimumSpeed)
            //        ChangeAnimationState("arrow_id_UpRight");

            //    else
            //        ChangeAnimationState("arrow_d_UpSide");

            //    break;

            case 1:
                if (rb.velocity.magnitude <= minimumSpeed)
                    ChangeAnimationState(animations[1].name);

                else
                    ChangeAnimationState(animations[0].name);

                break;

            case 2:
                if (rb.velocity.magnitude <= minimumSpeed)
                    ChangeAnimationState(animations[3].name);

                else
                    ChangeAnimationState(animations[2].name);

                break;

            case 3:
                if (rb.velocity.magnitude <= minimumSpeed)
                    ChangeAnimationState(animations[3].name);

                else
                    ChangeAnimationState(animations[2].name);

                break;

            case 4:
                if (rb.velocity.magnitude <= minimumSpeed)
                    ChangeAnimationState(animations[3].name);

                else
                    ChangeAnimationState(animations[2].name);

                break;

            case 5:
                if (rb.velocity.magnitude <= minimumSpeed)
                    ChangeAnimationState(animations[5].name);

                else
                    ChangeAnimationState(animations[4].name);

                break;

            case 6:
                if (rb.velocity.magnitude <= minimumSpeed)
                    ChangeAnimationState(animations[3].name);

                else
                    ChangeAnimationState(animations[2].name);

                break;

            case 7:
                if (rb.velocity.magnitude <= minimumSpeed)
                    ChangeAnimationState(animations[3].name);

                else
                    ChangeAnimationState(animations[2].name);

                break;

            case 8:
                if (rb.velocity.magnitude <= minimumSpeed)
                    ChangeAnimationState(animations[3].name);

                else
                    ChangeAnimationState(animations[2].name);

                break;

            default: ChangeAnimationState("Test");
                break;
        }
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
            return;

        animator.Play(newState);
        currentState = newState;
    }

    public void ReceiveAnimationDirection(int value)
    {
        animationDirection = value;
    }
}
