using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using STU;

public class GameController : Singleton<GameController>
{
    public enum States
    {
        NONE=0,
        DETROYED,
        PLAY,
        WIN,
        LOSE,
        LOADING,
    }
    protected  void Awake()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
