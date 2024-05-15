using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public float speed { get; set; }
        public bool isMaxSize { get; set; }
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

    //  Creates a new EndgameScreen canvas if one doesnt already exist
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
    public Canvas GetCanvas()
    {
        Canvas existingCanvas = GameObject.FindObjectOfType<Canvas>();
        if (existingCanvas != null && existingCanvas.gameObject.name == "EndscreenCanvas")
        {
           // Debug.LogWarning("EndscreenCanvas already exists in the scene");
            canvas = existingCanvas;
            return canvas;
        }
        else
        {
            return null;
        }
    }

    // Creates a background / rectangle on the canvas
    public void CreateRectangleOnCanvas(Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition, Vector2 sizeDelta, Color colour, Transform parent)
    {
        CreateCanvas();

        GameObject backgroundRect = new GameObject("BackgroundRectangle");

        if (parent != null)
        {
            backgroundRect.transform.SetParent(parent, false);
        }
        else
        {
            if (GetCanvas() != null)
            {
                backgroundRect.transform.SetParent(GetCanvas().transform, false);
            }
        }

        Image newRect = backgroundRect.AddComponent<Image>();

        newRect.color = colour;

        RectTransform rectTransform = newRect.GetComponent<RectTransform>();
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.anchoredPosition = anchoredPosition;

        if (sizeDelta == Vector2.zero)
        {
            Canvas canvas = GetCanvas();

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
    public void CreateImageOnCanvas(Vector2 size, Vector2 startPos, float maxScale, float scaleSpeed, Color colour, Transform parent)
    {
        CreateCanvas();

        GameObject backgroundImage = new GameObject("Image");
        backgroundImage.transform.SetParent(parent, false);
        Image newImage = backgroundImage.AddComponent<Image>();

        newImage.color = colour;

        if(parent != null)
        {
            backgroundImage.transform.SetParent(parent, false);
        }
        else
        {
            if (GetCanvas() != null)
            {
                backgroundImage.transform.SetParent(GetCanvas().transform, false);
            }
        }


        RectTransform rectTransform = newImage.GetComponent<RectTransform>();

        rectTransform.sizeDelta = size;
        rectTransform.localPosition = startPos;

        CreateObjectToScale(maxScale, scaleSpeed, rectTransform);
    }

    // Creates an image with a texture on the canvas
    public void CreateImageWithTextureOnCanvas(string spriteName, Vector2 size, Vector2 startPos, float maxScale, float scaleSpeed, Transform parent)
    {
        CreateCanvas();

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
            if (GetCanvas() != null)
            {
                backgroundImage.transform.SetParent(GetCanvas().transform, false);
            }
        }
        RectTransform rectTransform = newImage.GetComponent<RectTransform>();

        rectTransform.sizeDelta = size;
        rectTransform.localPosition = startPos;

        CreateObjectToScale(maxScale, scaleSpeed, rectTransform);
    }

    // Creates text that scales on the canvas
    public void CreateScalableText(string textName, string textString, float textSize, Color colour, Vector2 startPos, float targetScale, float scaleSpeed, Transform parent)
    {
        CreateCanvas();

        GameObject textObject = new GameObject(textName);
        TextMeshProUGUI textMeshProUGUI = textObject.AddComponent<TextMeshProUGUI>();

        textMeshProUGUI.text = textString;
        textMeshProUGUI.fontSize = textSize;
        textMeshProUGUI.color = colour;
        textMeshProUGUI.fontStyle = FontStyles.Bold;
        textMeshProUGUI.alignment = TextAlignmentOptions.Left;

        if (parent != null)
        {
            textObject.transform.SetParent(parent, false);
        }
        else
        {
            if (GetCanvas() != null)
            {
                textObject.transform.SetParent(GetCanvas().transform, false);
            }
        }

        RectTransform textRectTransform = textMeshProUGUI.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(textMeshProUGUI.preferredWidth, textSize);
        textRectTransform.localPosition = new Vector2(startPos.x, startPos.y);

        CreateObjectToScale(targetScale, scaleSpeed, textRectTransform);
    }

    // Creates object that scales on the canvas
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
    void IncreaseObjectSize()
    {
        if (objectsToScale.Count > 0)
        {
            for (int i = 0; i < objectsToScale.Count; i++)
            {
                if (objectsToScale[i].isMaxSize)
                {
                    continue;
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

    //
    // Moving Object
    //

    // Creates object that moves in a circle around a set point
    public void CreateMovingObject(Vector2 size, float radius, float rotationSpeed, float moveSpeed, List<Vector2> list, string spriteName, float deleteTimer, Transform parent, 
                                    bool scaleObject, float maxScale, float scaleSpeed)
    {
        CreateCanvas();

        GameObject parentObj = new GameObject("MovingParent");
        RectTransform parentRectTransform = parentObj.AddComponent<RectTransform>();
        if (parent != null)
        {
            parentObj.transform.SetParent(parent, false);
        }
        else
        {
            if (GetCanvas() != null)
            {
                parentObj.transform.SetParent(GetCanvas().transform, false);
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
    public void CreateMovingObjectWithText(Vector2 size, float radius, float rotationSpeed, float moveSpeed, List<Vector2> list, 
        string spriteName, string textString, float textSize, Color textColor, float deleteTimer, Transform parent, bool scaleObject, float maxScale, float scaleSpeed)
    {
        CreateCanvas();

        GameObject parentObj = new GameObject("MovingParent");
        RectTransform parentRectTransform = parentObj.AddComponent<RectTransform>();
        if (parent != null)
        {
            parentObj.transform.SetParent(parent, false);
        }
        else
        {
            if (GetCanvas() != null)
            {
                parentObj.transform.SetParent(GetCanvas().transform, false);
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
        textMeshProUGUI.color = textColor;
        textMeshProUGUI.fontStyle = FontStyles.Bold;
        textMeshProUGUI.alignment = TextAlignmentOptions.Center;

        RectTransform textRectTransform = textMeshProUGUI.rectTransform;
        textRectTransform.anchorMin = Vector2.zero;
        textRectTransform.anchorMax = Vector2.one;
        textRectTransform.sizeDelta = Vector2.zero;
        textRectTransform.anchoredPosition = Vector2.zero;

        ObjectToMoveInCircle objectToMove = new ObjectToMoveInCircle(rectTransform, radius, rotationSpeed, deleteTimer, scaleObject, maxScale, scaleSpeed);
        objectsToMoveInCircle.Add(objectToMove);
    }

    // Creates text that goes with the moving object
    public void CreateCircularMovingText(string textName, string textString, int textSize, Color colour, Vector2 startPos, float targetScale, float scaleSpeed, Transform parent)
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

        CreateObjectToScale(targetScale, scaleSpeed, textRectTransform);
    }

    // Function for moving the object in a circle
    void MoveObjectsInCircle()
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

    void MoveParentObjectsToNextPoint()
    {
        foreach (var parentObject in parentObjectsToMoveFromPointToPoint)
        {
            parentObject.MoveToNextPoint();
        }
    }

    //
    void ReduceRadiusOfMovingObject(int index)
    {
        if (objectsToMoveInCircle[index].radius > 0)
        {
            objectsToMoveInCircle[index].radius -= 0.1f;
        }
    }

    //
    void StartDeletionOfMovingObjectTimer(int index)
    {
        if (objectsToMoveInCircle[index].radius <= 0 && !objectsToMoveInCircle[index].isDone)
        {
            objectsToMoveInCircle[index].isDone = true;
            StartCoroutine(DeleteMovingImage(objectsToMoveInCircle[index].timer));
        }
    }
    //
    void StartDeletionOfMovingObjectTimerWithIncreasingObjectSize(int index)
    {
        if (objectsToMoveInCircle[index].radius <= 0 && !objectsToMoveInCircle[index].isDone)
        {
            objectsToMoveInCircle[index].isDone = true;
            CreateObjectToScale(objectsToMoveInCircle[index].maxScale, objectsToMoveInCircle[index].scaleSpeed, objectsToMoveInCircle[index].rectTransform);
            StartCoroutine(DeleteMovingImage(objectsToMoveInCircle[index].timer));
        }
    }
    
    //
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
    
    //
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

    //
    public void DeleteEndScreenCanvas()
    {
        GameObject existingCanvas = GameObject.Find("EndscreenCanvas");
        if (existingCanvas != null && existingCanvas.gameObject.name == "EndscreenCanvas")
        {
            Destroy(existingCanvas);
            objectsToScale.Clear();
            parentObjectsToMoveFromPointToPoint.Clear();
            objectsToMoveInCircle.Clear();
        }
    }

    // Creates entire end game screen
    public void CreateEndScreen(Color backgroundColour,
        Color playerBackgroundColour,
        Vector2 playerBackgroundSize,
        List<Vector2> playerPositions,
        float playerBackgroundMaxScale,
        float scaleSpeed,
        float nameTextOffsetX,
        float scoreTextOffsetX,
        float textSize,
        float textMaxScale,
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
        float movingObjectScaleSpeed)

    {

        string[] playerNames = playerInfo.Keys.ToArray();
        string[] playerscores = playerInfo.Values.ToArray();


        CreateCanvas();

        GameObject leaderboard = new GameObject("Leaderboard");

        if (GetCanvas().transform != null)
        {
            leaderboard.transform.SetParent(GetCanvas().transform, false);
        }

        CreateRectangleOnCanvas(Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero, backgroundColour, leaderboard.transform);

        for (int i = 0; i < playerInfo.Count; i++)
        {
            Vector2 newPlayerBackgroundSize = new Vector2(playerBackgroundSize.x - (i * 1.5f), playerBackgroundSize.y - (i * 1.2f));
            float textScaleFactor = textSize - (i * 0.3f);

            CreateImageOnCanvas(newPlayerBackgroundSize,
                playerPositions[i],
                playerBackgroundMaxScale,
                scaleSpeed,
                playerBackgroundColour,
                leaderboard.transform);
            CreateScalableText("Player" + i.ToString() + "Name",
                playerNames[i],
                textScaleFactor,
                textColour,
                new Vector2(playerPositions[i].x + nameTextOffsetX, playerPositions[i].y),
                textMaxScale,
                scaleSpeed * 1.5f,
                leaderboard.transform);

            CreateScalableText("Player" + i.ToString() + "Score",
                playerscores[i],
                textScaleFactor,
                textColour,
                new Vector2(80 + scoreTextOffsetX, playerPositions[i].y),
                textMaxScale,
                scaleSpeed * 1.5f,
                leaderboard.transform);
        }

        CreateMovingObjectWithText(movingObjectSize, movingObjectRadius, movingObjectRotationSpeed, movingObjectMoveSpeed, movingObjectPositions, movingObjectSpriteTextureString,
                                    movingObjectTextString, movingObjectTextSize, movingObjectTextColour, movingObjectDeletionTimer, 
                                    leaderboard.transform, canMovingObjectScale, movingObjectMaxScale, movingObjectScaleSpeed);
    }
}
