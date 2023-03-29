using Game.Scripts.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBullet : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            PlayerMove.instance.IsBullet = true;
            Debug.Log("co the ban dan");
        }
    }
}
