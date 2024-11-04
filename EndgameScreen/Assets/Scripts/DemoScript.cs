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
        CanvasUIEffects.Instance.CreateScalableText("EndScreenCanvas", "Name", "Andy Chuggler", "rocketfont", 3, Color.green, new Vector2(0,0), 20, 2, 0.5f, null);
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

        CanvasUIEffects.Instance.CreateMovingObjectWithText("EndScreenCanvas", new Vector2(60, 60), 20, 3, 70, positionPoints, "player", "Big Ones", 10, "", Color.magenta, 5, null, false, 0, 0);
    }

    public void CreatePulsatingImage()
    {
        CanvasUIEffects.Instance.CreatePulseEffectImage("EndScreenCanvas", "PulsingImage", "crown", new Vector2(0,0), new Vector2(150, 150),
                       0.8f, 1.5f, 0.5f, null);
    }

    public void CreatePulsatingText()
    {
        CanvasUIEffects.Instance.CreatePulseEffectTextObject("EndScreenCanvas", "PulseText", "Pulse Time", "", 10, Color.blue,
                                      new Vector2(0, 200), 0.5f, 2.5f, 1, null);
    }

    public void CreateWobblyObject()
    {
        CanvasUIEffects.Instance.CreateWobbleEffectImage("EndScreenCanvas", "WobblingImage", "player", new Vector2(0,0), new Vector2(200, 200), 3, 10, null);
    }

    public void CreateLeaderboard()
    {
        Color backgroundColour = new Color(248, 186, 66);
        Color playerBackgroundColour = new Color(75, 72, 61);
        Vector2 playerBackgroundSize = new Vector2(70, 12);

        Vector2 topOfLeaderBoardPosition = new Vector2(0, 100);
        float spaceBetweenPlayerBackground = 50;

        float backgroundMaxScale = 5;
        float backgroundScaleSpeed = 3;

        string textFont = "";
        float textSize = 7;
        float textMaxScale = 5;

        Dictionary<string, string> playerInfo = new Dictionary<string, string>()
        {
            { "Charles", "13" },
            { "Bald Frog", "10" },
            { "Small Carl", "8" },
            { "King Fisher", "4" },
        };

        List<Vector2> movingObjectPositionPoints = new List<Vector2>
        {
             new (-170, 127),
             new (0, 200),
             new (170, 127),
        };

        Vector2 movingObjectSize = new(50, 50);
        float movingObjectRadius = 30.0f;
        float movingObjectRotationSpeed = 3.0f;
        float movingObjectMoveSpeed = 50.0f;

        string pulsatingImageTextureName = "crown";
        Vector2 pulsatingImageStartPos = new Vector2(330, 105);
        Vector2 pulsatingImageSize = new Vector2(200, 200);
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

        CanvasUIEffects.Instance.CreateLeaderboard("EndScreenCanvas", backgroundColour, "Leaderboard", 10, 3, playerBackgroundColour,
                                                 playerBackgroundSize, topOfLeaderBoardPosition, spaceBetweenPlayerBackground,
                                                 backgroundMaxScale, backgroundScaleSpeed, 0, 0, textSize, textMaxScale, textFont, Color.white, playerInfo,
                                                 movingObjectSize, movingObjectRadius, movingObjectRotationSpeed, movingObjectMoveSpeed,
                                                 movingObjectPositionPoints, "crown", "", 0,  Color.white, 5, true, 5, 1,
                                                 pulsatingImageTextureName, pulsatingImageStartPos, pulsatingImageSize,
                                                 pulsatingImageMinScale, pulsatingImageMaxScale, pulsatingImagePulseSpeed,
                                                 pulsatingTextString, pulsatingTextSize, pulsatingTextColor, pulsatingTextStartPos,
                                                 pulsatingTextMinScale, pulsatingTextMaxScale, pulsatingTextSpeed,
                                                 wobblingImageTexture, wobblingImageStartPos, wobblingImageSize,
                                                 wobblingImageWobbleSpeed, wobblingImageWobbleAmount);

    }

    public void CreateLeaderboardForSimulator()
    {
        Color backgroundColour = new Color(248, 186, 66);
        Color playerBackgroundColour = new Color(75, 72, 61);

        Vector2 playerBackgroundSize = new Vector2(100, 20);
        Vector2 topOfLeaderBoardPosition = new Vector2(0, 200);
        float spaceBetweenPlayerBackground = 100;

        float backgroundMaxScale = 10;
        float backgroundScaleSpeed = 3;

        string textFont = "";
        float textSize = 7;
        float textMaxScale = 16;

        Dictionary<string, string> playerInfo = new Dictionary<string, string>()
            {{ "Charles", "13" }, { "Bald Frog", "10" }, { "Small Carl", "8" }, { "King Fisher", "4" }};

        Vector2 movingObjectSize = new Vector2(100, 100);


        float movingObjectRadius = 30.0f;
        float movingObjectRotationSpeed = 3.0f;
        float movingObjectMoveSpeed = 75.0f;

        List<Vector2> movingObjectPositionPoints = new List<Vector2>
        {new Vector2(-500, 300), new Vector2(0, 500), new Vector2(500, 300)};

        string pulsatingImageTextureName = "crown";
        Vector2 pulsatingImageStartPos = new Vector2(87, 519);
        Vector2 pulsatingImageSize = new Vector2(250, 250);
        float pulsatingImageMinScale = 0.8f;
        float pulsatingImageMaxScale = 1.2f;
        float pulsatingImagePulseSpeed = 1.0f;
        string pulsatingTextString = "Winner";
        float pulsatingTextSize = 80.0f;
        Color pulsatingTextColor = Color.white;
        Vector2 pulsatingTextStartPos = new Vector2(0, 640);
        float pulsatingTextMinScale = 0.8f;
        float pulsatingTextMaxScale = 1.2f;
        float pulsatingTextSpeed = 1.0f;
        string wobblingImageTexture = "player";
        Vector2 wobblingImageStartPos = new Vector2(0, 460);
        Vector2 wobblingImageSize = new Vector2(250, 250);
        float wobblingImageWobbleSpeed = 3.0f;
        float wobblingImageWobbleAmount = 30.0f;

        CanvasUIEffects.Instance.CreateLeaderboard("EndScreenCanvas", backgroundColour, "Leaderboard", 10, 4, playerBackgroundColour,
                                                 playerBackgroundSize, topOfLeaderBoardPosition, spaceBetweenPlayerBackground,
                                                 backgroundMaxScale, backgroundScaleSpeed, 0, 0, textSize, textMaxScale, textFont, Color.white, playerInfo,
                                                 movingObjectSize, movingObjectRadius, movingObjectRotationSpeed, movingObjectMoveSpeed,
                                                 movingObjectPositionPoints, "crown", "", 0, Color.white, 5, true, 5, 1,
                                                 pulsatingImageTextureName, pulsatingImageStartPos, pulsatingImageSize,
                                                 pulsatingImageMinScale, pulsatingImageMaxScale, pulsatingImagePulseSpeed,
                                                 pulsatingTextString, pulsatingTextSize, pulsatingTextColor, pulsatingTextStartPos,
                                                 pulsatingTextMinScale, pulsatingTextMaxScale, pulsatingTextSpeed,
                                                 wobblingImageTexture, wobblingImageStartPos, wobblingImageSize,
                                                 wobblingImageWobbleSpeed, wobblingImageWobbleAmount);
    }

    public void CreatePodium()
    {
        string canvasName = "EndScreenCanvas";
        Color backgroundColour = new Color(0.2f, 0.2f, 0.2f);
        List<Color> playerBackgroundColours = new List<Color>() { new Color(157, 232, 242), new Color(255, 215, 0), new Color(192, 192, 192), new Color(205, 127, 50) };
        float podiumWidth = 50;
        Vector2 podiumBasePosition = new Vector2(150, -150);
        float spaceBetweenPodiums = 100;
        float podiumScaleMultiplyer = 3.0f;
        float podiumScaleSpeed = 100.0f;
        float nameTextOffsetX = 0.3f;
        float scoreTextOffsetX = 0.2f;
        float podiumTextSize = 3.0f;
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
        Vector2 pulsatingImageSize = new Vector2(100, 100);
        float pulsatingImageMinScale = 0.9f;
        float pulsatingImageMaxScale = 1.5f;
        float pulsatingImagePulseSpeed = 1.0f;
        string pulsatingTextString = "Podium Leaderboard";
        float pulsatingTextSize = 30.0f;
        Color pulsatingTextColor = new Color(1f, 0f, 0f);
        Vector2 pulsatingTextStartPos = new Vector2(0, 250);
        float pulsatingTextMinScale = 0.8f;
        float pulsatingTextMaxScale = 1.2f;
        float pulsatingTextSpeed = 1.0f;
        string wobblingImageTexture = "player";
        Vector2 wobblingImageStartPos = new Vector2(500, 120);
        Vector2 wobblingImageSize = new Vector2(100, 100);
        float wobblingImageWobbleSpeed = 3.0f;
        float wobblingImageWobbleAmount = 30.0f;

        CanvasUIEffects.Instance.CreatePodiumLeaderboard(canvasName, backgroundColour, playerBackgroundColours,
                                 podiumWidth, podiumBasePosition, spaceBetweenPodiums,
                                 podiumScaleMultiplyer, podiumScaleSpeed,
                                 nameTextOffsetX, scoreTextOffsetX, podiumTextSize, "", textColour,
                                 playerInfo, movingObjectSize, movingObjectRadius,
                                 movingObjectRotationSpeed, movingObjectMoveSpeed,
                                 movingObjectSpriteTextureString, movingObjectTextString, movingObjectTextSize,
                                 movingObjectTextColour, movingObjectDeletionTimer,
                                 canMovingObjectScale, movingObjectMaxScale,
                                 movingObjectScaleSpeed, pulsatingImageTextureName,
                                 pulsatingImageSize, pulsatingImageMinScale, pulsatingImageMaxScale,
                                 pulsatingImagePulseSpeed, pulsatingTextString, pulsatingTextSize,
                                 pulsatingTextColor, pulsatingTextStartPos, pulsatingTextMinScale,
                                 pulsatingTextMaxScale, pulsatingTextSpeed, wobblingImageTexture,
                                 wobblingImageStartPos, wobblingImageSize, wobblingImageWobbleSpeed,
                                 wobblingImageWobbleAmount);
    }
    public void CreatePodiumForSimulator()
    {
        string canvasName = "EndScreenCanvas";
        Color backgroundColour = new Color(0.2f, 0.2f, 0.2f);
        List<Color> playerBackgroundColours = new List<Color>() { new Color(157, 232, 242), new Color(255, 215, 0), new Color(192, 192, 192), new Color(205, 127, 50) };
        float podiumWidth = 200;
        Vector2 podiumBasePosition = new Vector2(0, -350);
        float spaceBetweenPodiums = 250;
        float podiumScaleMultiplyer = 10.0f;
        float podiumScaleSpeed = 100.0f;
        float nameTextOffsetY = 0.6f;
        float scoreTextOffsetY = 0.4f;
        float podiumTextSize = 10.0f;
        Color textColour = Color.white;
        Dictionary<string, string> playerInfo = new Dictionary<string, string>();
        playerInfo.Add("Andy", "100");
        playerInfo.Add("Charles", "90");
        playerInfo.Add("Muck", "80");
        playerInfo.Add("Stud", "70");
        Vector2 movingObjectSize = new Vector2(150, 150);
        float movingObjectRadius = 10.0f;
        float movingObjectRotationSpeed = 5.0f;
        float movingObjectMoveSpeed = 100.0f;
        string movingObjectSpriteTextureString = "movingObjectTexture";
        string movingObjectTextString = "Loser";
        float movingObjectTextSize = 40.0f;
        Color movingObjectTextColour = Color.black;
        float movingObjectDeletionTimer = 5.0f;
        bool canMovingObjectScale = true;
        float movingObjectMaxScale = 2.0f;
        float movingObjectScaleSpeed = 1.0f;
        string pulsatingImageTextureName = "crown";
        Vector2 pulsatingImageSize = new Vector2(250, 250);
        float pulsatingImageMinScale = 0.8f;
        float pulsatingImageMaxScale = 1.2f;
        float pulsatingImagePulseSpeed = 1.0f;
        string pulsatingTextString = "Leaderboard";
        float pulsatingTextSize = 100;
        Color pulsatingTextColor = new Color(1f, 0f, 0f);
        Vector2 pulsatingTextStartPos = new Vector2(0, 800);
        float pulsatingTextMinScale = 0.8f;
        float pulsatingTextMaxScale = 1.2f;
        float pulsatingTextSpeed = 1.0f;
        string wobblingImageTexture = "player";
        Vector2 wobblingImageStartPos = new Vector2(0, 1000);
        Vector2 wobblingImageSize = new Vector2(250, 250);
        float wobblingImageWobbleSpeed = 3.0f;
        float wobblingImageWobbleAmount = 30.0f;

        CanvasUIEffects.Instance.CreatePodiumLeaderboard(canvasName, backgroundColour, playerBackgroundColours,
                                 podiumWidth, podiumBasePosition, spaceBetweenPodiums,
                                 podiumScaleMultiplyer, podiumScaleSpeed,
                                 nameTextOffsetY, scoreTextOffsetY, podiumTextSize, "", textColour,
                                 playerInfo, movingObjectSize, movingObjectRadius,
                                 movingObjectRotationSpeed, movingObjectMoveSpeed,
                                 movingObjectSpriteTextureString, movingObjectTextString, movingObjectTextSize,
                                 movingObjectTextColour, movingObjectDeletionTimer,
                                 canMovingObjectScale, movingObjectMaxScale,
                                 movingObjectScaleSpeed, pulsatingImageTextureName,
                                 pulsatingImageSize, pulsatingImageMinScale, pulsatingImageMaxScale,
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

    public void RocketLeagueDemo()
    {
        List<Vector2> imagePos = new List<Vector2>{new Vector2(0, 220)};
        List<Vector2> textPos = new List<Vector2>{new Vector2(22, 200)};
        List<Vector2> scorePos = new List<Vector2>{new Vector2(22, 180)};

        float textMaxScale = 7;
        CanvasUIEffects.Instance.CreateMovingObject("EndScreenCanvas", new Vector2(50, 50), 0, 0, 0, imagePos, "rocket", 2, null, true, 3, 5);

        CanvasUIEffects.Instance.CreateMovingText("EndScreenCanvas", "RocketText", "GOOD SHOT", 3, "rocketfont",
                                                   new Color(255, 254, 151), 0, 0, 0, textPos,
                                                   2, null, true, textMaxScale, 10);

        CanvasUIEffects.Instance.CreateMovingText("EndScreenCanvas", "RocketText", "+20", 3, "rocketfont",
                                                   new Color(255, 254, 151), 0, 0, 0, scorePos,
                                                   2, null, true, textMaxScale, 10);

    }

    public void BatmanDemo()
    {
        StartCoroutine(Batman());
    }


    IEnumerator Batman()
    {
        CanvasUIEffects.Instance.CreateImageOnCanvas("EndScreenCanvas", 
            new Vector2(350, 200), new Vector2(0, 0), 1, 0, Color.black, null);

        yield return new WaitForSeconds(1);

        string[] rounds = 
            { "Round 1", "8390", "Round 2", "12635", "Round 3", "21465" };
        Vector2[] roundPositions = 
            { new Vector2(-165, 75), new Vector2(165, 75), new Vector2(-165, 55),
            new Vector2(165, 55), new Vector2(-165, 35), new Vector2(165, 35) };

        for (int i = 0; i < rounds.Length; i += 2)
        {
            CanvasUIEffects.Instance.CreateScalableText("EndScreenCanvas", "Name",
                rounds[i], "batman", 30, Color.white, roundPositions[i], 0.5f, 1, 0, null);
            yield return new WaitForSeconds(0.3f);
            CanvasUIEffects.Instance.CreateScalableText("EndScreenCanvas", "Name",
                rounds[i + 1], "batman", 30, Color.white, roundPositions[i + 1], 0.5f, 1, 1, null);
            yield return new WaitForSeconds(1);
        }

        string[] bonuses = 
        {
                "Base Score", "13965",
                "Variation Bonus x 9", "4000",
                "Gadget Variation Bonus x 5", "2000",
                "Perfect Round Bonus", "500",
                "Perfect Round Bonus", "500",
                "Flawless Freeflow Bonus", "1000"
         };
        Vector2[] bonusPositions = 
        {
                new Vector2(-150, 15), new Vector2(150, 15),
                new Vector2(-150, -5), new Vector2(150, -5),
                new Vector2(-150, -25), new Vector2(150, -25),
                new Vector2(-150, -45), new Vector2(150, -45),
                new Vector2(-150, -65), new Vector2(150, -65),
                new Vector2(-150, -85), new Vector2(150, -85)
        };

        for (int i = 0; i < bonuses.Length; i += 2)
        {
            CanvasUIEffects.Instance.CreateScalableText("EndScreenCanvas", "Name", bonuses[i], 
                "", 27, Color.white, bonusPositions[i], 0.5f, 1, 0, null);
            yield return new WaitForSeconds(0.3f);
            CanvasUIEffects.Instance.CreateScalableText("EndScreenCanvas", "Name", bonuses[i + 1],
                "", 27, Color.white, bonusPositions[i + 1], 0.5f, 1, 1, null);
            yield return new WaitForSeconds(1);
        }
    }

}

