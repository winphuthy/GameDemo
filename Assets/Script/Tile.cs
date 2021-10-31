using System;
using System.Collections;
using System.Collections.Generic;
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
    public Vector2Int positionUp;
    public Vector2Int positionDown;
    public Vector2Int positionLeft;
    public Vector2Int positionRight;


    void OnEnable()
    {
        position = Vector2Int.RoundToInt(transform.position);
        positionUp.Set(position.x, position.y + 1);
        positionDown.Set(position.x, position.y - 1);
        positionRight.Set(position.x + 1, position.y);
        positionLeft.Set(position.x - 1, position.y);
    }

    public int MoveCost(MoveMethod moveCost)
    {
        switch (moveCost)
        {
            case MoveMethod.Onfoot:
                return 1;
            case MoveMethod.Riding:
                return 1;
            case MoveMethod.Flying:
                return 1;
            default:
                throw new Exception();
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
            gm.selectedUnit.Move(this.transform.position);
        }
    }
}