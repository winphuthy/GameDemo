using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer rend;
    public float hoverAmount;
    public Color HihglightedColor;
    public Sprite highlightSprite;
    public bool isWalkable;
    GameMaster gm;
    public Sprite tileGraphics;
    public Vector2Int position;
    public Vector2Int UpTile;
    public Vector2Int DownTile;
    public Vector2Int LeftTile;
    public Vector2Int RightTile;

    public TileType tileType;

    public int Onfoot;
    public int Riding;
    public int Flying;



    void OnEnable()
    {
        position = Vector2Int.RoundToInt(transform.localPosition);
        UpTile = position + Vector2Int.up;
        DownTile = position + Vector2Int.down;
        LeftTile = position + Vector2Int.left;
        RightTile = position + Vector2Int.right;

        Onfoot = 1;
        Riding = 1;
        Flying = 1;
    }

    public int MoveCost(MoveMethod moveCost)
    {
        switch (moveCost)
        {
            case MoveMethod.Onfoot:
                return Onfoot;
            case MoveMethod.Riding:
                return Riding;
            case MoveMethod.Flying:
                return Flying;
            default:
                throw new Exception("Data missing");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        gm = FindObjectOfType<GameMaster>();
        rend.sprite = tileGraphics;
    }

    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * hoverAmount;
    }

    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * hoverAmount;
    }
    // Update is called once per frame

    public void Highlight()
    {
        rend.color = HihglightedColor;
        isWalkable = true;
        Debug.Log(this.transform.position.ToString() + "highlight trigged");
    }

    public void Reset()
    {
        rend.color = Color.white;
        isWalkable = false;
    }

    private void OnMouseDown()
    {
        if (isWalkable && gm.selectedUnit != null)
        {
            gm.selectedUnit.Move(position);
        }
    }
}