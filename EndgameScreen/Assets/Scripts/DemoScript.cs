using Codice.CM.Common;
using CodiceApp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

public class DemoScript : MonoBehaviour
{

    GameObject buttonObject;

    private void Start()
    {
        buttonObject = GameObject.Find("BUTTONS");
    }

    public void CreateCanvas()
    {
        CanvasUIEffects.Instance.CreateCanvas("EndScreenCanvas");
    }


    public void CreateBackground()
    {
        Color backgroundColour = new Color(248f / 255f, 186f / 255f, 66f / 255f);
        CanvasUIEffects.Instance.CreateRectangleOnCanvas("EndScreenCanvas", Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero, backgroundColour, null);
    }

    public void CreateScalingImage()
    {
        CanvasUIEffects.Instance.CreateImageOnCanvas("EndScreenCanvas", new Vector2(50, 50), new Vector2(0,0), 10, 1, Color.black, null);
    }

    public void CreateScalingImageWithTexture()
    {
        CanvasUIEffects.Instance.CreateImageWithTextureOnCanvas("EndScreenCanvas", "player", new Vector2(100, 50), new Vector2(0, 0), 5, 3, null);
    }

    public void CreateScalingText()
    {
        CanvasUIEffects.Instance.CreateScalableText("EndScreenCanvas", "Name", "Andy Chuggler", 3, Color.green, new Vector2(0,0), 20, 2, null);
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

        CanvasUIEffects.Instance.CreateMovingObject("EndScreenCanvas", new Vector2(60, 60), 20, 3, 70, positionPoints, "player", 5, null, false, 0, 0);
    }
    public void CreateMovingImageThatScales()
    {
        List<Vector2> positionPoints = new List<Vector2>
        {
            new (-130, 130),
            new (0, 200),
            new (130, 130),
         
        };

        CanvasUIEffects.Instance.CreateMovingObject("EndScreenCanvas", new Vector2(60, 60), 20, 3, 70, positionPoints, "player", 5, null, true, 5, 1);
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

        CanvasUIEffects.Instance.CreateMovingObjectWithText("EndScreenCanvas", new Vector2(60, 60), 20, 3, 70, positionPoints, "player", "Big Ones", 10, Color.magenta, 5, null, false, 0, 0);
    }

    public void CreatePulsatingImage()
    {
        CanvasUIEffects.Instance.CreatePulseEffectImage("EndScreenCanvas", "PulsingImage", "crown", new Vector2(0,0), new Vector2(150, 150),
                       0.8f, 1.5f, 0.5f, null);
    }

    public void CreatePulsatingText()
    {
        CanvasUIEffects.Instance.CreatePulseEffectTextObject("EndScreenCanvas", "PulseText", "Pulse Time", 10, Color.blue,
                                      new Vector2(0, 200), 0.5f, 2.5f, 1, null);
    }

    public void CreateWobblyObject()
    {
        CanvasUIEffects.Instance.CreateWobbleEffectImage("EndScreenCanvas", "WobblingImage", "player", new Vector2(0,0), new Vector2(200, 200), 3, 10, null);
    }

