using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 20;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float gravity = -20;
    [SerializeField] float maxSpeed = 20;
    [SerializeField] float laneDistance = 2.5f;
    [SerializeField] TMP_Text scoreText = null;
    [SerializeField] TMP_Text highScoreText = null;
    [SerializeField] ScoreCounter ScoreCounter = null;


    int laneToSwitch = 1;
    float highScore = 0;

    CharacterController controller;
    Vector3 movePlayer;
    Animator animator;




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
        highScore = PlayerPrefs.GetInt("High Score", (int)highScore);
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
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        FindObjectOfType<AudioManager>().Play("Game Over");
        float score = ScoreCounter.GetScore();
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("High Score", (int)highScore);
        }
        scoreText.text = $"Score: {(int)score}";
        highScoreText.text = $"Highscore: {(int)highScore}";
        PlayerManager.isGameOver = true;
    }
}
