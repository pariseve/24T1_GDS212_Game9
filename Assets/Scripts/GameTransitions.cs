using System.Collections;
using UnityEngine;
using TMPro;

public class GameTransitions : MonoBehaviour
{
    [Header("Minigames")]
    public GameObject firstMiniGame;
    public GameObject secondMiniGame;
    public GameObject thirdMiniGame;

    [Header("Transition Panels")]
    public GameObject firstPanel;
    public GameObject secondPanel;
    public GameObject thirdPanel;
    public GameObject fourthPanel;
    public GameObject scorePanel;

    public float transitionDuration = 1f; // Duration of the transition in seconds
    public TextMeshProUGUI countdownText;

    private GameObject currentMiniGame; // Reference to the current mini-game GameObject
    private IEnumerator transitionCoroutine;

    public bool isFirstGameActive = false;
    public bool isSecondGameActive = false;
    public bool isThirdGameActive = false;


    private void Start()
    {
        //startPanel.SetActive(true);
    }

    // Call this method to transition to the first mini-game
    public void TransitionToFirstGame()
    {
            if (transitionCoroutine != null)
            {
                StopCoroutine(transitionCoroutine); // Stop any ongoing transition
            }

        isFirstGameActive = true;
        transitionCoroutine = Transition(firstPanel, firstMiniGame);
        StartCoroutine(transitionCoroutine);
        
    }


    // Call this method to transition to the second mini-game
    public void TransitionToSecondGame()
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine); // Stop any ongoing transition
        }
        transitionCoroutine = Transition(secondPanel, secondMiniGame);
        StartCoroutine(transitionCoroutine);
        isSecondGameActive=true;
    }

    // Call this method to transition to the third mini-game
    public void TransitionToThirdGame()
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine); // Stop any ongoing transition
        }
        transitionCoroutine = Transition(thirdPanel, thirdMiniGame);
        StartCoroutine(transitionCoroutine);
        isThirdGameActive=true;
    }

    public void TransitionToEnd()
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine); // Stop any ongoing transition
        }
        transitionCoroutine = Transition(fourthPanel, scorePanel);
        StartCoroutine(transitionCoroutine);
    }

    public bool isGamePaused = false;

    // Coroutine to handle the transition effect
    private IEnumerator Transition(GameObject transitionPanel, GameObject nextMiniGame)
    {
        PauseGame();

        // Deactivate the current mini-game
        if (currentMiniGame != null)
        {
            currentMiniGame.SetActive(false);
        }

        transitionPanel.SetActive(true); // Show the transition panel
        yield return new WaitForSecondsRealtime(transitionDuration); // Wait for the transition duration
        transitionPanel.SetActive(false); // Hide the transition panel

        // Start the next mini-game
        nextMiniGame.SetActive(true);
        currentMiniGame = nextMiniGame; // Update the current mini-game reference

        // Countdown before starting the next mini-game
        // Countdown before starting the next mini-game
        int countdownDuration = 3; // Set the initial countdown duration
        while (countdownDuration > 0)
        {
            Debug.Log("Countdown: " + countdownDuration);
            countdownText.text = countdownDuration.ToString(); // Update the UI text with the remaining time

            // Check if the game is paused
            if (!isGamePaused)
            {
                yield return new WaitForSecondsRealtime(1f); // Wait for one second only if the game is not paused
                countdownDuration--;
            }
            else
            {
                // If the game is paused, wait until it's unpaused
                while (isGamePaused)
                {
                    yield return null;
                }
            }
        }


        // Reset the countdown text
        countdownText.text = "";

        ResumeGame();
    }

    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
    }

}
