using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField]
    private bool isGetDown;

    private bool isTagerPlayer;

    [SerializeField]
    private GameObject POSDir;

    private CameraController cameraController;

    private void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isGetDown)
        {
            Debug.Log("hien nut");
            StartCoroutine(Offtaget());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("dong");
    }

    private void MovePlayerPosPipe()
    {
        //OSDir.transform.position
    }

    private IEnumerator Offtaget()
    {
        yield return new WaitForSeconds(2f);
    }
}