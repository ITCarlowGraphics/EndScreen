using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static CanvasUIEffects;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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
    public class ObjectToMoveInCircle
    {
        public RectTransform rectTransform { get; set; }
        public float angle { get; set; }
        public float radius { get; set; }
        public float rotationSpeed { get; set; }
        public Vector2 targetPosition { get; }
        public Vector2 currentPosition { get; set; }
        public float timer { get; set; }

        public bool isDone = false;

        public ObjectToMoveInCircle(RectTransform rectTransform, float radius, float rotationSpeed, Vector2 startPosition, float timer)
        {
            this.rectTransform = rectTransform;
            this.angle = 0f;
            this.radius = radius;
            this.rotationSpeed = rotationSpeed;
            this.currentPosition = startPosition;
            this.timer = timer;
        }
    }


    public List<ObjectToScale> objectsToScale = new List<ObjectToScale>();
    public List<ParentObjectToMoveFromPointToPoint> parentObjectsToMoveFromPointToPoint = new List<ParentObjectToMoveFromPointToPoint>();
    public List<ObjectToMoveInCircle> objectsToMoveInCircle = new List<ObjectToMoveInCircle>();

    public void Update()
    {
        IncreaseObjectSize();
        MoveParentObjectsToNextPoint();
        MoveObjectsInCircle();

        for (int i = 0; i < parentObjectsToMoveFromPointToPoint.Count; i++)
        {
            if (parentObjectsToMoveFromPointToPoint[i].currentIndex == parentObjectsToMoveFromPointToPoint[i].targetPositions.Count)
            {
                ReduceRadiusOfMovingObject(i);

                StartDeletionOfMovingObjectTimer(i);
               
                ReducingTransparencyOfMovingObject(i);

            }
        }
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

    /// <summary>
    ///  Creates a new EndgameScreen canvas if one doesnt already exist
    /// </summary>
    public void CreateCanvas()
    {
        Canvas existingCanvas = GameObject.FindObjectOfType<Canvas>();
        if (existingCanvas != null && existingCanvas.gameObject.name == "EndscreenCanvas")
        {
            Debug.LogWarning("EndscreenCanvas already exists in the scene");
            canvas = existingCanvas;
            return;
        }

        canvas = new GameObject("EndscreenCanvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }
    /// <summary>
    ///  Creates a background / rectangle on the canvas
    /// </summary>
    /// <param name="anchorMin"></param>
    /// <param name="anchorMax"></param>
    /// <param name="anchoredPosition"></param>
    /// <param name="sizeDelta"></param>
    /// <param name="colour"></param>
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

    /// <summary>
    ///  Creates a blank image on the canvas
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="startPos"></param>
    /// <param name="maxScale"></param>
    /// <param name="scaleSpeed"></param>
    /// <param name="colour"></param>
    public void CreateImageShapeOnCanvas(float width, float height, Vector2 startPos, float maxScale, float scaleSpeed, Color colour)
    {
        GameObject backgroundImage = new GameObject("Image");
        Image newImage = backgroundImage.AddComponent<Image>();

        newImage.color = colour;

        backgroundImage.transform.SetParent(canvas.transform, false);

        RectTransform rectTransform = newImage.GetComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(width, height);
        rectTransform.localPosition = startPos;

        CreateObjectToScale(new Vector2(width, height), maxScale, scaleSpeed, rectTransform);
    }

    /// <summary>
    ///  Creates an image with a texture on the canvas
    /// </summary>
    /// <param name="spriteName"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="startPos"></param>
    /// <param name="maxScale"></param>
    /// <param name="scaleSpeed"></param>
    public void CreateImageWithTextureOnCanvas(string spriteName, float width, float height, Vector2 startPos, float maxScale, float scaleSpeed)
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

    /// <summary>
    /// Creates text that scales on the canvas
    /// </summary>
    /// <param name="textName"></param>
    /// <param name="textString"></param>
    /// <param name="textSize"></param>
    /// <param name="colour"></param>
    /// <param name="startPos"></param>
    /// <param name="targetScale"></param>
    /// <param name="scaleSpeed"></param>
    public void CreateScalableText(string textName, string textString, int textSize, Color colour, Vector2 startPos, float targetScale, float scaleSpeed)
    {
        GameObject textObject = new GameObject(textName);
        TextMeshProUGUI textMeshProUGUI = textObject.AddComponent<TextMeshProUGUI>();

        textMeshProUGUI.text = textString;
        textMeshProUGUI.fontSize = textSize;
        textMeshProUGUI.color = colour;
        textMeshProUGUI.fontStyle = FontStyles.Bold;
        textMeshProUGUI.alignment = TextAlignmentOptions.Left;


        textObject.transform.SetParent(canvas.transform, false);

        RectTransform textRectTransform = textMeshProUGUI.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(textMeshProUGUI.preferredWidth, textSize);
        textRectTransform.localPosition = new Vector2(startPos.x, startPos.y);

        CreateObjectToScale(new Vector2(targetScale, targetScale), targetScale, scaleSpeed, textRectTransform);
    }

    /// <summary>
    /// Creates object that scales on the canvas
    /// </summary>
    /// <param name="targetSize"></param>
    /// <param name="maxScale"></param>
    /// <param name="speed"></param>
    /// <param name="rectTransform"></param>
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

    /// <summary>
    /// Function for increasing object size
    /// </summary>
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


    /// <summary>
    /// Creates entire end game screen
    /// </summary>
    /// <param name="backgroundColour"></param>
    /// <param name="buttonColour"></param>
    /// <param name="textMaxScale"></param>
    /// <param name="playerInfo"></param>
    public void CreateEndScreen(Color backgroundColour, Color buttonColour, int textMaxScale, Dictionary<string, string> playerInfo)
    {
        Vector2[] playerPositions = new Vector2[]
        {
            new Vector2(0, 75),
            new Vector2(0, 0),
            new Vector2(0, -75),
            new Vector2(0, -150)
        };

        string[] playerNames = playerInfo.Keys.ToArray();
        string[] playerscores = playerInfo.Values.ToArray();

        CreateCanvas();
        CreateRectangleOnCanvas(Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero, backgroundColour);

        for (int i = 0; i < playerInfo.Count; i++)
        {
            CreateImageShapeOnCanvas(45, 8, playerPositions[i], 5, 1, buttonColour);
            CreateScalableText("Player" + i.ToString() + "Name", playerNames[i], 3, Color.black, new Vector2(playerPositions[i].x, playerPositions[i].y), textMaxScale, 1.5f);
            CreateScalableText("Player" + i.ToString() + "Score", playerscores[i], 3, Color.black, new Vector2(80, playerPositions[i].y), textMaxScale, 1.5f);
        }
    }
    /// <summary>
    /// Creates object that moves in a circle around a set point
    /// </summary>
    /// <param name="size"></param>
    /// <param name="radius"></param>
    /// <param name="rotationSpeed"></param>
    /// <param name="moveSpeed"></param>
    /// <param name="startPosition"></param>
    /// <param name="list"></param>
    /// <param name="spriteName"></param>
    public void CreateMovingObject(Vector2 size, float radius, float rotationSpeed, float moveSpeed, Vector2 startPosition, List<Vector2> list, string spriteName, float deleteTimer)
    {
        CreateCanvas();

        GameObject parentObj = new GameObject("MovingParent");
        RectTransform parentRectTransform = parentObj.AddComponent<RectTransform>();
        parentObj.transform.SetParent(canvas.transform, false);

        ParentObjectToMoveFromPointToPoint parentObjectToMove = new ParentObjectToMoveFromPointToPoint(parentRectTransform, moveSpeed, list);
        parentObjectToMove.rectTransform.localPosition = parentObjectToMove.currentPosition;
        parentObjectsToMoveFromPointToPoint.Add(parentObjectToMove);

        GameObject movingImage = new GameObject("MovingImage");
        movingImage.transform.SetParent(parentObj.transform, false);

        Image newImage = movingImage.AddComponent<Image>();
        RectTransform rectTransform = newImage.rectTransform;
        rectTransform.sizeDelta = size;
        rectTransform.localPosition = list[0];

        Sprite loadedSprite = Resources.Load<Sprite>(spriteName);
        newImage.sprite = loadedSprite;

        ObjectToMoveInCircle objectToMove = new ObjectToMoveInCircle(rectTransform, radius, rotationSpeed, startPosition, deleteTimer);
        objectsToMoveInCircle.Add(objectToMove);
    }
    /// <summary>
    /// Creates object that moves in a circle around a set point with text
    /// </summary>
    /// <param name="size"></param>
    /// <param name="radius"></param>
    /// <param name="rotationSpeed"></param>
    /// <param name="moveSpeed"></param>
    /// <param name="startPosition"></param>
    /// <param name="list"></param>
    /// <param name="spriteName"></param>
    /// <param name="textString"></param>
    /// <param name="textSize"></param>
    /// <param name="textColor"></param>
    public void CreateMovingObjectWithText(Vector2 size, float radius, float rotationSpeed, float moveSpeed, Vector2 startPosition, List<Vector2> list, string spriteName, string textString, float textSize, Color textColor, float deleteTimer)
    {
        CreateCanvas();

        GameObject parentObj = new GameObject("MovingParent");
        RectTransform parentRectTransform = parentObj.AddComponent<RectTransform>();
        parentObj.transform.SetParent(canvas.transform, false);
      
        ParentObjectToMoveFromPointToPoint parentObjectToMove = new ParentObjectToMoveFromPointToPoint(parentRectTransform, moveSpeed, list);
        parentObjectToMove.rectTransform.localPosition = parentObjectToMove.currentPosition;
        parentObjectsToMoveFromPointToPoint.Add(parentObjectToMove);

        GameObject movingImage = new GameObject("MovingImage");
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
        textMeshProUGUI.color = textColor;
        textMeshProUGUI.fontStyle = FontStyles.Bold;
        textMeshProUGUI.alignment = TextAlignmentOptions.Center;

        RectTransform textRectTransform = textMeshProUGUI.rectTransform;
        textRectTransform.anchorMin = Vector2.zero;
        textRectTransform.anchorMax = Vector2.one;
        textRectTransform.sizeDelta = Vector2.zero;
        textRectTransform.anchoredPosition = Vector2.zero;

        ObjectToMoveInCircle objectToMove = new ObjectToMoveInCircle(rectTransform, radius, rotationSpeed, startPosition, deleteTimer);
        objectsToMoveInCircle.Add(objectToMove);
    }
    /// <summary>
    /// Function for moving the object in a circle
    /// </summary>
    public void MoveObjectsInCircle()
    {
        foreach (var obj in objectsToMoveInCircle)
        {
            float x = Mathf.Cos(obj.angle) * obj.radius;
            float y = Mathf.Sin(obj.angle) * obj.radius;
            Vector2 newPos = new Vector2(x, y);

            obj.rectTransform.localPosition = newPos;

            obj.angle += Time.deltaTime * obj.rotationSpeed;
        }
    }
    /// <summary>
    /// Creates text that goes with the moving object
    /// </summary>
    /// <param name="textName"></param>
    /// <param name="textString"></param>
    /// <param name="textSize"></param>
    /// <param name="colour"></param>
    /// <param name="startPos"></param>
    /// <param name="targetScale"></param>
    /// <param name="scaleSpeed"></param>
    public void CreateCircularMovingText(string textName, string textString, int textSize, Color colour, Vector2 startPos, float targetScale, float scaleSpeed)
    {
        GameObject textObject = new GameObject(textName);
        TextMeshProUGUI textMeshProUGUI = textObject.AddComponent<TextMeshProUGUI>();

        textMeshProUGUI.text = textString;
        textMeshProUGUI.fontSize = textSize;
        textMeshProUGUI.color = colour;
        textMeshProUGUI.fontStyle = FontStyles.Bold;
        textMeshProUGUI.alignment = TextAlignmentOptions.Left;


        textObject.transform.SetParent(canvas.transform, false);

        RectTransform textRectTransform = textMeshProUGUI.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(textMeshProUGUI.preferredWidth, textSize);
        textRectTransform.localPosition = new Vector2(startPos.x, startPos.y);

        CreateObjectToScale(new Vector2(targetScale, targetScale), targetScale, scaleSpeed, textRectTransform);
    }
    public void MoveParentObjectsToNextPoint()
    {
        foreach (var parentObject in parentObjectsToMoveFromPointToPoint)
        {
            parentObject.MoveToNextPoint();
        }
    }

    void ReduceRadiusOfMovingObject(int index)
    {
        if (objectsToMoveInCircle[index].radius > 0)
        {
            objectsToMoveInCircle[index].radius -= 0.1f;
        }
    }

    void StartDeletionOfMovingObjectTimer(int index)
    {
        if (objectsToMoveInCircle[index].radius <= 0 && !objectsToMoveInCircle[index].isDone)
        {
            objectsToMoveInCircle[index].isDone = true;
            StartCoroutine(DeleteMovingImage(objectsToMoveInCircle[index].timer));
        }
    }

    void ReducingTransparencyOfMovingObject(int index)
    {
        if (objectsToMoveInCircle[index].isDone)
        {
            float alphaDecreaseRate = 1 / objectsToMoveInCircle[index].timer;
            Image image = objectsToMoveInCircle[index].rectTransform.GetComponent<Image>();
            TextMeshProUGUI text = objectsToMoveInCircle[index].rectTransform.GetComponentInChildren<TextMeshProUGUI>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (alphaDecreaseRate * 2) * Time.deltaTime);

            if (text != null)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (alphaDecreaseRate * 2) * Time.deltaTime);
            }
        }
    }

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

}
