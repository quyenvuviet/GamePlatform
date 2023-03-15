using UnityEngine;
using System;
using System.Collections;
namespace Game.Scripts.player
{
    /// <summary>
    /// 
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [SerializeField] public float speed;
        [SerializeField] private float direction;
        private bool hit;
        [SerializeField] private Animator animator;
        [SerializeField] private float lifetime;
        private BoxCollider2D boxCollider;


        private void Awake()
        {
            animator = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
        }
        public void OnStart()
        {
            boxCollider.enabled = true;
            StartCoroutine(IeDespawn(lifetime));
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (hit)
            {
                return;
            }
            
            if (collision.tag == "enemy")
            {
                Debug.Log("enemy");
                hit = true;
                collision.GetComponent<Health>().TakeDame(1);
                boxCollider.enabled = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                animator.SetTrigger("explode");
                StartCoroutine(IeDespawn(5f));
            }
            if (collision.CompareTag("ground"))
            {
                Debug.Log("ground");
                hit = true;
                var Pos = boxCollider.bounds.size.x / 2;
                transform.position = new Vector2(transform.position.x+Pos, transform.position.y);
                boxCollider.enabled = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                animator.SetTrigger("explode");
                StartCoroutine(IeDespawn(5f));

            }
          
            
        }
        /// <summary>
        ///  ??i trong vòng bao lâu thì viên ??n có th? xóa
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        IEnumerator IeDespawn(float time)
        {
            yield return new WaitForSeconds(time);
            this.Recyle();

        }
        /// <summary>
        /// xóa viên ??n
        /// </summary>
        private void Recyle()
        {
            this.gameObject.Recycle();
        }
        /// <summary>
        /// take Dame
        /// </summary>
        private void Deactive()
        {
            gameObject.SetActive(false);
        }
    }
}