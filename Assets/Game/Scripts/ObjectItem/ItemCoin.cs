using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "Player")
        {
            Debug.Log("+coin");
            SetActive(false);
        }
    }
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
