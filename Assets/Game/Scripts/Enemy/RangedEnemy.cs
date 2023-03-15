using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class RangedEnemy : MonoBehaviour
    {
        [Header("Attack Prameters")]
        [SerializeField] private float AttackCoolDown;

        [SerializeField] private int _dame;
        [SerializeField] private float ranger;

        [Header("Ranged Attack")]
        [SerializeField] private Transform firepoint;

        [SerializeField] private GameObject[] fireball;

        [Header("Collider Prammeters")]
        [SerializeField] private float colloderDistance;

        [SerializeField] private BoxCollider2D boxCollider2D;

        [Header("Player layer")]
        [SerializeField] private LayerMask playerLayer;

        private float coolDownTime = Mathf.Infinity;
        private Animator animator;
        private EnemyPartrol enemyPartrol;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            enemyPartrol = GetComponentInParent<EnemyPartrol>();
        }

        private void Update()
        {
            coolDownTime += Time.deltaTime;
            if (CkeckPlayerInSight())
            {
                if (coolDownTime >= AttackCoolDown)
                {
                    coolDownTime = 0;
                    animator.SetTrigger("rangerAttack");
                }
            }
            if (enemyPartrol != null)
            {
                enemyPartrol.enabled = !CkeckPlayerInSight();
            }
        }


        private void RangedAttack()
        {
            coolDownTime = 0;
            fireball[FindFireBall()].transform.position = firepoint.position;
            fireball[FindFireBall()].GetComponent<EnemyProjectile>().ActiveProjectile();
        }
        private int FindFireBall()
        {
            for (int i = 0; i < fireball.Length; i++)
            {
                if (!fireball[i].activeInHierarchy)
                {
                    return i;
                }
            }
            return 0;
        }
        /// <summary>
        ///  check xem co va cham vao Plaeyer hay khong
        /// </summary>
        /// <returns></returns>
        private bool CkeckPlayerInSight()
        {
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center + transform.right * ranger * transform.localScale.x * colloderDistance, new Vector3(boxCollider2D.bounds.size.x * ranger, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z), 0, Vector2.left, 0, playerLayer);

            return hit.collider != null;
        }
        /// <summary>
        ///  V? ra hình ?? bi?t ph?p vi ?ánh c?a nhân v?t
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * ranger * transform.localScale.x * colloderDistance, new Vector3(boxCollider2D.bounds.size.x * ranger, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
        }
    }
}