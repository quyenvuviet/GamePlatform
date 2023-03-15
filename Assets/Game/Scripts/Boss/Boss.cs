using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.player;


public class Boss : MonoBehaviour
{
    [SerializeField]private Transform player;
    [SerializeField] private bool isFliped = false;
    [SerializeField]private BoxCollider2D boxCollider2D;
    private Health playerHealth;
    [SerializeField] private float ranger;
    [SerializeField] private float colloderDistance;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float _dame;

    private void Awake()
    {
       // playerHealth.GetComponent<Health>();
    }
    public void LookAtPlayer()
    {
        Vector3 fliped = transform.localScale;
        fliped.z *= -1;
        if(transform.position.x > player.position.x && isFliped)
        {
            transform.localScale = fliped;
            transform.Rotate(0f, 180f, 0f);
            isFliped = false;
        }
        else if(transform.position.x < player.position.x && !isFliped)
        {
            transform.localScale = fliped;
            transform.Rotate(0f,180f,0f);
            isFliped = true;
        }
    }
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCollider2D.bounds.center + transform.right * ranger * transform.localScale.x * colloderDistance, new Vector3(boxCollider2D.bounds.size.x * ranger, boxCollider2D.bounds.size.y, boxCollider2D.bounds.size.z));
    }
    private void AttackDam()
    {
        if (CkeckPlayerInSight())
        {
            playerHealth.TakeDame(_dame);

        }
    }
}
