using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class EnemyProjectile : EnemyDamge
{

        [SerializeField] protected float speed;
        [SerializeField] protected float restTime;
         protected float lifeTime;
       public void ActiveProjectile()
        {
            lifeTime = 0;
            gameObject.SetActive(true);
        }
        public void Update()
        {
            float movementSpeed = speed* Time.deltaTime;
            transform.Translate(movementSpeed,0,0);
            lifeTime += Time.deltaTime;
            if (lifeTime > restTime)
            {
                gameObject.SetActive(false );
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            gameObject.SetActive(false);
        }
    }
}
