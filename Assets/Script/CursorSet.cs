using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSet : MonoBehaviour
{
    private void Update()
    {
        Vector3 cursoePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursoePos;

    }


}
