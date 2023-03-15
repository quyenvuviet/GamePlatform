/*using Game.Scripts.Camera;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform PreviousRoom;
    [SerializeField] private Transform NextRoom;
  //  private Game.Scripts.Camera.CameraController cam;
    private void Awake()
    {
       // cam =Camera.main.GetComponent<Game.Scripts.Camera.CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if (collision.transform.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MovetoNextRoom(NextRoom);
                NextRoom.GetComponent<Room>().ActivationRoom(true);
                PreviousRoom.GetComponent<Room>().ActivationRoom(false);
            }
            else
            {
                cam.MovetoNextRoom(PreviousRoom);
                NextRoom.GetComponent<Room>().ActivationRoom(false);
                PreviousRoom.GetComponent<Room>().ActivationRoom(true);
            }

           
        }
    }
}*/