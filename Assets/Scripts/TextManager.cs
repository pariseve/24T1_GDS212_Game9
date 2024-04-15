using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI perfectText;
    public TextMeshProUGUI niceText;
    public TextMeshProUGUI badText;

    public TextMeshProUGUI directionText;
    public TextMeshProUGUI wrongDirectionText;

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
        directionText.text = "Stir " + direction; 
    }
}
