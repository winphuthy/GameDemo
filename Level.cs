using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Level 
{
    public int currentLevel;
    public int currentEXP;
    public int ExpForNextLevel;

    public Level(int currentLevel, int currentEXP)
    {
        this.currentLevel = currentLevel;
        this.currentEXP = currentEXP;
        SetExpForLevelup();

    }

    public void SetExpForLevelup()
    {    
        ExpForNextLevel = (int)Math.Floor(100*Math.Pow(1.25, currentLevel - 1));
    }

    public void AddEXP(int EXP)
    {
        currentEXP += EXP;
        if(currentEXP - ExpForNextLevel >= 0)
        {
            LevelUp();
        }

        Debug.Log("Current Level is: " + currentLevel + " Current EXP is: " +currentEXP + " EXP needed to Next Level: " + ExpForNextLevel);
    }

    public void LevelUp()
    {
        currentEXP = currentEXP - ExpForNextLevel;
        currentLevel++;
        SetExpForLevelup();
    }
}
