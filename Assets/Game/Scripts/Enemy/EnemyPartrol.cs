using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Scripts.Enemy
{
    public class EnemyPartrol : MonoBehaviour
    {
        [SerializeField] private Transform leftEdge;

        [SerializeField] private Transform rightEdge;
        [SerializeField] private Transform enemy;
        [SerializeField] private float speed;
        private Vector3 initScale;
        [SerializeField]
        private Animator animator;
        private bool moveLeft;
        private void Awake()
        {
            initScale = enemy.localScale;
        }
        private void OnDisable()
        {
            animator.SetBool("moving", false);
        }
        private void Update()
        {
            if (this.moveLeft)
            {
                if (this.enemy.position.x >= this.leftEdge.position.x)
                {
                    this.MoveInDirection(-1);
                }
                else
                {
                    this.DircetionChange();
                }
            }
            else
            {

                if (this.enemy.position.x <= this.rightEdge.position.x)
                {
                    this.MoveInDirection(1);
                }
                else
                {
                    this.DircetionChange();
                }


            }
        }
       
        private void DircetionChange()
        {
            animator.SetBool("moving",false);
            this.moveLeft = !this.moveLeft;
        }
        private void MoveInDirection(int _direction)
        {
            
            this.animator.SetBool("moving", true);
            this.enemy.transform.localScale = new Vector3(Mathf.Abs(initScale.x)*_direction, this.initScale.y, this.initScale.z);
            this.enemy.position = new Vector3(this.enemy.position.x+ Time.deltaTime*_direction, this.enemy.position.y, this.enemy.position.z);
        } 
    }

}
