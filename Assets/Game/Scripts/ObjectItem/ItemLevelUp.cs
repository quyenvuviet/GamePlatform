using Game.Scripts.player;
using UnityEngine;
using static Game.Scripts.player.PlayerMove;

public class ItemLevelUp : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float range = 2f;

    [SerializeField]
    private LayerMask groudMask;

    [SerializeField]
    private LayerMask wallMask;

    [SerializeField]
    private Vector3[] direction = new Vector3[3];

    [SerializeField]
    private Vector3 dir;

    [SerializeField]
    private PlayerMove player;

    private void Awake()
    {
        player.GetComponent<PlayerMove>();
    }

    private void Start()
    {
        dir = Vector3.left;
    }

    private void Update()
    {
        if (Horizonal() || Horizonal1())
        {
            ChangeDir();
        }
        this.MoveItem();
    }

    public void MoveItem()
    {
        transform.Translate(dir * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            player.LevelPlayer = PlayerHealth.HIGHT;
        }
    }

    /// <summary>
    /// chieu doc
    /// </summary>
    /// <returns></returns>
    private void vertical()
    {
        CalulateDiretions();
        RaycastHit2D raycast2dVertical = CommonDebug.RayCast(transform.position, direction[2], 0.3f, groudMask, Color.red, true);
        if (!raycast2dVertical.collider)
        {
            ChangeDir();
        }
    }

    /// <summary>
    ///  chieu dai
    /// </summary>
    private bool Horizonal()
    {
        CalulateDiretions();
        RaycastHit2D raycast2dHozizonal = CommonDebug.RayCast(transform.position, direction[0], 0.3f, wallMask, Color.green, true);
        return raycast2dHozizonal.collider != null;
    }

    private bool Horizonal1()
    {
        CalulateDiretions();
        RaycastHit2D raycast2dHozizonal = CommonDebug.RayCast(transform.position, direction[1], 0.3f, wallMask, Color.green, true);
        return raycast2dHozizonal.collider != null;
    }

    private bool CkeckGound()
    {
        CalulateDiretions();

        for (int i = 0; i <= direction.Length; i++)
        {
            RaycastHit2D ray3 = CommonDebug.RayCast(new Vector2(transform.position.x, transform.position.y), direction[i], 0.3f, groudMask, Color.red, true);
            return ray3.collider != null;
        }
        return false;
    }

    private void ChangeDir()
    {
        this.dir = -this.dir;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void CalulateDiretions()
    {
        direction[0] = transform.right * range;// ben phai
        direction[1] = -transform.right * range;// ben trai
        direction[2] = -transform.up * range;// ben duoi
    }
}