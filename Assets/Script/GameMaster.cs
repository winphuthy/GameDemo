using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Unit selectedUnit;


    [SerializeField]
    public enum Turn
    {
        Player = 0,
        Ally = 1,
        Enemy = 2,
        Neutrality = 3
    }

    public Turn PlayerTurn;

    public GameObject selectedUnitSquare;

    void start()
    {
        PlayerTurn = Turn.Player;

        //register view

    }

    public void ResetTiles()
    {
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            tile.Reset();
        }
    }

    private void Update()
    {
        if (selectedUnit != null)
        {
            selectedUnitSquare.SetActive(true);
            selectedUnitSquare.transform.position = selectedUnit.transform.position;
        }
        else
        {
            selectedUnitSquare.SetActive(false);
        }
    }

    public void EndTurn()
    {
        Debug.Log("EndTurn");

        if (PlayerTurn == Turn.Neutrality)
        {
            PlayerTurn = Turn.Player;
        }
        else
        {
            PlayerTurn++;
        }


        if (selectedUnit != null) //if some unit has been selected
        {
            selectedUnit.selected = false; //cancel selection
            selectedUnit = null;
        }

        ResetTiles();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.hasMoved = false; //reset all unit to movable when turn end.
            unit.AttackSquare.SetActive(false);
            unit.hasAttacked = false;
        }
    }
}