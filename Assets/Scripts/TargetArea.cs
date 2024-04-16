using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArea : MonoBehaviour
{
    public float minSpeed = 2f;
    public float maxSpeed = 5f;

    public float minStopTime = 0.5f;
    public float maxStopTime = 3.5f;

    Vector3 targetPoints;

    public GameObject movement;
    public Transform[] movePoints;
    private List<int> availableIndices = new List<int>();
    private int currentTargetIndex;
    private int lastTargetIndex = -1;
    private int direction = 1;

    private float currentSpeed;
    private float currentStopTime;

    private void Awake()
    {
        movePoints = new Transform[movement.transform.childCount];
        for (int i = 0; i < movement.transform.childCount; i++)
        {
            movePoints[i] = movement.transform.GetChild(i);
            availableIndices.Add(i);
        }
    }

    private void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks); // Initialize random seed with current system time
        ChooseNextTarget();
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }


    private float pauseTimer = 0f;
    private bool isPaused = false;

    private void Update()
    {
        if (!isPaused) // Check if movement is not paused
        {
            var step = currentSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPoints, step);


            if (transform.position == targetPoints)
            {
                pauseTimer = Random.Range(minStopTime, maxStopTime); // Set the pause timer
                isPaused = true; // Pause movement
            }
        }
        else
        {
            // Movement is paused, decrement the pause timer
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                isPaused = false; // Resume movement
                ChooseNextTarget(); // Choose next target after pause
                currentSpeed = Random.Range(minSpeed, maxSpeed); // Update speed
            }
        }
    }


    private Coroutine currentCoroutine;

    private IEnumerator PauseAndChooseNextTarget()
    {
        isPaused = true; // Set isPaused to true at the beginning of the coroutine
        PauseTarget();
        yield return new WaitForSeconds(currentStopTime);
        ChooseNextTarget();
        isPaused = false; // Reset isPaused to false after the pause is completed
        currentCoroutine = null; // Reset currentCoroutine after the coroutine is completed
    }

    private void ChooseNextTarget()
    {
        if (!isPaused) // Check if movement is not paused before choosing next target
        {
            if (availableIndices.Count == 0)
            {
                Debug.LogError("No available targets.");
                return;
            }

            int randomIndex = Random.Range(0, availableIndices.Count); // Choose a random index from the list
            currentTargetIndex = availableIndices[randomIndex]; // Get the target index using the random index

            targetPoints = movePoints[currentTargetIndex].position;
            lastTargetIndex = currentTargetIndex;

            // Debug chosen target position
            Debug.Log($"Chosen Target Position: {targetPoints}");

            // Remove the selected index from the list of available indices to avoid selecting it again
            availableIndices.RemoveAt(randomIndex);
        }
        else
        {
            // Debug that movement is paused
            Debug.Log("Movement is paused. Skipping ChooseNextTarget...");
        }
    }



    private void PauseTarget()
    {
        currentStopTime = Random.Range(minStopTime, maxStopTime);
    }
}
