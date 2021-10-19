
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{

    private void Start()
    {
        StartGame();
        GetComponent();
        CreateEventAnimation();
    }

    private void GetComponent()
    {
        rigidbodyAnimation = playerMovemment.rigi;
    }

    private void StartGame()
    {
        state = PlayerState.Idle;
        _timeinStop = timeInIdle;

    }

    private void CreateEventAnimation()
    {
        #region Jump Force
        AnimationEvent animationEventJump = new AnimationEvent();
        animationEventJump.functionName = "JumpForceAnimation";
        animationEventJump.time = 0.7f;
        animationClipStartJump.AddEvent(animationEventJump);

        AnimationEvent animationEventJumpTwo = new AnimationEvent();
        animationEventJumpTwo.functionName = "JumpForceAnimation";
        animationEventJumpTwo.time = 0.2f;
        animationClipJumpTwo.AddEvent(animationEventJumpTwo);
        #endregion 
    }

    private void AnimationIdle()
    {
        _timeinStop = timeInIdle;
        animatorPlayer.SetBool("Rest", false);
    }

    private void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        _jump = Input.GetButtonDown("Jump") && state != PlayerState.Jumping && !IsJumpingAnimation();
        ParameterAnimation();
        StateMachine();
    }
    private void FixedUpdate()
    {
        VerificationGround();
    }

    private void StateMachine()
    {
        switch (state)
        {
            case PlayerState.Idle:
                if (_jump && isGround)
                {
                    JumpPlayer();
                    _timeinStop = timeInIdle;
                    NewStateMachine(PlayerState.Jumping);
                }
                if (!isGround || IsJumpingAnimation())
                {
                    _timeinStop = timeInIdle;
                    NewStateMachine(PlayerState.Jumping);
                }
                else if (Mathf.Abs(xInput) > 0 || Mathf.Abs(zInput) > 0)
                {
                    _timeinStop = timeInIdle;
                    NewStateMachine(PlayerState.Walking);
                }
                else
                {
                    NewStateMachine(PlayerState.Idle);
                }
                break;
            case PlayerState.Walking:
                if (_jump && isGround)
                {
                    JumpPlayer();
                    NewStateMachine(PlayerState.Jumping);
                }
                if (!isGround || IsJumpingAnimation())
                {
                    NewStateMachine(PlayerState.Jumping);
                }
                else if ((Mathf.Abs(xInput) == 0 && Mathf.Abs(zInput) == 0))
                {
                    NewStateMachine(PlayerState.Idle);
                }
                else if (Input.GetButton("Run"))
                {
                    NewStateMachine(PlayerState.Running);
                }
                else
                {
                    NewStateMachine(PlayerState.Walking);
                }
                break;
            case PlayerState.Running:
                if (_jump && isGround)
                {
                    JumpPlayer();
                    NewStateMachine(PlayerState.Jumping);
                }
                if (!isGround || IsJumpingAnimation())
                {
                    NewStateMachine(PlayerState.Jumping);
                }
                else if ((Mathf.Abs(xInput) == 0 && Mathf.Abs(zInput) == 0))
                {
                    NewStateMachine(PlayerState.Idle);
                }
                else if (Input.GetButton("Run"))
                {
                    NewStateMachine(PlayerState.Running);
                }
                else
                {
                    NewStateMachine(PlayerState.Walking);
                }
                break;
            case PlayerState.Swimming:
                break;
            case PlayerState.Jumping:
                if (!isGround || IsJumpingAnimation())
                {
                    NewStateMachine(PlayerState.Jumping);
                }
                else
                {
                    NewStateMachine(PlayerState.Idle);
                }
                break;
            case PlayerState.Talking:
                break;
        }
    }

    private void JumpPlayer()
    {
        animatorPlayer.SetTrigger("JumpAction");
    }

    //Event de Animation
    private void JumpForceAnimation()
    {
        playerMovemment.JumpPlayer();
    }

    private void NewStateMachine(PlayerState newState)
    {
        switch (newState)
        {
            case PlayerState.Idle:
                InIdle();
                playerMovemment.enabled = false;
                break;
            case PlayerState.Walking:
                playerMovemment.Locomantion(xInput, zInput);
                playerMovemment.speed = 2.5f;
                break;
            case PlayerState.Running:
                playerMovemment.Locomantion(xInput, zInput);
                playerMovemment.speed = 5;
                break;
            case PlayerState.Swimming:
                break;
            case PlayerState.Jumping:
                playerMovemment.speed = 0;
                break;
            case PlayerState.Talking:
                break;
        }

        state = newState;
    }

    private void InIdle()
    {
        _timeinStop -= Time.deltaTime;
        if (_timeinStop <= 0)
        {
            animatorPlayer.SetFloat("IddleNumber", UnityEngine.Random.Range(0, 4));
            animatorPlayer.SetBool("Rest", true);
            _timeinStop = timeInIdle;
            Invoke("AnimationIdle", 2);
        }
    }

    void ParameterAnimation()
    {
        animatorPlayer.SetFloat("Speed", Mathf.Abs((rigidbodyAnimation.velocity.magnitude + rigidbodyAnimation.velocity.y)));
        animatorPlayer.SetBool("IsGround", isGround);
        animatorPlayer.SetFloat("Gravity", rigidbodyAnimation.velocity.y);
    }

    private void VerificationGround()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(rayCastOrigin.position, Vector3.down, out hitInfo, 0.8f))
        {
            if (hitInfo.collider != null)
            {
                print(hitInfo.collider.name);
                isGround = true;
            }
            else
            {
                isGround = false;
            }
        }
        else
        {
            isGround = false;
        }

        if (!isGround)
        {
            isGround = !(rigidbodyAnimation.velocity.y < -0.1);
        }

    }

    private bool IsJumpingAnimation()
    {
        return animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("Jump 0")  || animatorPlayer.GetCurrentAnimatorStateInfo(0).IsName("JUMP00");
    }

    [Header("Player")]
    [SerializeField] PlayerState state;
    [SerializeField] PlayerMovemment playerMovemment;
    [SerializeField] float timeInIdle;

    [SerializeField] private Transform rayCastOrigin;

    [Header("Animation")]
    [SerializeField] Animator animatorPlayer;
    [SerializeField] AnimationClip animationClipIdle;
    [SerializeField] AnimationClip animationClipStartJump, animationClipJumpTwo;

    private Rigidbody rigidbodyAnimation;
    private float xInput;
    private float zInput;
    private float _timeinStop = 10;

    bool _jump;

    [SerializeField] private bool isGround;
}