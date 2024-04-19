using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BarMovement : MonoBehaviour
{
    public float speed;
    Vector3 targetPoints;

    public GameObject movement;
    public Transform[] movePoints;
    int pointIndex;
    int pointCount;
    int direction = 1;

    public List<GameObject> perfectAreas = new List<GameObject>();
    public List<GameObject> niceAreas = new List<GameObject>();
    public List<GameObject> badAreas = new List<GameObject>();

    private bool isStopped = false;

    private Animator animator;
    private TextManager textManager;

    GameTransitions transitions;

    private float endMinigame = 2f;

    private void Awake()
    {
        textManager = FindObjectOfType<TextManager>();
        transitions = FindObjectOfType<GameTransitions>();

        animator = GameObject.Find("Paws + Egg Sprite").GetComponent<Animator>(); 

        movePoints = new Transform[movement.transform.childCount];
        for (int i = 0; i < movement.transform.childCount; i++)
        {
            movePoints[i] = movement.transform.GetChild(i);
        }
    }

    private void Start()
    {
        pointCount = movePoints.Length;
        pointIndex = 1;
        targetPoints = movePoints[pointIndex].position;
    }

    private void Update()
    {
        if (!isStopped)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPoints, step);

            if (transform.position == targetPoints)
            {
                NextPoint();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isStopped = true;
                CheckStoppingArea();
                StartCoroutine(transitionWait());

            }
        }
    }

    private void NextPoint()
    {
        if (pointIndex == pointCount - 1) //has arrived at last point
        {
            direction = -1;
        }
        if (pointIndex == 0) // has arrived at first point
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPoints = movePoints[pointIndex].position;
    }

    private void CheckStoppingArea()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);

        foreach (Collider2D collider in colliders)
        {
            if (perfectAreas.Contains(collider.gameObject))
            {
                Debug.Log("Stopped over Perfect Area! You nailed it!");
                // Add actions for stopping over the perfect area
                textManager.PerfectTextActive();
                animator.SetBool("isCompleted", true); 
                return;
            }
            else if (niceAreas.Contains(collider.gameObject))
            {
                Debug.Log("Stopped over Nice Area. Good job!");
                // Add actions for stopping over the nice area
                textManager.NiceTextActive();
                animator.SetBool("isCompleted", true); 
                return;
            }
            else if (badAreas.Contains(collider.gameObject))
            {
                Debug.Log("Stopped over Bad Area. Oops!");
                // Add actions for stopping over the bad area
                textManager.BadTextActive();
                animator.SetBool("isCompleted", true);
                return;
            }
        }

        Debug.Log("Stopped over unknown area.");
    }

    private IEnumerator transitionWait()
    {
        yield return new WaitForSecondsRealtime(endMinigame);
        textManager.SetAllTextInactive();
        
        transitions.TransitionToSecondGame();
    }

}
