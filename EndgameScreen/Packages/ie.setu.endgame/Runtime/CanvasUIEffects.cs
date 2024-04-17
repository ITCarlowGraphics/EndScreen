using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUIEffects : MonoBehaviour
{
    public Sprite backgroundSprite;
    private Canvas canvas;

    public static CanvasUIEffects instance;

    public void Awake()
    {
        instance = this;
    }

    public class ObjectToScale
    {
        public RectTransform rectTransform { get; set; }
        public Vector2 targetSize { get; set; }
        public float speed { get; set; }
        public bool isMaxSize { get; set; }
        public bool isFinalSize { get; set; }
        public float maxScale { get; set; } 
    }

    public List<ObjectToScale> objectsToScale = new List<ObjectToScale>();

    void Start()
    {
    }

    public void Update()
    {
        IncreaseObjectSize();
    }

    public static CanvasUIEffects Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CanvasUIEffects>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("CanvasManager");
                    instance = singletonObject.AddComponent<CanvasUIEffects>();
                }
            }
            return instance;
        }
    }

    public void CreateCanvas()
    {
        canvas = new GameObject("EndscreenCanvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }

    public void CreateRectangleOnCanvas(Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition, Vector2 sizeDelta, Color colour)
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

    public void CreateImageOnCanvas(string spriteName, float width, float height, Vector2 startPos, float maxScale, float scaleSpeed)
    {
        GameObject backgroundImage = new GameObject(spriteName + "Image");
        Image newImage = backgroundImage.AddComponent<Image>();
        Sprite loadedSprite = Resources.Load<Sprite>(spriteName);

        newImage.sprite = loadedSprite;

        backgroundImage.transform.SetParent(canvas.transform, false);

        RectTransform rectTransform = newImage.GetComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(width, height);
        rectTransform.localPosition = startPos;

        CreateObjectToScale(new Vector2(width, height), maxScale, scaleSpeed, rectTransform);
    }

    public void CreateText(string textName, string textString, int textSize, Color colour, Vector2 startPos, float targetScale, float scaleSpeed)
    {
        GameObject textObject = new GameObject(textName);
        TextMeshProUGUI textMeshProUGUI = textObject.AddComponent<TextMeshProUGUI>();

        textMeshProUGUI.text = textString;
        textMeshProUGUI.fontSize = textSize;
        textMeshProUGUI.color = colour;
        textMeshProUGUI.fontStyle = FontStyles.Bold;

        textObject.transform.SetParent(canvas.transform, false);

        RectTransform textRectTransform = textMeshProUGUI.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(textMeshProUGUI.preferredWidth, textSize);
        textRectTransform.localPosition = new Vector2(startPos.x, startPos.y);

        CreateObjectToScale(new Vector2(targetScale, targetScale), targetScale, scaleSpeed, textRectTransform);
    }

    public void CreateObjectToScale(Vector2 targetSize, float maxScale, float speed, RectTransform rectTransform)
    {
        ObjectToScale objectToScale = new ObjectToScale();
        objectToScale.isMaxSize = false;
        objectToScale.isFinalSize = false;
        objectToScale.targetSize = targetSize;
        objectToScale.maxScale = maxScale;
        objectToScale.speed = speed;
        objectToScale.rectTransform = rectTransform;
        objectsToScale.Add(objectToScale);
    }

    public void IncreaseObjectSize()
    {
        if (objectsToScale.Count > 0)
        {
            for (int i = 0; i < objectsToScale.Count; i++)
            {
                if (objectsToScale[i].isMaxSize)
                {
                    return;
                }

                if (objectsToScale[i].rectTransform.localScale.x >= objectsToScale[i].maxScale)
                {
                    objectsToScale[i].isMaxSize = true;
                    objectsToScale[i].rectTransform.localScale = new Vector3(objectsToScale[i].maxScale, objectsToScale[i].maxScale, 1f);
                }
                else
                {
                    objectsToScale[i].rectTransform.localScale += Vector3.one * Time.deltaTime * objectsToScale[i].speed;
                }
            }
        }
    }
}