    public void CreateLeaderboard()
    {
        //Dictionary<string, string> playerInfo = new Dictionary<string, string>()
        //{
        //    { "Charles", "13" },
        //    { "Bald Frog", "10" },
        //    { "Small Carl", "8" },
        //    { "King Fisher", "4" },
        //};

        //List<Vector2> movingObjectPositionPoints = new List<Vector2>
        //{
        //     new (-120, 115),
        //     new (0, 150),
        //     new (120, 115),
        //};

        //Vector2 movingObjectSize = new(50, 50);
        //float movingObjectRadius = 30.0f;
        //float movingObjectRotationSpeed = 3.0f;
        //float movingObjectMoveSpeed = 50.0f;

        //Color backgroundColour = new Color(248, 186, 66);
        //Color playerBackgroundColour = new Color(75, 72, 61);

        //string pulsatingImageTextureName = "crown";
        //Vector2 pulsatingImageStartPos = new Vector2(330, 105);
        //float pulsatingImageMinScale = 0.8f;
        //float pulsatingImageMaxScale = 1.2f;
        //float pulsatingImagePulseSpeed = 1.0f;
        //string pulsatingTextString = "Winner";
        //float pulsatingTextSize = 30.0f;
        //Color pulsatingTextColor = new Color(1f, 0f, 0f);
        //Vector2 pulsatingTextStartPos = new Vector2(300, 0);
        //float pulsatingTextMinScale = 0.8f;
        //float pulsatingTextMaxScale = 1.2f;
        //float pulsatingTextSpeed = 1.0f;
        //string wobblingImageTexture = "player";
        //Vector2 wobblingImageStartPos = new Vector2(300, 80);
        //Vector2 wobblingImageSize = new Vector2(100, 100);
        //float wobblingImageWobbleSpeed = 3.0f;
        //float wobblingImageWobbleAmount = 30.0f;

        //CanvasUIEffects.Instance.CreateLeaderboard("EndScreenCanvas", backgroundColour, "Leaderboard", 10, playerBackgroundColour, new Vector2(45, 8), new Vector2(0, 100),
        //                                           50, 5, 2, 0, 0, 3.5f, 7, Color.white, playerInfo,
        //                                           movingObjectSize, movingObjectRadius, movingObjectRotationSpeed, movingObjectMoveSpeed, movingObjectPositionPoints,
        //                                           "crown", "", 0, Color.black, 5, true, 2, 1,
        //                                           pulsatingImageTextureName, pulsatingImageStartPos, new Vector2(100, 100),
        //                                           pulsatingImageMinScale, pulsatingImageMaxScale, pulsatingImagePulseSpeed,
        //                                           pulsatingTextString, pulsatingTextSize, pulsatingTextColor, pulsatingTextStartPos,
        //                                           pulsatingTextMinScale, pulsatingTextMaxScale, pulsatingTextSpeed,
        //                                           wobblingImageTexture, wobblingImageStartPos, wobblingImageSize,
        //                                           wobblingImageWobbleSpeed, wobblingImageWobbleAmount);


        //// End Screen if using Simulator

        Vector2 playerBackgroundSize = new Vector2(100, 20);

        Vector2 topOfLeaderBoardPosition = new Vector2(0, 200);

        float spaceBetweenPlayerBackground = 100;

        float backgroundMaxScale = 10;
        float backgroundScaleSpeed = 3;

        float textSize = 7;
        float textMaxScale = 20;

        Dictionary<string, string> playerInfo = new Dictionary<string, string>()
            {
                { "Charles", "13" },
                { "Bald Frog", "10" },
                { "Small Carl", "8" },
                { "King Fisher", "4" },
            };

        Vector2 movingObjectSize = new Vector2(200, 200);


        float radius = 30.0f;
        float rotationSpeed = 3.0f;
        float movingObjectSpeed = 100.0f;

        List<Vector2> movingObjectPositionPoints = new List<Vector2>
            {
                new Vector2(450, 900),
                new Vector2(450, 400),
                new Vector2(-450, 400),
                new Vector2(-450, 900),
                new Vector2(0, 700),
            };

        string pulsatingImageTextureName = "crown";
        Vector2 pulsatingImageStartPos = new Vector2(330, 105);
        float pulsatingImageMinScale = 0.8f;
        float pulsatingImageMaxScale = 1.2f;
        float pulsatingImagePulseSpeed = 1.0f;
        string pulsatingTextString = "Winner";
        float pulsatingTextSize = 30.0f;
        Color pulsatingTextColor = new Color(1f, 0f, 0f);
        Vector2 pulsatingTextStartPos = new Vector2(300, 0);
        float pulsatingTextMinScale = 0.8f;
        float pulsatingTextMaxScale = 1.2f;
        float pulsatingTextSpeed = 1.0f;
        string wobblingImageTexture = "player";
        Vector2 wobblingImageStartPos = new Vector2(300, 80);
        Vector2 wobblingImageSize = new Vector2(100, 100);
        float wobblingImageWobbleSpeed = 3.0f;
        float wobblingImageWobbleAmount = 30.0f;

        CanvasUIEffects.Instance.CreateLeaderboard("EndScreenCanvas",  Color.red, "Leaderboard", 10, Color.gray, playerBackgroundSize, topOfLeaderBoardPosition, spaceBetweenPlayerBackground,
                                                 backgroundMaxScale, backgroundScaleSpeed, -50, 0, textSize, textMaxScale, Color.black, playerInfo,
                                                 movingObjectSize, radius, rotationSpeed, movingObjectSpeed,
                                                 movingObjectPositionPoints, "player", "Win", 50, Color.blue, 5, true, 7, 1,                    
                                                 pulsatingImageTextureName, pulsatingImageStartPos, new Vector2(100, 100),
                                                 pulsatingImageMinScale, pulsatingImageMaxScale, pulsatingImagePulseSpeed,
                                                 pulsatingTextString, pulsatingTextSize, pulsatingTextColor, pulsatingTextStartPos,
                                                 pulsatingTextMinScale, pulsatingTextMaxScale, pulsatingTextSpeed,
                                                 wobblingImageTexture, wobblingImageStartPos, wobblingImageSize,
                                                 wobblingImageWobbleSpeed, wobblingImageWobbleAmount);

    }

