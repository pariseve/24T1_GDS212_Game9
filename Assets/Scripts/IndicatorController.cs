using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    public Transform minPos;
    public Transform maxPos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // move the indicator up in small increments each time the space key is pressed
        }
        else
        {
            // make the indicator slowly return back to minPos
        }
    }
}
