using Game.Scripts.player;
using UnityEngine;
using System;

public class Raw : MonoBehaviour
{
    [SerializeField] private float dame;
    [SerializeField] private float speed;
    private bool moveLeft;
    private float lefeEdge;
    private float rightEdge;
    [SerializeField] private float movementDistance;

    private void Start()
    {
        lefeEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if (moveLeft)
        {
            if (transform.position.x > lefeEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                moveLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                moveLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDame(dame);
        }
    }
    
}