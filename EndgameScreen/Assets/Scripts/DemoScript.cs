using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, string> playerInfo = new Dictionary<string, string>()
        {
            { "Charles", "13" },
            { "Bald Frog", "10" },
            { "Small Carl", "8" },
            { "King Fisher", "4" },
         };

        // CanvasUIEffects.Instance.CreateEndScreen(Color.red, Color.gray, 15, playerInfo);

        Vector2 size = new Vector2(50, 50);
        float radius = 50.0f;
        float rotationSpeed = 3.0f;
        float moveSpeed = 100.0f;
        List<Vector2> list = new List<Vector2>
        {
            new Vector2(-400, -200),
            new Vector2(-200, 200),
            new Vector2(0, 0),
        };

        List<Vector2> list2 = new List<Vector2>
        {
            new Vector2(0, 0),
            new Vector2(500, 100),
            new Vector2(-300, -150),
        };

        CanvasUIEffects.Instance.CreateMovingObject(size, radius, rotationSpeed, moveSpeed,  new Vector2(-500,0), list, "player", 5);
        CanvasUIEffects.Instance.CreateMovingObjectWithText(size, radius, rotationSpeed, moveSpeed,  new Vector2(-500,0), list2, "player", "Hey", 15, Color.red, 5);
    }
}
