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
