using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [Header("Tutorial UI")]
    public GameObject Tutorial1;
    public GameObject Tutorial2;
    public GameObject Tutorial3;

    private GameObject currentUI; // Variable to hold the reference to the currently active tutorial UI

    public void FirstTutorial()
    {
        Tutorial1.gameObject.SetActive(true);
        currentUI = Tutorial1;
        PauseGame();
    }
    public void SecondTutorial()
    {
        Tutorial2.gameObject.SetActive(true);
        currentUI = Tutorial2;
        PauseGame();
    }
    public void ThirdTutorial()
    {
        Tutorial3.gameObject.SetActive(true);
        currentUI = Tutorial3;
        PauseGame();
    }

    public void DeactivateTutorialUI()
    {
        if (currentUI != null)
        {
            currentUI.SetActive(false); // Deactivate the current tutorial UI
            ResumeGame();
        }
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
