using Game.Scripts.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class EnemyDamge : MonoBehaviour
    {
        [SerializeField] protected float _dame;
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Health>().TakeDame(_dame);
            }
        }
    }
}
