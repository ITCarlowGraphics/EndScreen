using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CanvasManager : MonoBehaviour
{
    public Sprite backgroundSprite; // Reference to your sprite, assign it in the Unity Editor
    private Canvas canvas;

    class ImageScalar : MonoBehaviour
    {
        public RectTransform rectTransform { get; set; }
        public Vector2 targetSize { get; set; }
        public float speed { get; set; }
        public bool done { get; set; }
    }

    List<ImageScalar> imagesToScale = new List<ImageScalar>();

    void Start()
    {

        CreateCanvas();
        CreateBackgroundImage();

        GameObject textObject = new GameObject("TextMeshProText");
        TextMeshProUGUI textMeshProUGUI = textObject.AddComponent<TextMeshProUGUI>();

        // Set text properties
        textMeshProUGUI.text = "Hello, TextMeshPro!";
        textMeshProUGUI.fontSize = 24;
        textMeshProUGUI.color = Color.white;

        // Set the parent of the text to the canvas
        textObject.transform.SetParent(canvas.transform, false);

        // Optionally, you can set the RectTransform properties for the text
        RectTransform textRectTransform = textMeshProUGUI.GetComponent<RectTransform>();
        textRectTransform.anchoredPosition = new Vector2(0.5f, 0.5f); // Center of the canvas
    }

    void CreateCanvas()
    {
        canvas = new GameObject("Canvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }

    void CreateBackgroundImage()
    {
        
        CreateRectangleOnCanvas(Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero, Color.red);
        CreateImageOnCanvas("Player", Vector2.one * 3f, 10f);
    }

    void CreateRectangleOnCanvas(Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition, Vector2 sizeDelta, Color colour)
    {
        GameObject backgroundRect = new GameObject("BackgroundRectangle");
        Image newRect = backgroundRect.AddComponent<Image>();

        newRect.color = colour;

        backgroundRect.transform.SetParent(canvas.transform, false);

        RectTransform rectTransform = newRect.GetComponent<RectTransform>();
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = sizeDelta;
        
    }
    void CreateImageOnCanvas(string spriteName, Vector2 targetScale, float speed)
    {
        GameObject backgroundImage = new GameObject(spriteName + "Image");
        Image newImage = backgroundImage.AddComponent<Image>();

        // Load the sprite by name
        Sprite loadedSprite = Resources.Load<Sprite>(spriteName);

        if (loadedSprite != null)
        {
            newImage.sprite = loadedSprite;
        }
        else
        {
            Debug.LogError("Sprite not found with name: " + spriteName);
        }

        backgroundImage.transform.SetParent(canvas.transform, false);

        RectTransform rectTransform = newImage.GetComponent<RectTransform>();
        rectTransform.localScale = Vector2.one;
        rectTransform.localPosition = Vector2.one;


        ImageScalar imgscalar = new ImageScalar();
        imgscalar.done = false;
        imgscalar.targetSize = targetScale;
        imgscalar.speed = speed;
        imgscalar.rectTransform = rectTransform;
        imagesToScale.Add(imgscalar);


    }

    void Update()
    {
        if (imagesToScale.Count > 0)
        {
            for (int i = 0; i < imagesToScale.Count; i++)
            {
                if (imagesToScale[i].done)
                {
                    return;
                }
                imagesToScale[i].rectTransform.localScale += Vector3.one * Time.deltaTime * imagesToScale[i].speed;
                if (imagesToScale[i].rectTransform.localScale.x > imagesToScale[i].targetSize.x)
                {
                    imagesToScale[i].done = true;
                    imagesToScale[i].rectTransform.localScale = imagesToScale[i].targetSize;
                }
            }
        }
    }
}
