using Codice.CM.Common;
using CodiceApp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

public class DemoScript : MonoBehaviour
{
    void Start()
    {

    }

    private void Update()
    {

    }

    public void CreateCanvas()
    {
        CanvasUIEffects.Instance.CreateCanvas();
    }

    public void CreateBackground()
    {
        CanvasUIEffects.Instance.CreateRectangleOnCanvas(Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero, Color.gray, null);
    }

    public void CreateScalingImage()
    {
        CanvasUIEffects.Instance.CreateImageOnCanvas(new Vector2(50, 50), new Vector2(0,0), 10, 1, Color.black, null);
    }

    public void CreateScalingImageWithTexture()
    {
        CanvasUIEffects.Instance.CreateImageWithTextureOnCanvas("player", new Vector2(100, 50), new Vector2(0, 0), 5, 3, null);
    }

    public void CreateScalingText()
    {
        CanvasUIEffects.Instance.CreateScalableText("Name", "Andy Chuggler", 3, Color.green, new Vector2(0,0), 20, 2, null);
    }
    public void CreateMovingImage()
    {
        List<Vector2> positionPoints = new List<Vector2>
        {
            new (400, -200),
            new (400, 200),
            new (200, 200),
            new (200, -200),
        };

        CanvasUIEffects.Instance.CreateMovingObject(new Vector2(60, 60), 20, 3, 70, positionPoints, "player", 5, null, false, 0, 0);
    }
    public void CreateMovingImageThatScales()
    {
        List<Vector2> positionPoints = new List<Vector2>
        {
            new (400, -200),
            new (400, 200),
            new (200, 200),
            new (200, -200),
        };

        CanvasUIEffects.Instance.CreateMovingObject(new Vector2(60, 60), 20, 3, 70, positionPoints, "player", 5, null, true, 5, 1);
    }
    public void CreateMovingImageWithText()
    {
        List<Vector2> positionPoints = new List<Vector2>
        {
            new (400, -200),
            new (400, 200),
            new (200, 200),
            new (200, -200),
        };

        CanvasUIEffects.Instance.CreateMovingObjectWithText(new Vector2(60, 60), 20, 3, 70, positionPoints, "player", "Big Ones", 10, Color.magenta, 5, null, false, 0, 0);
    }

    public void CreateLeaderboard()
    {
        Dictionary<string, string> playerInfo = new Dictionary<string, string>()
            {
                { "Charles", "13" },
                { "Bald Frog", "10" },
                { "Small Carl", "8" },
                { "King Fisher", "4" },
            };

        List<Vector2> playerPositions = new List<Vector2>
            {
                new (0, 100),
                new (0, 25),
                new (0, -50),
                new (0, -125)
            };

        List<Vector2> list = new List<Vector2>
            {
                new (500, 160),
                new (500, -160),
                new (-500, -160),
                new (-500, 160),
            };

        Vector2 size = new(50, 50);
        float radius = 30.0f;
        float rotationSpeed = 3.0f;
        float moveSpeed = 50.0f;

        CanvasUIEffects.Instance.CreateEndScreen(Color.red, Color.gray, new(45, 8), playerPositions, 5, 2, 0, 0, 3.5f, 7, Color.black, playerInfo,
                                                 size, radius, rotationSpeed, moveSpeed, list, "player", "Win", 11, Color.blue, 5, true, 3, 1);
    }

    public void DeleteCanvas()
    {
        CanvasUIEffects.Instance.DeleteEndScreenCanvas();
    }
}

