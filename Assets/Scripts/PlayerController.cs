using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 20;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float gravity = -20;
    [SerializeField] float maxSpeed = 20;


    CharacterController controller;
    Vector3 movePlayer;
    Animator animator;

    private int laneToSwitch = 1;
    public float laneDistance = 2.5f;

    private void Awake()
    {
        PlayerManager.isGameStarted = false;
    }

    public Animator GetAnimator()
    {
        return animator;
    }


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!PlayerManager.isGameStarted) return;
        animator.SetBool("Start Game", true);

        if (speed < maxSpeed)
        {

            speed += 0.1f * Time.deltaTime;
        }

        PlayerMovement();
    }

    private void PlayerMovement()
    {
        movePlayer.z = speed;

        if (controller.isGrounded && (Input.GetKeyDown(KeyCode.Space) || SwipeManager.swipeUp))
        {
            animator.SetTrigger("Jump");
            JumpPlayer();
        }
        else
        {
            movePlayer.y += gravity * Time.deltaTime;
        }
        HorizontalMovement();
        controller.Move(movePlayer * Time.deltaTime);
    }

    private void HorizontalMovement()
    {
        if (Input.GetKeyDown(KeyCode.A) || SwipeManager.swipeLeft)
        {
            laneToSwitch++;
            if (laneToSwitch == 3)
                laneToSwitch = 2;
        }
        if (Input.GetKeyDown(KeyCode.D) || SwipeManager.swipeRight)
        {
            laneToSwitch--;
            if (laneToSwitch == -1)
                laneToSwitch = 0;
        }
        Vector3 targetLane = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (laneToSwitch == 0)
        {
            targetLane += Vector3.left * laneDistance;
        }
        else if (laneToSwitch == 2)
        {
            targetLane += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetLane, 70 * Time.deltaTime);
        controller.center = controller.center;
    }

    private void JumpPlayer()
    {
        movePlayer.y = jumpForce;
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            FindObjectOfType<AudioManager>().Play("Game Over");
            PlayerManager.isGameOver = true;
        }
    }
}
