using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [Header("Tutorial UI")]
    public GameObject Tutorial1;
    public GameObject Tutorial2;
    public GameObject Tutorial3;

    private GameObject currentUI; // Variable to hold the reference to the currently active tutorial UI

    void Start()
    {
        // Set the initial currentUI based on whichever tutorial UI is active at the start
        if (Tutorial1.activeSelf)
        {
            currentUI = Tutorial1;
        }
        else if (Tutorial2.activeSelf)
        {
            currentUI = Tutorial2;
        }
        else if (Tutorial3.activeSelf)
        {
            currentUI = Tutorial3;
        }
    }

    public void DeactivateTutorialUI()
    {
        if (currentUI != null)
        {
            currentUI.SetActive(false); // Deactivate the current tutorial UI
        }
    }
}