    public void CreatePodium()
    {

        //string canvasName = "EndScreenCanvas";
        //Color backgroundColour = new Color(0.2f, 0.2f, 0.2f);
        //List<Color> playerBackgroundColours = new List<Color>() { new Color(157, 232, 242), new Color(255, 215, 0), new Color(192, 192, 192), new Color(205, 127, 50) };
        //Vector2 playerBackgroundSize = new Vector2(50, 200);
        //Vector2 podiumBasePosition = new Vector2(0, -150);
        //float podiumHeightDifference = 100.0f;
        //float playerBackgroundMaxScale = 1.5f;
        //float scaleSpeed = 1.0f;
        //float nameTextOffsetX = 0;
        //float scoreTextOffsetX = 0;
        //float textSize = 10.0f;
        //float textMaxScale = 2.0f;
        //Color textColour = Color.white;
        //Dictionary<string, string> playerInfo = new Dictionary<string, string>();
        //playerInfo.Add("Andy", "100");
        //playerInfo.Add("Charles", "90");
        //playerInfo.Add("Muck", "80");
        //playerInfo.Add("Stud", "70");
        //Vector2 movingObjectSize = new Vector2(50, 50);
        //float movingObjectRadius = 10.0f;
        //float movingObjectRotationSpeed = 5.0f;
        //float movingObjectMoveSpeed = 100.0f;
        //string movingObjectSpriteTextureString = "movingObjectTexture";
        //string movingObjectTextString = "Loser";
        //float movingObjectTextSize = 10.0f;
        //Color movingObjectTextColour = Color.black;
        //float movingObjectDeletionTimer = 5.0f;
        //bool canMovingObjectScale = true;
        //float movingObjectMaxScale = 2.0f;
        //float movingObjectScaleSpeed = 1.0f;
        //string pulsatingImageTextureName = "crown";
        //Vector2 pulsatingImageStartPos = new Vector2(100, 100);
        //float pulsatingImageMinScale = 0.8f;
        //float pulsatingImageMaxScale = 1.2f;
        //float pulsatingImagePulseSpeed = 1.0f;
        //string pulsatingTextString = "Podium Leaderboard";
        //float pulsatingTextSize = 30.0f;
        //Color pulsatingTextColor = new Color(1f, 0f, 0f);
        //Vector2 pulsatingTextStartPos = new Vector2(0, 250);
        //float pulsatingTextMinScale = 0.8f;
        //float pulsatingTextMaxScale = 1.2f;
        //float pulsatingTextSpeed = 1.0f;
        //string wobblingImageTexture = "player";
        //Vector2 wobblingImageStartPos = new Vector2(300, 80);
        //Vector2 wobblingImageSize = new Vector2(100, 100);
        //float wobblingImageWobbleSpeed = 3.0f;
        //float wobblingImageWobbleAmount = 30.0f;

        //CanvasUIEffects.Instance.CreatePodiumLeaderboard(canvasName, backgroundColour, playerBackgroundColours,
        //                         playerBackgroundSize, podiumBasePosition, podiumHeightDifference,
        //                         playerBackgroundMaxScale, scaleSpeed,
        //                         nameTextOffsetX, scoreTextOffsetX, textSize, textMaxScale, textColour,
        //                         playerInfo, movingObjectSize, movingObjectRadius,
        //                         movingObjectRotationSpeed, movingObjectMoveSpeed,
        //                         movingObjectSpriteTextureString, movingObjectTextString, movingObjectTextSize,
        //                         movingObjectTextColour, movingObjectDeletionTimer,
        //                         canMovingObjectScale, movingObjectMaxScale,
        //                         movingObjectScaleSpeed, pulsatingImageTextureName,
        //                         pulsatingImageStartPos, pulsatingImageMinScale, pulsatingImageMaxScale,
        //                         pulsatingImagePulseSpeed, pulsatingTextString, pulsatingTextSize,
        //                         pulsatingTextColor, pulsatingTextStartPos, pulsatingTextMinScale,
        //                         pulsatingTextMaxScale, pulsatingTextSpeed, wobblingImageTexture,
        //                         wobblingImageStartPos, wobblingImageSize, wobblingImageWobbleSpeed,
        //                         wobblingImageWobbleAmount);

        string canvasName = "EndScreenCanvas";
        Color backgroundColour = new Color(0.2f, 0.2f, 0.2f);
        List<Color> playerBackgroundColours = new List<Color>() { new Color(157, 232, 242), new Color(255, 215, 0), new Color(192, 192, 192), new Color(205, 127, 50) };
        float podiumWidth = 200;
        Vector2 podiumBasePosition = new Vector2(0, -350);
        float podiumScaleMultiplyer = 10.0f;
        float podiumScaleSpeed = 50.0f;
        float nameTextOffsetX = 0;
        float scoreTextOffsetX = 0;
        float textSize = 10.0f;
        Color textColour = Color.white;
        Dictionary<string, string> playerInfo = new Dictionary<string, string>();
        playerInfo.Add("Andy", "100");
        playerInfo.Add("Charles", "90");
        playerInfo.Add("Muck", "80");
        playerInfo.Add("Stud", "70");
        Vector2 movingObjectSize = new Vector2(50, 50);
        float movingObjectRadius = 10.0f;
        float movingObjectRotationSpeed = 5.0f;
        float movingObjectMoveSpeed = 100.0f;
        string movingObjectSpriteTextureString = "movingObjectTexture";
        string movingObjectTextString = "Loser";
        float movingObjectTextSize = 10.0f;
        Color movingObjectTextColour = Color.black;
        float movingObjectDeletionTimer = 5.0f;
        bool canMovingObjectScale = true;
        float movingObjectMaxScale = 2.0f;
        float movingObjectScaleSpeed = 1.0f;
        string pulsatingImageTextureName = "crown";
        Vector2 pulsatingImageStartPos = new Vector2(100, 100);
        float pulsatingImageMinScale = 0.8f;
        float pulsatingImageMaxScale = 1.2f;
        float pulsatingImagePulseSpeed = 1.0f;
        string pulsatingTextString = "Podium Leaderboard";
        float pulsatingTextSize = 30.0f;
        Color pulsatingTextColor = new Color(1f, 0f, 0f);
        Vector2 pulsatingTextStartPos = new Vector2(0, 250);
        float pulsatingTextMinScale = 0.8f;
        float pulsatingTextMaxScale = 1.2f;
        float pulsatingTextSpeed = 1.0f;
        string wobblingImageTexture = "player";
        Vector2 wobblingImageStartPos = new Vector2(300, 80);
        Vector2 wobblingImageSize = new Vector2(100, 100);
        float wobblingImageWobbleSpeed = 3.0f;
        float wobblingImageWobbleAmount = 30.0f;

        CanvasUIEffects.Instance.CreatePodiumLeaderboard(canvasName, backgroundColour, playerBackgroundColours,
                                 podiumWidth, podiumBasePosition,
                                 podiumScaleMultiplyer, podiumScaleSpeed,
                                 nameTextOffsetX, scoreTextOffsetX, textSize, textColour,
                                 playerInfo, movingObjectSize, movingObjectRadius,
                                 movingObjectRotationSpeed, movingObjectMoveSpeed,
                                 movingObjectSpriteTextureString, movingObjectTextString, movingObjectTextSize,
                                 movingObjectTextColour, movingObjectDeletionTimer,
                                 canMovingObjectScale, movingObjectMaxScale,
                                 movingObjectScaleSpeed, pulsatingImageTextureName,
                                 pulsatingImageStartPos, pulsatingImageMinScale, pulsatingImageMaxScale,
                                 pulsatingImagePulseSpeed, pulsatingTextString, pulsatingTextSize,
                                 pulsatingTextColor, pulsatingTextStartPos, pulsatingTextMinScale,
                                 pulsatingTextMaxScale, pulsatingTextSpeed, wobblingImageTexture,
                                 wobblingImageStartPos, wobblingImageSize, wobblingImageWobbleSpeed,
                                 wobblingImageWobbleAmount);
    }

    public void ShowHideButtons()
    {
        if(buttonObject.activeSelf)
        {
            buttonObject.SetActive(false);
        }
        else
        {
            buttonObject.SetActive(true);
        }
    }
    public void DeleteCanvas()
    {
        CanvasUIEffects.Instance.DeleteEndScreenCanvas("EndScreenCanvas");
    }
}

