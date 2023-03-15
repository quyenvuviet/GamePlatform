using Game.Scripts.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            Debug.Log("co the ban dan");
        }
    }
}
