using Game.Scripts.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Scripts.Enemy
{
    public class FireTrap : MonoBehaviour
    {
        /// <summary>
        /// Dame Player Take Dame
        /// </summary>
        [SerializeField] private float _dame;
        private bool trigered;
        private bool active;
        /// <summary>
        /// Time ActiveDelay Fire Trap 
        /// </summary>
        [SerializeField]
        private float ActivationDelay;
        /// <summary>
        /// Live FrieTrap
        /// </summary>
        [SerializeField]
        private float activeTime;
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private void Awake()
        {
            
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                if (!trigered)
                {
                    StartCoroutine(ActiveteFireTrap());
                }
                if (active)
                {
                    collision.GetComponent<Health>().TakeDame(_dame);

                }
            }
        }
        IEnumerator ActiveteFireTrap()
        {
            trigered = true;
            spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(ActivationDelay);
            spriteRenderer.color = Color.white;
            active = true;
            animator.SetBool("actived", true);
            yield return new WaitForSeconds(activeTime);
            active = false;
            trigered = false;
            animator.SetBool("actived", false);

        }

    }
}
