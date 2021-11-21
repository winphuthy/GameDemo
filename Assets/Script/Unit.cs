using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    public Character character;

    public bool selected;

    // public int HP;
    GameMaster gm;
    Navigation N;
    public int RestMoveable;
    public bool hasMoved;
    public GameMaster.Turn PlayerNumber;

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
        N = Navigation.instance;
        character = CharacterModel.instance.White;
        // HP = character.Stamina;
    }

    private void OnMouseDown()
    {
        ResetAttackableIcon();
        UpdatePosition();

        if (selected) //if this unit has been selected, cancel selected.
        {
            selected = false;
            gm.selectedUnit = null;
            gm.ResetTiles();
            Debug.Log("selected cancelled");
        }
        else // if did not selected this unit
        {
            if (PlayerNumber == gm.PlayerTurn) // if unit is below to play in the turn
            {
                if (gm.selectedUnit != null) //but select other unit
                {
                    gm.selectedUnit.selected = false; //cancel the selection from other unit
                }

                selected = true; //select this unit 
                gm.selectedUnit = this;
                Debug.Log($"{name}:{position} has been selected");

                gm.ResetTiles();
                GetEnemies();
                Debug.Log("before Achievable" + transform.localPosition);
                if (hasMoved != this)
                {
                    N.Achievable(this); //GetWalkableTiles();//search available tile
                }
            }
        }

        Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.2f);
        Unit unit = col.GetComponent<Unit>();

        if (gm.selectedUnit != null)
        {
            if (gm.selectedUnit.enemiesInRange.Contains(unit) && gm.selectedUnit.hasAttacked == false)
            {
                // gm.selectedUnit.Attack(unit);
            }
        }
    }

    private void UpdatePosition()
    {
        position = Vector2Int.RoundToInt(transform.localPosition);
    }

    private void Attack(Unit enemy)
    {
        hasAttacked = true;


        /*int enemyDamage = attackDamage - Enemy.armor;
        int myDamage = Enemy.defenseDamage - armor;

        if (enemyDamage >= 1)
        {
            Enemy.health -= enemyDamage;
            Debug.Log("Enemy damage deal");
        }

        if (myDamage >= 1)
        {
            health -= myDamage;
            Debug.Log("My damage take");
        }

        if (Enemy.health <= 0)
        {
            Destroy(Enemy.gameObject);
            GetWalkableTiles();
        }*/
    }

    public void GetWalkableTiles()
    {
        if (hasMoved) //if no more step left
        {
            return; //return null
        }

        foreach (var tile in MapModel.instance.TileTable)
        {
            if (Mathf.Abs(transform.position.x - tile.Value.transform.position.x) +
                Mathf.Abs(transform.position.y - tile.Value.transform.position.y) <= RestMoveable)
            {
                tile.Value.Highlight();
            }
        }
    }


    void GetEnemies() //get all enemies in said the range
    {
        enemiesInRange.Clear();

        foreach (var unit in MapModel.instance.UnitTable)
        {

            if (Mathf.Abs(transform.position.x - unit.Value.transform.position.x) +
                Mathf.Abs(transform.position.y - unit.Value.transform.position.y) <= AttackRange)
            {
                if (unit.Value.PlayerNumber != gm.PlayerTurn)
                {
                    enemiesInRange.Add(unit.Value);
                    unit.Value.AttackSquare.SetActive(true);
                    unit.Value.AttackSquare.transform.position = unit.Value.transform.position;
                    /*instant = Instantiate(AttackSquare);
                    instant.SetActive(true);*/
                }
            }
        }
    }

    public void ResetAttackableIcon() //reset the attackable 
    {
        Debug.Log("Attackable Icon reset");
        foreach (var unit in MapModel.instance.UnitTable)
        {
            unit.Value.AttackSquare.SetActive(false);
        }
        /*foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.AttackSquare.SetActive(false);
        }*/
    }


    public void Move(Vector2Int destination) // move character
    {
        StartCoroutine(StartMovement(destination));
    }

    IEnumerator StartMovement(Vector2Int destination)
    {
        gm.ResetTiles();


        /*foreach (var vector2Int in Navigation.instance.GetPathToDestination(this, destination))
        {
            while (transform.position.x != vector2Int.x)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(vector2Int.x, transform.position.y, transform.position.z),
                    character.Occupation.MoveRange * Time.deltaTime);
                yield return null;
            }

            while (transform.position.y != vector2Int.y)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(transform.position.x, vector2Int.y, transform.position.z),
                    character.Occupation.MoveRange * Time.deltaTime);
                yield return null;
            }
        }*/

        List<Vector2Int> path = Navigation.instance.GetPathToDestination(this, destination);

        for (int i = path.Count - 1; i >= 0; i--)
        {
            while (transform.position.x != path[i].x)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(path[i].x, transform.position.y, transform.position.z),
                    character.Occupation.MoveRange * Time.deltaTime);
                yield return null;
            }

            while (transform.position.y != path[i].y)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(transform.position.x, path[i].y, transform.position.z),
                    character.Occupation.MoveRange * Time.deltaTime);
                yield return null;
            }
        }

        hasMoved = true;
        ResetAttackableIcon();
        GetEnemies();
    }
}