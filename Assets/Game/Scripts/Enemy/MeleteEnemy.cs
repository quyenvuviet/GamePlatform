using Game.Scripts.player;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class MeleteEnemy : MonoBehaviour
    {
        [SerializeField] private float AttackCoolDown;
        [SerializeField] private int _dame;
        [SerializeField] private float ranger;
        [SerializeField] private float colloderDistance;
        private float coolDownTime = Mathf.Infinity;
        [SerializeField] private BoxCollider2D boxCollider2D;
        [SerializeField] private LayerMask playerLayer;
        private Animator animator;
        private Health playerHealth;
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
                    animator.SetTrigger("meteAttack");
                }
            }
            if (enemyPartrol != null)
            {
                enemyPartrol.enabled = !CkeckPlayerInSight();
            }
        }

        /// <summary>
        ///  check xem co va cham vao Plaeyer hay khong
        /// </summary>
        /// <returns></returns>
        private bool CkeckPlayerInSight()
        {
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center + transform.right * ranger * transform.localScale.x * colloderDistance, new Vector3(boxCollider2D.bounds.size.x * ranger, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z), 0, Vector2.left, 0, playerLayer);
            if (hit.collider != null)
            {
                playerHealth = hit.transform.GetComponent<Health>();
            }
            return hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * ranger * transform.localScale.x * colloderDistance, new Vector3(boxCollider2D.bounds.size.x * ranger, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
        }

        private void DamePlayer()
        {
            if (CkeckPlayerInSight())
            {
                playerHealth.TakeDame(_dame);
            }
        }
    }
}