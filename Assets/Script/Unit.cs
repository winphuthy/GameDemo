using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update

    public Character character;

    public bool selected;
    GameMaster gm;
    Navigation N;
    public int RestMoveable;
    public bool hasMoved;
    public int PlayerNumber;

    public int AttackRange;
    List<Unit> enemiesInRange = new List<Unit>();
    public bool hasAttacked;
    public GameObject AttackSquare;

    public Vector2Int position;






    private void OnEnable()
    {
        UpdatePosition();
    }

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        N = FindObjectOfType<Navigation>();
    }


    private void OnMouseDown()
    {
        ResetAttackableIcon();
        UpdatePosition();

        if (selected == true)//if this unit has been selected, cancel selected.
        {
            selected = false;
            gm.selectedUnit = null;
            gm.ResetTiles();
        }
        else// if did not selected this unit
        {
            if (PlayerNumber == gm.PlayerTurn)// if unit is below to play in the turn
            {
                if (gm.selectedUnit != null)//but select other unit
                {
                    gm.selectedUnit.selected = false;//cancel the selection from other unit
                }

                selected = true;//select this unit 
                gm.selectedUnit = this;

                gm.ResetTiles();
                GetEnemies();
                Debug.Log("before Achievable" + this.transform.position.ToString());
                N.Achievable(this, N.GetTile(this), this.RestMoveable);//GetWalkableTiles();//search available tile

            }
            
        }

        Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.2f);
        Unit unit = col.GetComponent<Unit>();


        if (gm.selectedUnit != null)
        {
            if (gm.selectedUnit.enemiesInRange.Contains(unit) && gm.selectedUnit.hasAttacked == false)
            {
                //gm.selectedUnit.Attack(unit);
            }
        }
    }

    private void UpdatePosition()
    {
        position = Vector2Int.RoundToInt(transform.position);
    }

    /*private void Attack(Unit enemy)
    {
        hasAttacked = true;
        int enemyDamage = attackDamage - enemy.armor;
        int myDamage = enemy.defenseDamage - armor;

        if (enemyDamage >= 1)
        {
            enemy.health -= enemyDamage;
            Debug.Log("Enemy damage deal");
        }

        if (myDamage >= 1)
        {
            health -= myDamage;
            Debug.Log("My damage take");
        }

        if (enemy.health <= 0)
        {
            Destroy(enemy.gameObject);
            GetWalkableTiles();
        }
    }*/

    public void GetWalkableTiles()
    {
        if (hasMoved == true)//if no more step left
        {
            return;//return null
        }
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + 
                Mathf.Abs(transform.position.y - tile.transform.position.y) <= RestMoveable)
            {
                tile.Highlight();
            }
            
        }
    }


    void GetEnemies() //get all enemies in said the range
    {
        enemiesInRange.Clear();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            if (Mathf.Abs(transform.position.x - unit.transform.position.x) + Mathf.Abs(transform.position.y - unit.transform.position.y) <= AttackRange)
            {
                if (unit.PlayerNumber != gm.PlayerTurn)
                {
                    enemiesInRange.Add(unit);
                    unit.AttackSquare.SetActive(true);
                    unit.AttackSquare.transform.position = unit.transform.position;
                    /*instant = Instantiate(AttackSquare);
                    instant.SetActive(true);*/

                }
            }
        }
    }

    public void ResetAttackableIcon() //reset the attackable 
    {
        Debug.Log("Attackable Icon reset");
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.AttackSquare.SetActive(false);
        }
    }


    public void Move(Vector2 tilePos) // move character
    {
        StartCoroutine(StartMovement(tilePos));
    }

    IEnumerator StartMovement(Vector2 tilePos)
    {
        gm.ResetTiles();
        while (transform.position.x != tilePos.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(tilePos.x, transform.position.y, transform.position.z), character.AbstractClass.MoveRange * Time.deltaTime);
            yield return null;
        }
        while (transform.position.y != tilePos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, tilePos.y, transform.position.z), character.AbstractClass.MoveRange * Time.deltaTime);
            yield return null;
        }

        
        hasMoved = true;
        ResetAttackableIcon();
        GetEnemies();
    }

}
