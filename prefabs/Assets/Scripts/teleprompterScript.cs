using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teleprompter : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Use TextMeshProUGUI for UI elements
    public string filePath = "teleprompterText"; // Path to the text file in Resources
    public float scrollSpeed = 100f; // Speed for scrolling
    public float startDelay = 7f; // Delay before starting scrolling

    private RectTransform textRectTransform;
    private bool isScrolling = false;
    private float contentHeight;
    private string originalText;

    void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
            if (textMeshPro == null)
            {
                Debug.LogError("TextMeshProUGUI component not found!");
                return;
            }
        }

        textRectTransform = textMeshPro.GetComponent<RectTransform>();

        LoadTextFromFile();
        Invoke("StartScrolling", startDelay); // Start scrolling after delay
    }

    void LoadTextFromFile()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(filePath);
        if (textAsset != null)
        {
            originalText = textAsset.text;
            textMeshPro.text = originalText;
            StartCoroutine(InitializeText());
        }
        else
        {
            Debug.LogError("Text file not found! Make sure the file is in the Resources folder.");
        }
    }

    IEnumerator InitializeText()
    {
        // Wait for the text to update its size
        yield return new WaitForEndOfFrame();

        contentHeight = textMeshPro.preferredHeight;
        textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, contentHeight);
        
        // Duplicate the text to create a seamless scroll effect
        textMeshPro.text += "\n\n" + originalText;
    }

    void Update()
    {
        if (isScrolling)
        {
            ScrollText();
        }
    }

    void ScrollText()
    {
        // Calculate the new position
        Vector2 newPosition = textRectTransform.anchoredPosition;
        float deltaY = scrollSpeed * Time.deltaTime;
        newPosition.y += deltaY;

        // Log the details to debug
        Debug.Log("Scroll Speed: " + scrollSpeed);
        Debug.Log("Delta Y: " + deltaY);
        Debug.Log("New Position Y: " + newPosition.y);

        // Scroll and reset position for continuous looping effect
        if (newPosition.y >= contentHeight)
        {
            newPosition.y -= contentHeight;
        }

        textRectTransform.anchoredPosition = newPosition;
    }

    public void StartScrolling()
    {
        isScrolling = true;
    }

    public void StopScrolling()
    {
        isScrolling = false;
    }

    public void SetScrollSpeed(float speed)
    {
        scrollSpeed = speed;
        Debug.Log("Scroll Speed Set To: " + scrollSpeed);
    }
}
