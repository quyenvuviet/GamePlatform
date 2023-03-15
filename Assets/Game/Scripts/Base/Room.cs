using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]private GameObject[] enemies;
    private Vector3[] initiaPostion;
    private void Awake()
    {
        initiaPostion = new Vector3[enemies.Length];
        for(int i = 0; i < initiaPostion.Length; i++)
        {
            if (enemies != null )
            initiaPostion[i] = enemies[i].transform.position;
        }
    }
    public void ActivationRoom(bool _status)
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position= initiaPostion[i];
            }
        }
    }
}
