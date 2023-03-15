using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Scripts.player
{
    public class HealtheCollectTible : MonoBehaviour
    {
        [SerializeField] private float HealthValue;
      
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Health>().AddHeath(HealthValue);
                gameObject.SetActive(false);
            }
        }

    } 
}
