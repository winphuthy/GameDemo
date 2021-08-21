using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    public Level level;
    void Start()
    {
        level = new Level(1,0);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        level.AddEXP(100);
    }

}
