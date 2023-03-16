using Spine.Unity;
using UnityEngine;

/// <summary>
/// Quản lý Player
/// </summary>
[RequireComponent(typeof(Controller2D))]
public class PlayerManager : MonoBehaviour
{
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    private float moveSpeed = 6;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    private float timeToWallUnstick;

    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;

    private Controller2D controller;

    //  private Vector2 DirectionInput;
    private bool wallSliding;

    private float wallDirX;

    public static PlayerManager intance;
    private bool isBullet;
    private PlayerHealth levelPlayer;
    private Vector2 directionalInput;

    public PlayerHealth LevelPlayer
    {
        get
        {
            return levelPlayer;
        }
        set
        {
            levelPlayer = value;
        }
    }

    private float directionX;
    [SerializeField] private SkeletonAnimation AnimationData;
    private string currentAnimation = "";

    public bool IsBuiiet
    {
        get
        {
            return isBullet;
        }
        set
        {
            isBullet = value;
        }
    }

    private void Awake()
    {
        if (intance == null)
        {
            intance = this;
        }
        else return;
    }

    private void Start()
    {
        LevelPlayer = PlayerHealth.LOWER;
        IsBuiiet = false;
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
       // print("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);
    }

    public void SetDirectionInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDow()
    {
        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
        }
        if (controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                if (directionalInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
                {
                    velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                }
            }
            else
                velocity.y = maxJumpVelocity;
        }
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    private void Update()
    {
      CalculateVelocity();
        HandleWallSling();

       directionX = controller.collisions.faceDir;

        if (levelPlayer == PlayerHealth.HIGHT)
        {
            transform.localScale = new Vector3(1.4f * directionX, 1.4f, 1.4f);
        }
        else
        {
           transform.localScale = new Vector3(1f * directionX, 1f, 1f);
        }
;

        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
            }
        }
    }

    private void HandleWallSling()
    {
        int wallDirX = (controller.collisions.left) ? -1 : 1;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirX && directionalInput.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }
    }
    private void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
         velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
         velocity.y += gravity * Time.deltaTime;
    }

    private bool IsGround()
    {
        if (velocity.y < minJumpVelocity)
            return true;
        else return false;
    }

    public bool CanAttack()
    {
        return velocity.x == 0 && IsGround() && isBullet;
    }

    public void SetAnination(string name, bool loop = true)
    {
        if (name == this.currentAnimation)
        {
            return;
        }
        AnimationData.state.SetAnimation(0, name, loop);
        currentAnimation = name;
    }
}

public enum PlayerState
{
    NONE,
    DIE,
    IDLE,
    RUN,
    JUMP,
    SWIM,
}

public enum PlayerHealth
{
    NONE = 0,
    LOWER = 1,
    HIGHT = 2,
}