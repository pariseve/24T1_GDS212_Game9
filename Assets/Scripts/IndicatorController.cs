using UnityEngine;
using System.Collections;

public class IndicatorController : MonoBehaviour
{
    public Transform minPos;
    public Transform maxPos;
    public float moveSpeed = 1f; // speed at which the indicator moves when space is pressed
    public float moveBackSpeedMin = 1f; // speed of the indicator when space is released
    public float moveBackSpeedMax = 3.5f;

    private IEnumerator moveCoroutine;

    public Transform targetArea;
    private bool isOverTarget = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine); // Stop any previous movement coroutine
            }
            moveCoroutine = MoveTowardsMaxPos();
            StartCoroutine(moveCoroutine); // Start moving towards maxPos
        }
        else if (!Input.GetKey(KeyCode.Space)) // Check if space key is not being held down
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine); // Stop any previous movement coroutine
            }
            moveCoroutine = MoveTowardsMinPos();
            StartCoroutine(moveCoroutine); // Start moving towards minPos
        }

        // Check if the indicator is within the target area
        if (IsIndicatorWithinTargetArea())
        {
            // Do something when the indicator is within the target area
            Debug.Log("Indicator is within the target area.");
        }
        else
        {
            // Do something when the indicator is not within the target area
            Debug.Log("Indicator is not within the target area.");
        }
    }

    private bool IsIndicatorWithinTargetArea()
    {
        // check if the indicator's position is within the bounds of the target area
        return targetArea.GetComponent<Collider2D>().bounds.Contains(transform.position);
    }

    private IEnumerator MoveTowardsMaxPos()
    {
        while (Vector3.Distance(transform.position, maxPos.position) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, maxPos.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MoveTowardsMinPos()
    {
        while (Vector3.Distance(transform.position, minPos.position) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, minPos.position, moveBackSpeedMin * Time.deltaTime);
            yield return null;
        }
    }
}