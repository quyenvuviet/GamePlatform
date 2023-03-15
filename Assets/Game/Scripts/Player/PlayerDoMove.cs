using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoMove : MonoBehaviour
{
    private bool playerMove;
    private bool isTagerPlayer;
    [SerializeField]
    private Transform DirPos;
    private float speed;
    CameraController cameraController;
    
    private  void Start()
    {
        playerMove = false;
    }

    // Update is called once per frame
    private void Update()
    {
       
    }
    public void PlayerMovePipe()
    {
        if (!this.CheckMove())
        {
            return;
        }
        move();

    }
    private bool CheckMove()
    {
        playerMove = true;
        return false;
    }
    private void move()
    {
        if (playerMove)
        {
            StartCoroutine(Offtaget());
            transform.position = new Vector3(DirPos.position.x * Time.deltaTime * speed, DirPos.position.y, DirPos.position.z);
            playerMove = false;
        }
       
    }
    private IEnumerator Offtaget()
    {
        if (isTagerPlayer)
        {
            cameraController.target = null;
        }
        yield return new WaitForSeconds(2f);
        isTagerPlayer = false;
    }
}
