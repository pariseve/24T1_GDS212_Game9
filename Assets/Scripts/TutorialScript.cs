using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [Header("Tutorial UI")]
    public GameObject Tutorial1;
    public GameObject Tutorial2;
    public GameObject Tutorial3;

    private GameObject currentUI; // Variable to hold the reference to the currently active tutorial UI

    private GameTransitions transitions;

    private void Awake()
    {
        transitions = FindObjectOfType<GameTransitions>();
    }

    private void Update()
    {
        if (transitions.isFirstGameActive)
        {
            FirstTutorial();
        }
        else if (transitions.isSecondGameActive)
        {
            SecondTutorial();
        }
        else if (transitions.isThirdGameActive)
        {
            ThirdTutorial();
        }
    }

    public void FirstTutorial()
    {
            Tutorial1.gameObject.SetActive(true);
            Debug.Log("Tutorial 1 is active");
            currentUI = Tutorial1;
            PauseGame();
    }

    public void SecondTutorial()
    {
        Debug.Log("Starting second tutorial...");
        Tutorial2.gameObject.SetActive(true);
        currentUI = Tutorial2;
        PauseGame();
    }

    public void ThirdTutorial()
    {
        Debug.Log("Starting third tutorial...");
        Tutorial3.gameObject.SetActive(true);
        currentUI = Tutorial3;
        PauseGame();
    }

    public void DeactivateTutorialUI()
    {
        Debug.Log("Deactivating tutorial UI...");
        if (currentUI != null)
        {
            currentUI.SetActive(false); // Deactivate the current tutorial UI
            if (currentUI == Tutorial1)
            {
                transitions.isFirstGameActive = false; // Set isFirstGameActive to false
            }
            else if (currentUI == Tutorial2)
            {
                transitions.isSecondGameActive = false; // Set isSecondGameActive to false
            }
            else if (currentUI == Tutorial3)
            {
                transitions.isThirdGameActive = false; // Set isThirdGameActive to false
            }
            ResumeGame();
        }
    }


    private void PauseGame()
    {
        transitions.isGamePaused = true;
        Debug.Log("Pausing game...");
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        transitions.isGamePaused = false;
        Debug.Log("Resuming game...");
        Time.timeScale = 1f;
    }
}
