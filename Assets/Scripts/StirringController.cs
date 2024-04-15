using UnityEngine;

public class StirringController : MonoBehaviour
{
    public float maxStirringSpeed = 10f;
    public float stirringAcceleration = 5f;
    public float friction = 2f;
    public float directionChangeThreshold = 0.5f; // Threshold to change direction (in seconds)
    public float directionChangeDelay = 2f; // Delay before changing direction (in seconds)

    public enum StirringDirection { Clockwise, CounterClockwise }
    public StirringDirection[] stirringDirections; // List of stirring directions

    public TextManager textManager; // Reference to the TextManager script for displaying UI

    private bool isStirring = false;
    private Vector2 lastMousePos;
    private int currentDirectionIndex = 0;
    private float timeSinceDirectionChange = 0f;

    void Start()
    {
        UpdateStirringDirectionUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isStirring = true;
            lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isStirring = false;
        }

        if (isStirring)
        {
            // Calculate stirring speed based on mouse movement
            Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 stirringDirection = currentMousePos - (Vector2)transform.position;

            // Calculate stirring speed
            float stirringSpeed = stirringDirection.magnitude * stirringAcceleration;
            stirringSpeed = Mathf.Clamp(stirringSpeed, 0f, maxStirringSpeed);

            // Calculate rotation angle
            float rotationAngle = Mathf.Atan2(stirringDirection.y, stirringDirection.x) * Mathf.Rad2Deg;

            // Smoothly rotate the object towards the desired angle
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotationAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, stirringSpeed * Time.deltaTime);

            // Update last mouse position
            lastMousePos = currentMousePos;

            // Check stirring direction continuously
            CheckStirringDirection();

            // Check if direction needs to be changed
            timeSinceDirectionChange += Time.deltaTime;
            if (timeSinceDirectionChange >= directionChangeThreshold)
            {
                ChangeDirection();
                timeSinceDirectionChange = 0f; // Reset time since direction change
            }
        }
    }

    void ChangeDirection()
    {
        // Change to the next direction in the list
        currentDirectionIndex = (currentDirectionIndex + 1) % stirringDirections.Length;

        UpdateStirringDirectionUI(); // Update UI with new stirring direction
    }

    void CheckStirringDirection()
    {
        // Get the current stirring direction
        StirringDirection currentDirection = stirringDirections[currentDirectionIndex];

        // Calculate the expected stirring direction based on the current rotation of the object
        StirringDirection expectedDirection = CalculateExpectedDirection();

        // Check if the actual stirring direction matches the expected direction
        if (currentDirection != expectedDirection)
        {
            // Activate the wrong direction text GameObject
            textManager.wrongDirectionText.gameObject.SetActive(true);
        }
        else
        {
            // Deactivate the wrong direction text GameObject
            textManager.wrongDirectionText.gameObject.SetActive(false);
        }
    }

    StirringDirection CalculateExpectedDirection()
    {
        // Get the rotation angle of the object
        float currentRotation = transform.eulerAngles.z;

        // Determine the expected stirring direction based on the rotation angle
        if (currentRotation < 0f)
        {
            // Normalize the rotation angle to be within [0, 360) degrees
            currentRotation += 360f;
        }

        // Define the threshold angle for clockwise stirring (90 degrees)
        float clockwiseThreshold = 90f;

        // Determine the expected stirring direction based on the rotation angle
        if (currentRotation < clockwiseThreshold || currentRotation >= 360f - clockwiseThreshold)
        {
            return StirringDirection.Clockwise;
        }
        else
        {
            return StirringDirection.CounterClockwise;
        }
    }

    void UpdateStirringDirectionUI()
    {
        // Update UI to display the current stirring direction
        StirringDirection currentDirection = stirringDirections[currentDirectionIndex];
        if (currentDirection == StirringDirection.Clockwise)
        {
            textManager.SetDirectionText("Clockwise");
        }
        else
        {
            textManager.SetDirectionText("Counter-clockwise");
        }
    }
}
