using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static CanvasUIEffects;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CanvasUIEffects : MonoBehaviour
{
    private Canvas canvas;

    static CanvasUIEffects instance;

    public void Awake()
    {
        instance = this;
    }

    // Feature #3, Task ID #8
    public class ObjectToScale
    {
        public RectTransform rectTransform { get; set; }
        public float speed { get; set; }
        public bool isMaxSize { get; set; }
        public float maxScale { get; set; }
    }

    // Feature #4, Task ID #17
    public class ParentObjectToMoveFromPointToPoint
    {
        public RectTransform rectTransform { get; }
        public float moveSpeed { get; }
        public List<Vector2> targetPositions { get; }
        public Vector2 currentPosition { get; private set; }
        public int currentIndex { get; set; }

        public ParentObjectToMoveFromPointToPoint(RectTransform rectTransform, float moveSpeed, List<Vector2> targetPositions)
        {
            this.rectTransform = rectTransform;
            this.moveSpeed = moveSpeed;
            this.targetPositions = targetPositions;
            this.currentPosition = targetPositions[0];
            this.currentIndex = 0;

        }

        // Feature #4, Task ID #18
        public void MoveToNextPoint()
        {
            if (currentIndex < targetPositions.Count)
            {
                Vector2 nextPosition = targetPositions[currentIndex];
                if (rectTransform != null)
                {
                    Vector2 direction = (nextPosition - (Vector2)rectTransform.localPosition).normalized;
                    float distance = Vector2.Distance(rectTransform.localPosition, nextPosition);

                    if (distance > 1f)
                    {
                        rectTransform.localPosition += (Vector3)direction * moveSpeed * Time.deltaTime;
                    }
                    else
                    {
                        currentIndex++;
                        rectTransform.localPosition = nextPosition;
                    }
                }
            }
            else
            {
                currentIndex = targetPositions.Count - 1;
            }
        }

    }
    // Feature #4, Task ID #16
    public class ObjectToMoveInCircle
    {
        public RectTransform rectTransform { get; set; }
        public float angle { get; set; }
        public float radius { get; set; }
        public float rotationSpeed { get; set; }
        public Vector2 targetPosition { get; }
        public float timer { get; set; }

        public bool isDone = false;
        public bool isScalable { get; set; }
        public float maxScale { get; set; }
        public float scaleSpeed { get; set; }

        public ObjectToMoveInCircle(RectTransform rectTransform, float radius, float rotationSpeed, float timer, bool isScalable, float maxScale, float scaleSpeed)
        {
            this.rectTransform = rectTransform;
            this.angle = 0f;
            this.radius = radius;
            this.rotationSpeed = rotationSpeed;
            this.timer = timer;
            this.isScalable = isScalable;
            this.maxScale = maxScale;
            this.scaleSpeed = scaleSpeed;
        }
    }

    // Feature #7, task #42
    public class PulseEffect
    {
        public RectTransform rectTransform { get; set; }
        public float pulseSpeed { get; set; }
        public float minScale { get; set; }
        public float maxScale { get; set; }
        private bool increasing = true;

        public PulseEffect(RectTransform rectTransform, float pulseSpeed, float minScale, float maxScale)
        {
            this.rectTransform = rectTransform;
            this.pulseSpeed = pulseSpeed;
            this.minScale = minScale;
            this.maxScale = maxScale;
        }

        public void UpdatePulse()
        {
            float scale = rectTransform.localScale.x;

            if (increasing)
            {
                scale += pulseSpeed * Time.deltaTime;
                if (scale >= maxScale)
                {
                    scale = maxScale;
                    increasing = false;
                }
            }
            else
            {
                scale -= pulseSpeed * Time.deltaTime;
                if (scale <= minScale)
                {
                    scale = minScale;
                    increasing = true;
                }
            }

            rectTransform.localScale = new Vector3(scale, scale, scale);
        }
    }

    // Feature #7, task #45
    public class WobbleEffect
    {
        public RectTransform rectTransform { get; set; }
        public float wobbleSpeed { get; set; }
        public float wobbleAmount { get; set; }
        private float wobbleTime = 0f;

        public WobbleEffect(RectTransform rectTransform, float wobbleSpeed, float wobbleAmount)
        {
            this.rectTransform = rectTransform;
            this.wobbleSpeed = wobbleSpeed;
            this.wobbleAmount = wobbleAmount;
        }

        public void UpdateWobble()
        {
            wobbleTime += Time.deltaTime * wobbleSpeed;

            float wobbleX = Mathf.Sin(wobbleTime) * wobbleAmount;
            float wobbleY = Mathf.Cos(wobbleTime * 0.8f) * wobbleAmount; 

            rectTransform.localEulerAngles = new Vector3(wobbleX, wobbleY, rectTransform.localEulerAngles.z);
        }
    }

    // Feature #7, task #46
    public class HeightIncreaseEffect
    {
        public RectTransform rectTransform { get; set; }
        public float heightIncreaseSpeed { get; set; }
        public float targetHeight { get; set; }
        public string canvasName { get; set; }
        public string nameString { get; set; }
        public float nameSize { get; set; }
        public Vector2 namePos { get; set; }
        public string scoreString { get; set; }
        public float scoreSize { get; set; }
        public Vector2 scorePos { get; set; }
        public Color textColour { get; set; }



        public bool isDone = false;

        public bool hasTextStarted = false;

        public HeightIncreaseEffect(RectTransform rectTransform, float heightIncreaseSpeed, float targetHeight, string canvasName, 
                                    string nameString, float nameSize, Vector2 namePos, string scoreString, float scoreSize, Vector2 scorePos, Color textColour )
        {
            this.rectTransform = rectTransform;
            this.heightIncreaseSpeed = heightIncreaseSpeed;
            this.targetHeight = targetHeight;
            this.canvasName = canvasName;
            this.nameString = nameString;
            this.nameSize = nameSize;
            this.namePos = namePos;
            this.scoreString = scoreString;
            this.scoreSize = scoreSize;
            this.scorePos = scorePos;
            this.textColour = textColour;

        }

        public void UpdateHeight()
        {
            float currentHeight = rectTransform.sizeDelta.y;

            if (currentHeight < targetHeight)
            {
                float progress = currentHeight / targetHeight;

                float effectiveHeightIncreaseSpeed = heightIncreaseSpeed;

                if (progress > 0.75f)
                {
                    effectiveHeightIncreaseSpeed *= 0.5f;
                }

                float newHeight = Mathf.Min(currentHeight + effectiveHeightIncreaseSpeed * Time.deltaTime, targetHeight);

                Vector2 sizeDelta = rectTransform.sizeDelta;
                sizeDelta.y = newHeight;
                rectTransform.sizeDelta = sizeDelta;
            }
            else
            {
                isDone = true;
            }
        }
    }


    public List<ObjectToScale> objectsToScale = new List<ObjectToScale>();
    public List<ParentObjectToMoveFromPointToPoint> parentObjectsToMoveFromPointToPoint = new List<ParentObjectToMoveFromPointToPoint>();
    public List<ObjectToMoveInCircle> objectsToMoveInCircle = new List<ObjectToMoveInCircle>();
    public List<PulseEffect> pulseEffects = new List<PulseEffect>();
    public List<WobbleEffect> wobbleEffects = new List<WobbleEffect>();
    public List<HeightIncreaseEffect> heightIncreaseEffects = new List<HeightIncreaseEffect>();

    public void Update()
    {
        UpdateObjectsToScale();
        UpdateParentObjectsToMove();
        MoveObjectsInCircle();
        UpdatePulseEffects();
        UpdateWobbleEffects();
        UpdateHeightIncreaseEffects();

        for (int i = 0; i < parentObjectsToMoveFromPointToPoint.Count; i++)
        {
            if (parentObjectsToMoveFromPointToPoint[i] == null) { continue; }

            if (parentObjectsToMoveFromPointToPoint[i].currentIndex == parentObjectsToMoveFromPointToPoint[i].targetPositions.Count)
            {
                ReduceRadiusOfMovingObject(i);

                if (objectsToMoveInCircle[i].isScalable)
                {
                    StartDeletionOfMovingObjectTimerWithIncreasingObjectSize(i);
                }
                else
                {
                    StartDeletionOfMovingObjectTimer(i);
                }

                ReducingTransparencyOfMovingObject(i);

            }
        }

        for (int i = 0; i < heightIncreaseEffects.Count; i++)
        {
            if (heightIncreaseEffects[i] == null) { continue; }

            if (heightIncreaseEffects[i].isDone && !heightIncreaseEffects[i].hasTextStarted)
            {
                heightIncreaseEffects[i].hasTextStarted = true;

                CreateScalableText(heightIncreaseEffects[i].canvasName, "PlayerName", heightIncreaseEffects[i].nameString, "",
                                    heightIncreaseEffects[i].nameSize, heightIncreaseEffects[i].textColour, heightIncreaseEffects[i].namePos,
                                    5, 1, 0.5f, null);

                CreateScalableText(heightIncreaseEffects[i].canvasName, "PlayerScore", heightIncreaseEffects[i].scoreString, "",
                                    heightIncreaseEffects[i].scoreSize, heightIncreaseEffects[i].textColour, heightIncreaseEffects[i].scorePos,
                                    5, 1, 0.5f, null);
            }
        }
    }

    // Feature #1, Task ID #39
    public static CanvasUIEffects Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CanvasUIEffects>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("CanvasUIEffectsManager");
                    instance = singletonObject.AddComponent<CanvasUIEffects>();
                }
            }
            return instance;
        }
    }

    // Creates a new EndgameScreen canvas if one doesnt already exist
    // Feature #1, Task ID #5
    public void CreateCanvas(string canvasName)
    {
        Canvas[] existingCanvases = GameObject.FindObjectsOfType<Canvas>();

        foreach (Canvas existingCanvas in existingCanvases)
        {
            if (existingCanvas.gameObject.name == canvasName)
            {
                canvas = existingCanvas;
                return;
            }
        }

        canvas = new GameObject(canvasName).AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }

    // Feature #1, Task ID #6
    public Canvas GetCanvas(string canvasName)
    {
        Canvas[] existingCanvases = GameObject.FindObjectsOfType<Canvas>();
        foreach (Canvas existingCanvas in existingCanvases)
        {
            if (existingCanvas.gameObject.name == canvasName)
            {
                return existingCanvas;
            }
        }

        return null;
    }

    // Creates a background / rectangle on the canvas
    // Feature #2, Task ID #10
    public void CreateRectangleOnCanvas(string canvasName, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition, Vector2 sizeDelta, Color colour, Transform parent)
    {
        CreateCanvas(canvasName);

        GameObject backgroundRect = new GameObject("BackgroundRectangle");

        if (parent != null)
        {
            backgroundRect.transform.SetParent(parent, false);
        }
        else
        {
            if (GetCanvas(canvasName) != null)
            {
                backgroundRect.transform.SetParent(GetCanvas(canvasName).transform, false);
            }
        }

        Image newRect = backgroundRect.AddComponent<Image>();

        newRect.color = FixRGBColour(colour);

        RectTransform rectTransform = newRect.GetComponent<RectTransform>();
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.anchoredPosition = anchoredPosition;

        if (sizeDelta == Vector2.zero)
        {
            Canvas canvas = GetCanvas(canvasName);

            if (canvas != null)
            {
                rectTransform.sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta;
            }
            else
            {
                rectTransform.sizeDelta = Vector2.zero; 
            }
        }
        else
        {
            rectTransform.sizeDelta = sizeDelta;
        }
    }

    // Creates a blank image on the canvas
    // Feature #3, Task ID #24
    public void CreateImageOnCanvas(string canvasName, Vector2 size, Vector2 startPos, float maxScale, float scaleSpeed, Color colour, Transform parent)
    {
        CreateCanvas(canvasName);

        GameObject backgroundImage = new GameObject("Image");
        backgroundImage.transform.SetParent(parent, false);
        Image newImage = backgroundImage.AddComponent<Image>();

        newImage.color = FixRGBColour(colour);

        if(parent != null)
        {
            backgroundImage.transform.SetParent(parent, false);
        }
        else
        {
            if (GetCanvas(canvasName) != null)
            {
                backgroundImage.transform.SetParent(GetCanvas(canvasName).transform, false);
            }
        }


        RectTransform rectTransform = newImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = size;
        rectTransform.localPosition = startPos;
      
        CreateObjectToScale(maxScale, scaleSpeed, rectTransform);
    }

    // Creates an image with a texture on the canvas
    // Feature #3, Task ID #25
    public void CreateImageWithTextureOnCanvas(string canvasName, string spriteName, Vector2 size, Vector2 startPos, float maxScale, float scaleSpeed, Transform parent)
    {
        CreateCanvas(canvasName);

        GameObject backgroundImage = new GameObject(spriteName + "Image");
        Image newImage = backgroundImage.AddComponent<Image>();
        Sprite loadedSprite = Resources.Load<Sprite>(spriteName);

        newImage.sprite = loadedSprite;

        if (parent != null)
        {
            backgroundImage.transform.SetParent(parent, false);
        }
        else
        {
            if (GetCanvas(canvasName) != null)
            {
                backgroundImage.transform.SetParent(GetCanvas(canvasName).transform, false);
            }
        }
        RectTransform rectTransform = newImage.GetComponent<RectTransform>();

        rectTransform.sizeDelta = size;
        rectTransform.localPosition = startPos;

        CreateObjectToScale(maxScale, scaleSpeed, rectTransform);
    }

    // Creates text that scales on the canvas
    // Feature #1, Task ID #15
    public void CreateScalableText(string canvasName, string textName, string textString, string textFont,
        float textSize, Color colour, Vector2 startPos, float targetScale, float scaleSpeed, float xPivot, Transform parent)
    {
        CreateCanvas(canvasName);

        GameObject textObject = new GameObject(textName);
        TextMeshProUGUI textMeshProUGUI = textObject.AddComponent<TextMeshProUGUI>();
        TMP_FontAsset loadedFont = Resources.Load<TMP_FontAsset>(textFont);


        textMeshProUGUI.text = textString;
        textMeshProUGUI.fontSize = textSize;
        textMeshProUGUI.color = FixRGBColour(colour);
        textMeshProUGUI.fontStyle = FontStyles.Bold;
        textMeshProUGUI.alignment = TextAlignmentOptions.Left;

        if(loadedFont != null)
        {
            textMeshProUGUI.font = loadedFont;
        }


        if (parent != null)
        {
            textObject.transform.SetParent(parent, false);
        }
        else
        {
            if (GetCanvas(canvasName) != null)
            {
                textObject.transform.SetParent(GetCanvas(canvasName).transform, false);
            }
        }

        RectTransform textRectTransform = textMeshProUGUI.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(textMeshProUGUI.preferredWidth, textSize);
        textRectTransform.localPosition = new Vector2(startPos.x, startPos.y);
        textRectTransform.pivot = new Vector2(xPivot, textRectTransform.pivot.y);

        CreateObjectToScale(targetScale, scaleSpeed, textRectTransform);
    }

    // Creates object that scales on the canvas
    // Feature #1, Task ID #11
    public void CreateObjectToScale(float maxScale, float speed, RectTransform rectTransform)
    {
        ObjectToScale objectToScale = new ObjectToScale();
        objectToScale.isMaxSize = false;
        objectToScale.maxScale = maxScale;
        objectToScale.speed = speed;
        objectToScale.rectTransform = rectTransform;
        objectsToScale.Add(objectToScale);
    }

    // Function for increasing object size
    // Feature #1, Task ID #12
    void UpdateObjectsToScale()
    {
        if (objectsToScale.Count > 0)
        {
            for (int i = 0; i < objectsToScale.Count; i++)
            {
                if (objectsToScale[i] == null || objectsToScale[i].rectTransform == null || objectsToScale[i].rectTransform.gameObject == null)
                {
                    continue;
                }

                float currentScale = objectsToScale[i].rectTransform.localScale.x;
                float targetScale = objectsToScale[i].maxScale;

                if (objectsToScale[i].isMaxSize)
                {
                    continue;
                }

                if (targetScale < 1.0f)
                {
                    if (currentScale <= targetScale)
                    {
                        objectsToScale[i].isMaxSize = true;
                        objectsToScale[i].rectTransform.localScale = new Vector3(targetScale, targetScale, 1f);
                    }
                    else
                    {
                        objectsToScale[i].rectTransform.localScale -= Vector3.one * Time.deltaTime * objectsToScale[i].speed;
                    }
                }
                else
                {
                    if (currentScale >= targetScale)
                    {
                        objectsToScale[i].isMaxSize = true;
                        objectsToScale[i].rectTransform.localScale = new Vector3(targetScale, targetScale, 1f);
                    }
                    else
                    {
                        objectsToScale[i].rectTransform.localScale += Vector3.one * Time.deltaTime * objectsToScale[i].speed;
                    }
                }
            }
        }
    }


    //
    // Moving Object
    //

    // Creates object that moves in a circle around a set point
    // Feature #4, Task ID #20
    public void CreateMovingObject(string canvasName, Vector2 size, float radius, float rotationSpeed, float moveSpeed, List<Vector2> list, string spriteName, float deleteTimer, Transform parent, 
                                    bool scaleObject, float maxScale, float scaleSpeed)
    {
        CreateCanvas(canvasName);

        GameObject parentObj = new GameObject("MovingParent");
        RectTransform parentRectTransform = parentObj.AddComponent<RectTransform>();
        if (parent != null)
        {
            parentObj.transform.SetParent(parent, false);
        }
        else
        {
            if (GetCanvas(canvasName) != null)
            {
                parentObj.transform.SetParent(GetCanvas(canvasName).transform, false);
            }
        }

        ParentObjectToMoveFromPointToPoint parentObjectToMove = new ParentObjectToMoveFromPointToPoint(parentRectTransform, moveSpeed, list);
        parentObjectToMove.rectTransform.localPosition = parentObjectToMove.currentPosition;
        parentObjectsToMoveFromPointToPoint.Add(parentObjectToMove);

        GameObject movingImage = new GameObject("MovingObject");
        movingImage.transform.SetParent(parentObj.transform, false);

        Image newImage = movingImage.AddComponent<Image>();
        RectTransform rectTransform = newImage.rectTransform;
        rectTransform.sizeDelta = size;
        rectTransform.localPosition = list[0];

        Sprite loadedSprite = Resources.Load<Sprite>(spriteName);
        newImage.sprite = loadedSprite;

        ObjectToMoveInCircle objectToMove = new ObjectToMoveInCircle(rectTransform, radius, rotationSpeed, deleteTimer, scaleObject, maxScale, scaleSpeed);
        objectsToMoveInCircle.Add(objectToMove);
    }

    // Creates object that moves in a circle around a set point with text
    // Feature #4, Task ID #21
    public void CreateMovingObjectWithText(string canvasName, Vector2 size, float radius, float rotationSpeed, float moveSpeed, List<Vector2> list, 
        string spriteName, string textString, float textSize, string textFont, Color textColor, float deleteTimer, Transform parent, bool scaleObject, float maxScale, float scaleSpeed)
    {
        CreateCanvas(canvasName);

        GameObject parentObj = new GameObject("MovingParent");
        RectTransform parentRectTransform = parentObj.AddComponent<RectTransform>();
        TMP_FontAsset loadedFont = Resources.Load<TMP_FontAsset>(textFont);


        if (parent != null)
        {
            parentObj.transform.SetParent(parent, false);
        }
        else
        {
            if (GetCanvas(canvasName) != null)
            {
                parentObj.transform.SetParent(GetCanvas(canvasName).transform, false);
            }
        }

        ParentObjectToMoveFromPointToPoint parentObjectToMove = new ParentObjectToMoveFromPointToPoint(parentRectTransform, moveSpeed, list);
        parentObjectToMove.rectTransform.localPosition = parentObjectToMove.currentPosition;
        parentObjectsToMoveFromPointToPoint.Add(parentObjectToMove);

        GameObject movingImage = new GameObject("MovingObject");
        movingImage.transform.SetParent(parentObj.transform, false);

        Image newImage = movingImage.AddComponent<Image>();
        RectTransform rectTransform = newImage.rectTransform;
        rectTransform.sizeDelta = size;
        rectTransform.localPosition = list[0];

        Sprite loadedSprite = Resources.Load<Sprite>(spriteName);
        newImage.sprite = loadedSprite;

        GameObject textObject = new GameObject("Text");
        TextMeshProUGUI textMeshProUGUI = textObject.AddComponent<TextMeshProUGUI>();
        textObject.transform.SetParent(movingImage.transform, false);

        textMeshProUGUI.text = textString;
        textMeshProUGUI.fontSize = textSize;
        textMeshProUGUI.color = FixRGBColour(textColor);
        textMeshProUGUI.fontStyle = FontStyles.Bold;
        textMeshProUGUI.alignment = TextAlignmentOptions.Center;

        if (loadedFont != null)
        {
            textMeshProUGUI.font = loadedFont;
        }

        RectTransform textRectTransform = textMeshProUGUI.rectTransform;
        textRectTransform.anchorMin = Vector2.zero;
        textRectTransform.anchorMax = Vector2.one;
        textRectTransform.sizeDelta = Vector2.zero;
        textRectTransform.anchoredPosition = Vector2.zero;

        ObjectToMoveInCircle objectToMove = new ObjectToMoveInCircle(rectTransform, radius, rotationSpeed, deleteTimer, scaleObject, maxScale, scaleSpeed);
        objectsToMoveInCircle.Add(objectToMove);
    }

    // Creates text that goes with the moving object
    // Feature #4, Task ID #22


    public void CreateMovingText(string canvasName, string textName, string textString, int textSize, string textFont, Color colour,
                             float radius, float rotationSpeed, float moveSpeed, List<Vector2> movingPositions,
                             float deleteTimer, Transform parent, bool scaleObject, float maxScale, float scaleSpeed)
    {
        CreateCanvas(canvasName);

        GameObject parentObj = new GameObject("MovingParent");
        RectTransform parentRectTransform = parentObj.AddComponent<RectTransform>();

        TMP_FontAsset loadedFont = Resources.Load<TMP_FontAsset>(textFont);

        if (parent != null)
        {
            parentObj.transform.SetParent(parent, false);
        }
        else
        {
            if (GetCanvas(canvasName) != null)
            {
                parentObj.transform.SetParent(GetCanvas(canvasName).transform, false);
            }
        }

        ParentObjectToMoveFromPointToPoint parentObjectToMove = new ParentObjectToMoveFromPointToPoint(parentRectTransform, moveSpeed, movingPositions);
        parentObjectToMove.rectTransform.localPosition = parentObjectToMove.currentPosition;

        parentObjectsToMoveFromPointToPoint.Add(parentObjectToMove);

        GameObject textObject = new GameObject(textName);
        RectTransform textRectTransform = textObject.AddComponent<RectTransform>();
        TextMeshProUGUI textMeshProUGUI = textObject.AddComponent<TextMeshProUGUI>();
        textObject.transform.SetParent(parentObj.transform, false);

        textMeshProUGUI.text = textString;
        textMeshProUGUI.fontSize = textSize;
        textMeshProUGUI.color = FixRGBColour(colour);
        textMeshProUGUI.fontStyle = FontStyles.Bold;
        textMeshProUGUI.alignment = TextAlignmentOptions.Center;

        if (loadedFont != null)
        {
            textMeshProUGUI.font = loadedFont;
        }

        textRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        textRectTransform.pivot = new Vector2(0.5f, 0.5f);
        textRectTransform.sizeDelta = new Vector2(textMeshProUGUI.preferredWidth, textSize);
        textRectTransform.anchoredPosition = Vector2.zero;

        ObjectToMoveInCircle objectToMove = new ObjectToMoveInCircle(textRectTransform, radius, rotationSpeed, deleteTimer, scaleObject, maxScale, scaleSpeed);
        objectsToMoveInCircle.Add(objectToMove);
    }


    // Function for moving the object in a circle
    // Feature #4, Task ID #23
    void MoveObjectsInCircle()
    {
        foreach (ObjectToMoveInCircle obj in objectsToMoveInCircle)
        {
            if (obj == null || obj.rectTransform == null || obj.rectTransform.gameObject == null)
            {
                continue;
            }

            float x = Mathf.Cos(obj.angle) * obj.radius;
            float y = Mathf.Sin(obj.angle) * obj.radius;
            Vector2 newPos = new Vector2(x, y);

            obj.rectTransform.localPosition = newPos;

            obj.angle += Time.deltaTime * obj.rotationSpeed;
        }
    }

    // Feature #4, Task ID #19
    void UpdateParentObjectsToMove()
    {
        foreach (ParentObjectToMoveFromPointToPoint parentObject in parentObjectsToMoveFromPointToPoint)
        {
            if (parentObject == null || parentObject.rectTransform == null || parentObject.rectTransform.gameObject == null)
            {
                continue;
            }

            parentObject.MoveToNextPoint();
            
        }
    }

    // Feature #4, Task ID #26
    void ReduceRadiusOfMovingObject(int index)
    {
        if (objectsToMoveInCircle[index].radius > 0)
        {
            objectsToMoveInCircle[index].radius -= 0.1f;
        }
    }

    // Feature #4, Task ID #27
    void StartDeletionOfMovingObjectTimer(int index)
    {
        if (objectsToMoveInCircle[index].radius <= 0 && !objectsToMoveInCircle[index].isDone)
        {
            objectsToMoveInCircle[index].isDone = true;
            StartCoroutine(DeleteMovingImage(objectsToMoveInCircle[index].timer));
        }
    }

    // Feature #4, Task ID #28
    void StartDeletionOfMovingObjectTimerWithIncreasingObjectSize(int index)
    {
        if (objectsToMoveInCircle[index].radius <= 0 && !objectsToMoveInCircle[index].isDone)
        {
            objectsToMoveInCircle[index].isDone = true;
            CreateObjectToScale(objectsToMoveInCircle[index].maxScale, objectsToMoveInCircle[index].scaleSpeed, objectsToMoveInCircle[index].rectTransform);
            StartCoroutine(DeleteMovingImage(objectsToMoveInCircle[index].timer));
        }
    }

    // Feature #4, Task ID #29
    void ReducingTransparencyOfMovingObject(int index)
    {
        if (objectsToMoveInCircle[index].isDone)
        {
            float alphaDecreaseRate = 1 / objectsToMoveInCircle[index].timer;
            Image image = objectsToMoveInCircle[index].rectTransform.GetComponent<Image>();
            TextMeshProUGUI text = objectsToMoveInCircle[index].rectTransform.GetComponentInChildren<TextMeshProUGUI>();

            if (image != null)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (alphaDecreaseRate * 2) * Time.deltaTime);
            }

            if (text != null)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (alphaDecreaseRate * 2) * Time.deltaTime);
            }
        }
    }

    // Feature #4, Task ID #30
    IEnumerator DeleteMovingImage(float timer)
    {
        yield return new WaitForSeconds(timer);

        for (int i = 0; i < objectsToMoveInCircle.Count; i++)
        {
            if (objectsToMoveInCircle[i].isDone)
            {
                Destroy(parentObjectsToMoveFromPointToPoint[i].rectTransform.gameObject);
                parentObjectsToMoveFromPointToPoint.Remove(parentObjectsToMoveFromPointToPoint[i]);
                objectsToMoveInCircle.Remove(objectsToMoveInCircle[i]);
            }
        }
    }

    public Color FixRGBColour(Color colourToFix)
    {
        float r = colourToFix.r > 1 ? colourToFix.r / 255f : colourToFix.r;
        float g = colourToFix.g > 1 ? colourToFix.g / 255f : colourToFix.g;
        float b = colourToFix.b > 1 ? colourToFix.b / 255f : colourToFix.b;

        return new Color(r, g, b);
    }

    // Creates entire end game screen
    public void CreateLeaderboard(string canvasName,
        Color backgroundColour,
        string titleTextString,
        float titleTextSize,
        float titleTextYOffset,
        Color playerBackgroundColour,
        Vector2 playerBackgroundSize,
        Vector2 topOfLeaderBoardPosition,
        float spaceInBetweenPlayerBackgrounds,
        float playerBackgroundMaxScale,
        float scaleSpeed,
        float nameTextOffsetX,
        float scoreTextOffsetX,
        float textSize,
        float textMaxScale,
        string textFont,
        Color textColour,
        Dictionary<string, string> playerInfo,
        Vector2 movingObjectSize,
        float movingObjectRadius,
        float movingObjectRotationSpeed,
        float movingObjectMoveSpeed,
        List<Vector2> movingObjectPositions,
        string movingObjectSpriteTextureString,
        string movingObjectTextString,
        float movingObjectTextSize,
        Color movingObjectTextColour,
        float movingObjectDeletionTimer,
        bool canMovingObjectScale,
        float movingObjectMaxScale,
        float movingObjectScaleSpeed,
        string pulsatingImageTextureName,
        Vector2 pulsatingImageStartPos,
        Vector2 pulsatingImageSize,
        float pulsatingImageMinScale,
        float pulsatingImageMaxScale,
        float pulsatingImagePulseSpeed,
        string pulsatingTextString,
        float pulsatingTextSize,
        Color pulsatingTextColor,
        Vector2 pulsatingTextStartPos,
        float pulsatingTextMinScale,
        float pulsatingTextMaxScale,
        float pulsatingTextSpeed,
        string wobblingImageTexture,
        Vector2 wobblingImageStartPos,
        Vector2 wobblingImageSize,
        float wobblingImageWobbleSpeed,
        float wobblingImageWobbleAmount)
    {

        string[] playerNames = playerInfo.Keys.ToArray();
        string[] playerScores = playerInfo.Values.ToArray();

        CreateCanvas(canvasName);

        GameObject leaderboard = new GameObject("Leaderboard");

        if (GetCanvas(canvasName).transform != null)
        {
            leaderboard.transform.SetParent(GetCanvas(canvasName).transform, false);
        }

        // Feature #5, Task ID #32
        CreateRectangleOnCanvas(canvasName, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero, FixRGBColour(backgroundColour), leaderboard.transform);

        CreateScalableText(canvasName,
                "Title",
                titleTextString,
                textFont,
                titleTextSize,
                FixRGBColour(textColour),
                new Vector2(0, topOfLeaderBoardPosition.y + ((playerBackgroundSize.y * playerBackgroundMaxScale) * titleTextYOffset)),
                textMaxScale,
                scaleSpeed * 1.5f,
                0.5f,
                leaderboard.transform);

        Vector2 currentPos = topOfLeaderBoardPosition;

        for (int i = 0; i < playerInfo.Count; i++)
        {
            float scaleFactor = 1f - (i * 0.10f);

            Vector2 newPlayerBackgroundSize = new Vector2(playerBackgroundSize.x * scaleFactor, playerBackgroundSize.y * scaleFactor);

            float textScaleFactor = 1f - (i * 0.15f);
            float newTextSize = textSize * textScaleFactor;


            if (i > 0)
            {
                currentPos.y -= (newPlayerBackgroundSize.y * playerBackgroundMaxScale) + spaceInBetweenPlayerBackgrounds;
            }

            // Feature #5, Task ID #33
            CreateImageOnCanvas(canvasName, 
                newPlayerBackgroundSize,
                currentPos,
                playerBackgroundMaxScale,
                scaleSpeed,
                FixRGBColour(playerBackgroundColour),
                leaderboard.transform);

            float nameTextIndentation = (newPlayerBackgroundSize.x * playerBackgroundMaxScale) / 2.0f;
            float leftSideX = nameTextIndentation * -1;
            float nameTextX = leftSideX + (newPlayerBackgroundSize.x * playerBackgroundMaxScale * 0.05f);

            // Feature #5, Task ID #34
            CreateScalableText(canvasName,
                "Player" + (i + 1).ToString() + "Name",
                playerNames[i],
                textFont,
                newTextSize,
                FixRGBColour(textColour),
                new Vector2(nameTextX + nameTextOffsetX, currentPos.y),
                textMaxScale,
                scaleSpeed * 1.5f,
                0,
                leaderboard.transform);

            float rightSideX = currentPos.x + (newPlayerBackgroundSize.x * playerBackgroundMaxScale);
            float scoreTextX = rightSideX - (newPlayerBackgroundSize.x * playerBackgroundMaxScale * 0.20f);
            scoreTextX *= 0.5f;

            // Feature #5, Task ID #35
            CreateScalableText(canvasName,
                "Player" + (i + 1).ToString() + "Score",
                    playerScores[i],
                    textFont,
                    newTextSize,
                    FixRGBColour(textColour),
                    new Vector2(scoreTextX + scoreTextOffsetX, currentPos.y),
                    textMaxScale,
                    scaleSpeed * 1.5f,
                    0.5f,
                    leaderboard.transform);
        }

        // Feature #5, Task ID #36
        CreateMovingObjectWithText(canvasName, movingObjectSize, movingObjectRadius, movingObjectRotationSpeed, movingObjectMoveSpeed, movingObjectPositions, 
                                    movingObjectSpriteTextureString,movingObjectTextString, movingObjectTextSize, textFont, FixRGBColour(movingObjectTextColour), movingObjectDeletionTimer, 
                                    leaderboard.transform, canMovingObjectScale, movingObjectMaxScale, movingObjectScaleSpeed);

        CreateWobbleEffectImage(canvasName, "WobblingImage", wobblingImageTexture, wobblingImageStartPos, wobblingImageSize,
                                wobblingImageWobbleSpeed, wobblingImageWobbleAmount, leaderboard.transform);

        CreatePulseEffectImage(canvasName, "PulsingImage", pulsatingImageTextureName, pulsatingImageStartPos, pulsatingImageSize,
                               pulsatingImageMinScale, pulsatingImageMaxScale, pulsatingImagePulseSpeed, leaderboard.transform);

        CreatePulseEffectTextObject(canvasName, "PulsingText", pulsatingTextString, textFont, pulsatingTextSize, FixRGBColour(pulsatingTextColor),
                                      pulsatingTextStartPos, pulsatingTextMinScale, pulsatingTextMaxScale, pulsatingTextSpeed, leaderboard.transform);
    }

    // Feature #7, task #42
    public void AddPulseEffect(RectTransform rectTransform, float pulseSpeed, float minScale, float maxScale)
    {
        PulseEffect pulseEffect = new PulseEffect(rectTransform, pulseSpeed, minScale, maxScale);
        pulseEffects.Add(pulseEffect);
    }

    // Feature #7, task #42
    public void UpdatePulseEffects()
    {
        foreach (PulseEffect pulseEffect in pulseEffects)
        {
            if (pulseEffect == null || pulseEffect.rectTransform == null || pulseEffect.rectTransform.gameObject == null)
            {
                continue;
            }
            pulseEffect.UpdatePulse();
        }
    }

    // Feature #7, task #42
    public void CreatePulseEffectTextObject(string canvasName, string textName, string textString, string textFont, 
        float textSize, Color colour, Vector2 startPos, float minScale, float maxScale, float pulseSpeed, Transform parent)
    {
        CreateCanvas(canvasName);

        GameObject textObject = new GameObject(textName);

        TextMeshProUGUI textMeshProUGUI = textObject.AddComponent<TextMeshProUGUI>();
        TMP_FontAsset loadedFont = Resources.Load<TMP_FontAsset>(textFont);

        textMeshProUGUI.text = textString;
        textMeshProUGUI.fontSize = textSize;
        textMeshProUGUI.color = FixRGBColour(colour);
        textMeshProUGUI.fontStyle = FontStyles.Bold;
        textMeshProUGUI.alignment = TextAlignmentOptions.Left;

        if (loadedFont != null)
        {
            textMeshProUGUI.font = loadedFont;
        }

        if (parent != null)
        {
            textObject.transform.SetParent(parent, false);
        }
        else
        {
            if (GetCanvas(canvasName) != null)
            {
                textObject.transform.SetParent(GetCanvas(canvasName).transform, false);
            }
        }

        RectTransform textRectTransform = textMeshProUGUI.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(textMeshProUGUI.preferredWidth, textSize);
        textRectTransform.localPosition = new Vector2(startPos.x, startPos.y);

        AddPulseEffect(textRectTransform, pulseSpeed, minScale, maxScale);
    }

    // Feature #7, task #42
    public void CreatePulseEffectImage(string canvasName, string imageName, string textureName, Vector2 startPos, Vector2 size, float minScale, float maxScale, float pulseSpeed, Transform parent)
    {
        CreateCanvas(canvasName);

        GameObject imageObject = new GameObject(imageName);

        Image image = imageObject.AddComponent<Image>();

        Texture2D texture = Resources.Load<Texture2D>(textureName);
        if (texture == null)
        {
            Debug.LogError("Texture not found: " + textureName);
            return;
        }

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        image.sprite = sprite;

        if (parent != null)
        {
            imageObject.transform.SetParent(parent, false);
        }
        else
        {
            Canvas canvas = GetCanvas(canvasName);
            if (canvas != null)
            {
                imageObject.transform.SetParent(canvas.transform, false);
            }
        }

        RectTransform imageRectTransform = image.GetComponent<RectTransform>();
        imageRectTransform.sizeDelta = size;
        imageRectTransform.localPosition = new Vector2(startPos.x, startPos.y);

        AddPulseEffect(imageRectTransform, pulseSpeed, minScale, maxScale);
    }

    // Feature #7, task #45
    public void AddWobbleEffect(RectTransform rectTransform, float wobbleSpeed, float wobbleAmount)
    {
        WobbleEffect wobbleEffect = new WobbleEffect(rectTransform, wobbleSpeed, wobbleAmount);
        wobbleEffects.Add(wobbleEffect);
    }

    // Feature #7, task #45
    public void UpdateWobbleEffects()
    {
        foreach (WobbleEffect wobbleEffect in wobbleEffects)
        {
            if (wobbleEffect == null || wobbleEffect.rectTransform == null || wobbleEffect.rectTransform.gameObject == null)
            {
                continue;
            }
            wobbleEffect.UpdateWobble();
        }
    }
    // Feature #7, task #45
    public void CreateWobbleEffectImage(string canvasName, string imageName, string textureName, Vector2 startPos, Vector2 size, float wobbleSpeed, float wobbleAmount, Transform parent)
    {
        CreateCanvas(canvasName);

        GameObject imageObject = new GameObject(imageName);

        Image image = imageObject.AddComponent<Image>();

        Texture2D texture = Resources.Load<Texture2D>(textureName);
        if (texture == null)
        {
            Debug.LogError("Texture not found: " + textureName);
            return;
        }

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        image.sprite = sprite;

        if (parent != null)
        {
            imageObject.transform.SetParent(parent, false);
        }
        else
        {
            Canvas canvas = GetCanvas(canvasName);
            if (canvas != null)
            {
                imageObject.transform.SetParent(canvas.transform, false);
            }
        }

        RectTransform imageRectTransform = image.GetComponent<RectTransform>();
        imageRectTransform.sizeDelta = size;
        imageRectTransform.localPosition = new Vector2(startPos.x, startPos.y);

        AddWobbleEffect(imageRectTransform, wobbleSpeed, wobbleAmount);
    }

    // Feature #7, task #46
    public void AddHeightIncreaseEffect(string canvasName, RectTransform rectTransform, float heightIncreaseSpeed, float targetHeight,
                                      string nameString, float nameSize, Vector2 namePos, string scoreString, float scoreSize, Vector2 scorePos, Color textColour)
    {
        HeightIncreaseEffect heightIncreaseEffect = new HeightIncreaseEffect(rectTransform, heightIncreaseSpeed, targetHeight, canvasName, 
                                                                               nameString, nameSize, namePos, scoreString, scoreSize, scorePos, textColour);
        heightIncreaseEffects.Add(heightIncreaseEffect);
    }

    // Feature #7, task #46
    public void UpdateHeightIncreaseEffects()
    {
        foreach (HeightIncreaseEffect heightIncreaseEffect in heightIncreaseEffects)
        {
            if (heightIncreaseEffect == null || heightIncreaseEffect.rectTransform == null || heightIncreaseEffect.rectTransform.gameObject == null)
            {
                continue;
            }
            heightIncreaseEffect.UpdateHeight();
        }
    }

    // Feature #7, task #46
    public void CreateHeightIncreaseEffectObject(string canvasName, string imageName, Vector2 startPos, Vector2 size, float heightIncreaseSpeed, float targetHeight, Color colour,
                                              string nameString, float nameSize, Vector2 namePos, string scoreString, float scoreSize, Vector2 scorePos, Color textColour, Transform parent)
    {
        CreateCanvas(canvasName);

        GameObject imageObject = new GameObject(imageName);

        Image image = imageObject.AddComponent<Image>();
        image.color = FixRGBColour(colour);

        if (parent != null)
        {
            imageObject.transform.SetParent(parent, false);
        }
        else
        {
            Canvas canvas = GetCanvas(canvasName);
            if (canvas != null)
            {
                imageObject.transform.SetParent(canvas.transform, false);
            }
        }

        RectTransform imageRectTransform = image.GetComponent<RectTransform>();
        imageRectTransform.sizeDelta = size;
        imageRectTransform.localPosition = new Vector2(startPos.x, startPos.y);
        imageRectTransform.pivot = new Vector2(imageRectTransform.pivot.x, 0);

        AddHeightIncreaseEffect(canvasName, imageRectTransform, heightIncreaseSpeed, targetHeight, nameString, nameSize, namePos, scoreString, scoreSize, scorePos, textColour);
    }

    // Feature #8, task #43
    public void CreatePodiumLeaderboard(string canvasName,
    Color backgroundColour,
    List<Color> playerBackgroundColours,
    float podiumWidth,
    Vector2 podiumBasePosition,
    float spaceBetweenPodiums,
    float podiumScaleMultiplyer,
    float podiumScaleSpeed,
    float nameTextOffsetY,
    float scoreTextOffsetY,
    float textSize,
    string textFont,
    Color textColour,
    Dictionary<string, string> playerInfo,
    Vector2 movingObjectSize,
    float movingObjectRadius,
    float movingObjectRotationSpeed,
    float movingObjectMoveSpeed,
    string movingObjectSpriteTextureString,
    string movingObjectTextString,
    float movingObjectTextSize,
    Color movingObjectTextColour,
    float movingObjectDeletionTimer,
    bool canMovingObjectScale,
    float movingObjectMaxScale,
    float movingObjectScaleSpeed,
    string pulsatingImageTextureName,
    Vector2 pulsatingImageSize,
    float pulsatingImageMinScale,
    float pulsatingImageMaxScale,
    float pulsatingImagePulseSpeed,
    string pulsatingTextString,
    float pulsatingTextSize,
    Color pulsatingTextColor,
    Vector2 pulsatingTextStartPos,
    float pulsatingTextMinScale,
    float pulsatingTextMaxScale,
    float pulsatingTextSpeed,
    string wobblingImageTexture,
    Vector2 wobblingImageStartPos,
    Vector2 wobblingImageSize,
    float wobblingImageWobbleSpeed,
    float wobblingImageWobbleAmount)
    {
        string[] playerNames = playerInfo.Keys.ToArray();
        string[] playerScores = playerInfo.Values.ToArray();

        CreateCanvas(canvasName);
        GameObject leaderboard = new GameObject("PodiumLeaderboard");

        if (GetCanvas(canvasName).transform != null)
        {
            leaderboard.transform.SetParent(GetCanvas(canvasName).transform, false);
        }

        CreateRectangleOnCanvas(canvasName, Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero, FixRGBColour(backgroundColour), leaderboard.transform);

        int numberOfPlayers = playerInfo.Count;

        Vector2[] podiumPositions = CalculatePodiumPositions(podiumBasePosition, numberOfPlayers, spaceBetweenPodiums);

        List<Vector2> newMovingObjectPositions = new List<Vector2>();

        for (int i = 0; i < numberOfPlayers; i++)
        {
            Vector2 podiumPosition = podiumPositions[i];

            int scoreValue = int.Parse(playerScores[i]);

            Vector2 nameTextPosition = new Vector2(podiumPosition.x, podiumPosition.y + podiumPosition.y * nameTextOffsetY);

            Vector2 scoreTextPosition = new Vector2(podiumPosition.x, podiumPosition.y + podiumPosition.y * scoreTextOffsetY);

            CreateHeightIncreaseEffectObject(canvasName, "Podium" + (i + 1).ToString(), podiumPosition, new Vector2(podiumWidth, 0),
                                    podiumScaleSpeed, scoreValue * podiumScaleMultiplyer, FixRGBColour(playerBackgroundColours[i]),
                                    playerNames[i], textSize, nameTextPosition, playerScores[i], textSize, scoreTextPosition, FixRGBColour(textColour), leaderboard.transform);


            newMovingObjectPositions.Add(new Vector2(podiumPositions[i].x, podiumPositions[i].y + (scoreValue * podiumScaleMultiplyer)));
        }

        CreateMovingObjectWithText(canvasName, movingObjectSize, movingObjectRadius, movingObjectRotationSpeed, movingObjectMoveSpeed, newMovingObjectPositions, movingObjectSpriteTextureString,
            movingObjectTextString, movingObjectTextSize, textFont, FixRGBColour(movingObjectTextColour), movingObjectDeletionTimer, leaderboard.transform, canMovingObjectScale, movingObjectMaxScale, movingObjectScaleSpeed);

        Vector2 newPulsatingImagePos = new Vector2(podiumPositions[0].x + (podiumWidth / 2),
                (podiumPositions[0].y + (podiumScaleMultiplyer * int.Parse(playerScores[0]))) - pulsatingImageSize.y * 0.1f);

        CreatePulseEffectImage(canvasName, "PulsingImage", pulsatingImageTextureName, newPulsatingImagePos, pulsatingImageSize,
                               pulsatingImageMinScale, pulsatingImageMaxScale, pulsatingImagePulseSpeed, leaderboard.transform);

        CreatePulseEffectTextObject(canvasName, "Podium Leaderboard Text", pulsatingTextString, textFont, pulsatingTextSize, FixRGBColour(pulsatingTextColor),
                                      pulsatingTextStartPos, pulsatingTextMinScale, pulsatingTextMaxScale, pulsatingTextSpeed, leaderboard.transform);

        CreateWobbleEffectImage(canvasName, "WobblingImage", wobblingImageTexture, wobblingImageStartPos, wobblingImageSize,
                                wobblingImageWobbleSpeed, wobblingImageWobbleAmount, leaderboard.transform);
    }

    Vector2[] CalculatePodiumPositions(Vector2 basePosition, int numberOfPlayers, float horizontalSpacing)
    {
        Vector2[] positions = new Vector2[numberOfPlayers];
        positions[0] = basePosition;

        if(numberOfPlayers % 2 == 0)
        {
            positions[0] = new Vector2(positions[0].x + (horizontalSpacing / 2.0f), positions[0].y);
        }

        for (int i = 1; i < numberOfPlayers; i++)
        {
            int direction = (i % 2 == 1) ? -1 : 1;
            int step = (i + 1) / 2;

            float xOffset = step * horizontalSpacing * direction;

            if (numberOfPlayers % 2 == 0)
            {
                xOffset += horizontalSpacing / 2;
            }

            positions[i] = basePosition + new Vector2(xOffset, 0);
        }

        return positions;
    }

    // Feature #1, Task ID #7
    public void DeleteEndScreenCanvas(string canvasName)
    {
        GameObject existingCanvas = GameObject.Find(canvasName);
        if (existingCanvas != null && existingCanvas.gameObject.name == canvasName)
        {
            Destroy(existingCanvas);

            objectsToScale.Clear();
            parentObjectsToMoveFromPointToPoint.Clear();
            objectsToMoveInCircle.Clear();
            pulseEffects.Clear();
            wobbleEffects.Clear();
            heightIncreaseEffects.Clear();
        }
    }
}
