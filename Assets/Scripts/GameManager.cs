using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameTransitions gameTransitions;
    TutorialScript tutorial;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;

        gameTransitions = FindObjectOfType<GameTransitions>();
        tutorial = FindAnyObjectByType<TutorialScript>();

        gameTransitions.TransitionToFirstGame();

    }
}
