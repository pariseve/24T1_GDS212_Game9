using UnityEngine;

public class StirringController : MonoBehaviour
{
    public float maxStirringSpeed = 10f; 
    public float stirringAcceleration = 5f; 
    public float friction = 2f; 

    private bool isStirring = false;
    private Vector2 lastMousePos;

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
        }
    }
}
