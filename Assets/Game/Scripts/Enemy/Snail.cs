using Game.Scripts.player;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Snail : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private SkeletonAnimation animationSnail;
    [SerializeField] private LayerMask Wall;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask maskPlayer;
    [SerializeField] private int dame;
    private Rigidbody2D body;
    private Vector3 Diretion;
    private string nameanimation;
    [SerializeField]
    private float ranger;
    private float size;
    private bool isDead;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animationSnail = GetComponent<SkeletonAnimation>();
        size = transform.localScale.x;
    }

    private void Start()
    {
        Diretion = Vector3.right;
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        CkeckVetical();
        CkeckHorizonal();
        CkeckHorizonalLeft();
        CheckTop();
        Move();
    }
    private void Move()
    {
        transform.Translate(Diretion * speed * Time.deltaTime);
    }
    private void Animation(string name, bool loop)
    {
        if (name== nameanimation)
        {
            return;
        }
         animationSnail.state.SetAnimation(0, name, loop);
         nameanimation= name;


    }

    private void ChangerDir()
    {
        Diretion = -Diretion;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    private void CkeckVetical()
    {
        RaycastHit2D ray = CommonDebug.RayCast(transform.position, Vector2.down, 0.5f, ground, Color.red, true);
        if (!ray.collider)
        {
            ChangerDir();
        }
    }
    private void CkeckHorizonal()
    {
        RaycastHit2D ray = CommonDebug.RayCast(transform.position, Vector2.right, 0.5f, Wall, Color.red, true);

        if (ray.collider)
        {
            ChangerDir();
        }
    }
    private void CkeckHorizonalLeft()
    {
        RaycastHit2D ray = CommonDebug.RayCast(transform.position, -Vector2.right, 0.5f, Wall, Color.red, true);
        if (ray.collider)
        {
            ChangerDir();
        }
    }
    /// <summary>
    /// che dau
    /// </summary>
    protected virtual void CheckTop()
    {
        if (isDead) return;
        var a = new Vector2(transform.position.x, transform.position.y);
        var b = transform.localScale.x;
        RaycastHit2D raycast = CommonDebug.RayCast(a + new Vector2(box.offset.x * b, box.offset.y * size) + new Vector2(0, box.size.y), Vector2.up,
            0.1f * size, maskPlayer, Color.green, true);
        RaycastHit2D raycast2 = CommonDebug.RayCast(a + new Vector2(box.offset.x * b, box.offset.y * size) + new Vector2(-box.size.x * 3 / 4, box.size.y), Vector2.up,
            0.1f * size, maskPlayer, Color.green, true);
        RaycastHit2D raycast3 = CommonDebug.RayCast(a + new Vector2(box.offset.x * b, box.offset.y * size) + new Vector2(box.size.x * 3 / 4, box.size.y), Vector2.up,
            0.1f * size, maskPlayer, Color.green, true);
        RaycastHit2D raycast4 = CommonDebug.RayCast(a + new Vector2(box.offset.x * b, box.offset.y * size) + new Vector2(-box.size.x * 3 / 4 / 2, box.size.y), Vector2.up,
            0.1f * size, maskPlayer, Color.green, true);
        RaycastHit2D raycast5 = CommonDebug.RayCast(a + new Vector2(box.offset.x * b, box.offset.y * size) + new Vector2(box.size.x * 3 / 4 / 2, box.size.y), Vector2.up,
            0.1f * size, maskPlayer, Color.green, true);
        RaycastHit2D ray1 = Physics2D.Raycast(transform.position + new Vector3(0, box.bounds.size.y), Vector2.up,
            0.1f * size, LayerMask.NameToLayer("Player"));
        if (raycast || raycast2 || raycast3 || raycast4 || raycast5)
        {
            Debug.Log(raycast.collider);
            Debug.Log(raycast2.collider);
            Debug.Log(raycast3.collider);
            StartCoroutine(IeDead_AddForce());
        }
    }
   
    protected internal virtual IEnumerator IeDead_AddForce()
    {
        isDead = true;
        box.enabled = false;
        body.AddForce(new Vector2(0, 1) * 8, ForceMode2D.Impulse);
        transform.DORotate(new Vector3(0, 0, 180), 0.01f);
        yield return new WaitForSeconds(0.3f);
        body.AddForce(Vector2.down * 8, ForceMode2D.Impulse);

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDame(dame);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(IeDead_AddForce());
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
    }
}
