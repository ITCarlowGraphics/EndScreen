using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class UIManager : MonoBehaviour
{
    // Player UI
    private GameObject[] playerCards;
    private GameObject crown;
    private int playerAmount = 4; // Amount of players

    // Continue Button
    private GameObject continueButton;
    private float minScale = 0.8f;   // Minimum scale factor
    private float maxScale = 1.2f;   // Maximum scale factor
    public float duration = 1f;     // Time it takes to scale up or down
    private bool isScalingUp = true; // Determines the scaling direction

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        Initialize();
        StartCoroutine(ScaleUIElement(continueButton));
        StartCoroutine(AnimatePlayerCards());
    }

    private void Initialize()
    {
        // Player Cards
        playerCards = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject card in playerCards)
        {
            card.SetActive(false);
        }

        // Crown
        crown = GameObject.Find("Crown");
        crown.SetActive(false);

        // Continue Button
        continueButton = GameObject.Find("Continue Button");
    }

    public void showEndScreen()
    {
        gameObject.SetActive(true);
    }

    public void setPlayerAmount(int t_amount)
    {
        playerAmount = t_amount;
    }

    public void setPlayerStats(string t_name, int t_place, string t_General_Knowledge_Result, string t_English_Result, string t_Math_Result, string t_Science_Result, string t_Geography_Result, string t_History_Result)
    {
        GameObject playerCard = playerCards[3];
        if (t_place == 1)
        {
            playerCard = playerCards[3];
        }
        else if (t_place == 2)
        {
            playerCard = playerCards[2];
        }
        else if (t_place == 3)
        {
            playerCard = playerCards[1];
        }
        else if (t_place == 4)
        {
            playerCard = playerCards[0];
        }
        // Name Setting
        Transform childWithText = playerCard.transform.Find("Name Tag");
        TMP_Text name = childWithText.GetComponentInChildren<TMP_Text>();
        name.text = t_name;

        // General Knowledge
        childWithText = playerCard.transform.Find("Subject Result 1");
        TMP_Text[] subject = childWithText.GetComponentsInChildren<TMP_Text>();
        subject[1].text = t_General_Knowledge_Result;
        // General Knowledge
        childWithText = playerCard.transform.Find("Subject Result 2");
        subject = childWithText.GetComponentsInChildren<TMP_Text>();
        subject[1].text = t_English_Result;
        // General Knowledge
        childWithText = playerCard.transform.Find("Subject Result 3");
        subject = childWithText.GetComponentsInChildren<TMP_Text>();
        subject[1].text = t_Math_Result;
        // General Knowledge
        childWithText = playerCard.transform.Find("Subject Result 4");
        subject = childWithText.GetComponentsInChildren<TMP_Text>();
        subject[1].text = t_Science_Result;
        // General Knowledge
        childWithText = playerCard.transform.Find("Subject Result 5");
        subject = childWithText.GetComponentsInChildren<TMP_Text>();
        subject[1].text = t_Geography_Result;
        // General Knowledge
        childWithText = playerCard.transform.Find("Subject Result 6");
        subject = childWithText.GetComponentsInChildren<TMP_Text>();
        subject[1].text = t_History_Result;
    }

    IEnumerator ScaleUIElement(GameObject t_obj)
    {
        while (true)
        {
            float startScale = isScalingUp ? minScale : maxScale;
            float endScale = isScalingUp ? maxScale : minScale;

            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                float scale = Mathf.Lerp(startScale, endScale, t);
                t_obj.transform.localScale = new UnityEngine.Vector3(scale, scale, scale); ;
                yield return null;
            }

            // Toggle the scaling direction
            isScalingUp = !isScalingUp;
        }
    }

    IEnumerator AnimatePlayerCards()
    {
        int count = 4;
        foreach (GameObject card in playerCards)
        {
            if (count <= playerAmount)
            {
                UnityEngine.Vector3 originalPosition = card.transform.position;
                UnityEngine.Vector3 startingPosition = originalPosition;
                startingPosition.x -= 250;
                card.SetActive(true);

                // Move to the left
                card.transform.localPosition = startingPosition;

                // Move back to the original position
                yield return StartCoroutine(MoveObject(card, startingPosition, originalPosition, 1));
            }
            count--;
        }

        // Animate Crown
        UnityEngine.Vector3 crown_originalPosition = crown.transform.position;
        UnityEngine.Vector3 crown_startingPosition = crown_originalPosition;
        crown_startingPosition.x -= 400;
        crown.SetActive(true);
        StartCoroutine(MoveObject(crown, crown_startingPosition, crown_originalPosition, 1));
        StartCoroutine(ScaleUIElement(crown));
    }

    IEnumerator MoveObject(GameObject obj, UnityEngine.Vector3 start, UnityEngine.Vector3 end, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Smooth movement using Lerp
            obj.transform.position = UnityEngine.Vector3.Lerp(start, end, t);
            yield return null;
        }

        obj.transform.position = end; // Ensure it ends exactly at the target
    }
}
