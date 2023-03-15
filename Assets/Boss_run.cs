using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.player;

public class Boss_run : StateMachineBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed=2.5f;
    private float attackRange=3.5f;
    private Boss boss;
    private Health playerHealth;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        Vector2 target = new Vector2 (player.position.x, player.position.y);
        Vector2 movenew = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(movenew);
        if(Vector2.Distance(player.position,rb.position) <= attackRange)
        {
            animator.SetTrigger("attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");

    }
   

}
