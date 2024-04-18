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

    public float transitionDuration = 1f; // Duration of the transition in seconds
    public TextMeshProUGUI countdownText;

    private IEnumerator transitionCoroutine;

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
    }

    // Coroutine to handle the transition effect
    private IEnumerator Transition(GameObject transitionPanel, GameObject nextMiniGame)
    {
        PauseGame();
        transitionPanel.SetActive(true); // Show the transition panel
        yield return new WaitForSecondsRealtime(transitionDuration); // Wait for the transition duration

        // Hide the transition panel after transition duration
        transitionPanel.SetActive(false);

        // Countdown before starting the next mini-game
        int countdownDuration = 3;
        while (countdownDuration > 0)
        {
            Debug.Log("Countdown: " + countdownDuration);
            countdownText.text = countdownDuration.ToString(); // Update the UI text with the remaining time
            yield return new WaitForSecondsRealtime(1f); // Wait for one second
            countdownDuration--;
        }

        // Reset the countdown text
        countdownText.text = "";

        ResumeGame();

        // Start the next mini-game
        nextMiniGame.SetActive(true);
    }




    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}