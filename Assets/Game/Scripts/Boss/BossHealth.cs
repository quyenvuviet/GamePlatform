using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 6;
    public GameObject deathEfffect;
    public bool isInvulnerable = false;
    public void TakeDame(int dame)
    {
        if (isInvulnerable)
            return;
        health -=dame;
        if (health <= 3)
        {
            GetComponent<Animator>().SetBool("isEnraged",true);
        }
        if(health <= 0)
        {
            this.Die();
        }
    }
    private void Die()
    {
        Instantiate(deathEfffect, transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
