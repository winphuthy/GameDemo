using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Unit selectedUnit;

    public int PlayerTurn = 1;

    public GameObject selectedUnitSquare;

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

        if (PlayerTurn == 1)
        {
            PlayerTurn = 2;
        }
        else if (PlayerTurn ==2)
        {
            PlayerTurn = 1;
        }

        if (selectedUnit != null)//if some unit has been selected
        {
            selectedUnit.selected = false;//cancel selection
            selectedUnit = null;
        }

        ResetTiles();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.hasMoved = false;//reset all unit to movable when turn end.
            unit.AttackSquare.SetActive(false);
            unit.hasAttacked = false;
        }
    }
}
