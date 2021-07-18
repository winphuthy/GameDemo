using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Testing : MonoBehaviour
{
    Navigation N;
    Initalization init;
    // Start is called before the first frame update
    void Start()
    {
        N = FindObjectOfType<Navigation>();
        init = FindObjectOfType<Initalization>();
        /*
        */
        /*GameObject test1 = GameObject.Find("ArmouredRedDemon_0");
        
        unit.GetWalkableTiles();*/



        // GameObject test1 = GameObject.Find("ArmouredRedDemon_0");

        //Debug.Log(Vector2Int.RoundToInt(test1.transform.position));

        // Unit unit = N.GetUnit(Vector2Int.RoundToInt(test1.transform.position));

        //Debug.Log(unit.transform.position.ToString());
        /*N.Achievable(N.GetUnit(test1.transform.position), N.GetTile(test1.transform.position),
        N.GetUnit(test1.transform.position).RestMoveable);*/

        // Tile tile = N.GetTile(Vector2Int.RoundToInt(test1.transform.position));


        //Debug.Log(tile.transform.position.ToString());

        /* foreach (Tile tile1 in N.neighbour(tile))
         {
             tile1.Highlight();
         }*/




        Queue<int> testQueue = new Queue<int>();

        testQueue.Enqueue(1);
        testQueue.Enqueue(1);
        testQueue.Enqueue(1);
        testQueue.Enqueue(4);
        testQueue.Enqueue(1);
        testQueue.Enqueue(1);
        testQueue.Enqueue(3);
        testQueue.Enqueue(1);
        testQueue.Enqueue(1);
        testQueue.Enqueue(1);
        testQueue.Enqueue(1);
        testQueue.Enqueue(1);

        foreach (var item in testQueue.ToArray())
        {
            Debug.Log(item); 
        } 



    }
}
