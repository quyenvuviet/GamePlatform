using Spine.Unity;
using UnityEngine;

namespace Game.Scripts.player
{
    public partial class PlayerMove : MonoBehaviour
    {
        public static PlayerMove instance;

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

        private Rigidbody2D body;

        [Header("=====core Player=======")]
        [SerializeField]
        private float speed;

        [SerializeField]
        private float maxSpeed;

        [SerializeField]
        private float jumpPower;

        [SerializeField]
        private float houderInput;

        [SerializeField]
        private float ckeckHouderInputAttack;

        private PlayerHealth levelPlayer;
        private float walljumpCoolider;
        private float horizontal;
        private Collider2D boxcollider;
        [SerializeField] private SkeletonAnimation AnimationData;
        private string currentAnimation = "";
        private bool isDisFaceRight;
        private bool isBullet = false;

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

        public bool IsBullet
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

        [Header("=======LayerMask=========")]
        [SerializeField] private LayerMask groundLayer;

        [SerializeField] private LayerMask groundwall;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else return;
            body = GetComponent<Rigidbody2D>();
            boxcollider = GetComponent<Collider2D>();
            levelPlayer = PlayerHealth.LOWER;
        }

        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            if (isGround())
            {
                if (horizontal != 0)
                    this.SetAnination("run");
                else if (horizontal == 0)
                {
                    this.SetAnination("idle");
                }
                else
                {
                    this.SetAnination("jump");
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                this.MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                this.MoveRight();
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                this.MoveStop();
            }
            body.velocity = new Vector2((isDisFaceRight == true ? 1 : -1) * speed, body.velocity.y);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.SetAnination("jump", false);
                body.gravityScale = 3;
                this.Jump();
            }
        }

        private void MoveStop()
        {
            speed = 0;
        }

        private void Distation()
        {
        }

        private float GetLevelPlayer(PlayerHealth playerHealth)
        {
            if (playerHealth == PlayerHealth.HIGHT)
            {
                return 1.4f;
            }
            return (int)levelPlayer;
        }

        /// <summary>
        ///
        /// </summary>
        private void MoveLeft()
        {
            isDisFaceRight = false;
            transform.localScale = new Vector3(-1 * GetLevelPlayer(levelPlayer), 1 * GetLevelPlayer(levelPlayer), 1);
            speed = maxSpeed;
        }

        private void MoveRight()
        {
            isDisFaceRight = true;
            transform.localScale = new Vector3(1 * GetLevelPlayer(levelPlayer), 1 * GetLevelPlayer(levelPlayer), 1);
            speed = maxSpeed;
        }

        /// <summary>
        /// nhân v?t nh?y lên
        /// </summary>
        private void Jump()
        {
            if (this.isGround())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else if (/*this.onWall() &&*/ !this.isGround())
            {
                if (horizontal == 0)
                {
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                    transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
                }
                walljumpCoolider = 0;
            }
        }

        /// <summary>
        /// ckeck xem có ch?m ??t vs m?t ??t hay không
        /// </summary>
        /// <returns></returns>
        private bool isGround()
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
            return raycastHit2D.collider != null;
        }

        /// <summary>
        /// Ki?m tra xem nhân v?t có ch?m v?i t??ng hay không
        /// </summary>
        /// <returns></returns>
        private bool onWall()
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, groundwall);
            return raycastHit2D.collider != null;
        }

        /// <summary>
        ///  có th? b?n khi
        /// </summary>
        /// <returns></returns>
        public bool canAttack()
        {
            return horizontal == 0 && isGround() && isBullet;
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
}