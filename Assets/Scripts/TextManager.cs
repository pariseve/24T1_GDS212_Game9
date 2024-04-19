using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    [Header("Outcome Text")]
    public TextMeshProUGUI perfectText;
    public TextMeshProUGUI niceText;
    public TextMeshProUGUI badText;
    [Header("Stirring Minigame")]
    public TextMeshProUGUI directionText;
    public TextMeshProUGUI wrongDirectionText;
    [Header("Score Text")]
    public TextMeshProUGUI finalScoreText;

    private void Awake()
    {
        perfectText.gameObject.SetActive(false);
        niceText.gameObject.SetActive(false);
        badText.gameObject.SetActive(false);
    }
    public void PerfectTextActive()
    {
        perfectText.gameObject.SetActive(true);
    }
    public void NiceTextActive()
    {
        niceText.gameObject.SetActive(true);
    }
    public void BadTextActive()
    {
        badText.gameObject.SetActive(true);
    }

    public void SetDirectionText(string direction)
    {
        directionText.gameObject.SetActive(true);
        directionText.text = "Stir " + direction; 
    }

    public void SetAllTextInactive()
    {
        perfectText.gameObject.SetActive(false);
        niceText.gameObject.SetActive(false);
        badText.gameObject.SetActive(false);
        directionText.gameObject.SetActive(false);
        wrongDirectionText.gameObject.SetActive(false);
    }

}
