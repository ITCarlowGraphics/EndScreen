using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        CanvasUIEffects.Instance.CreateCanvas();
        CanvasUIEffects.Instance.CreateRectangleOnCanvas(Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero, Color.red);
        CanvasUIEffects.Instance.CreateImageOnCanvas("Player", 50, 50, Vector2.zero, 5, 1);
        CanvasUIEffects.Instance.CreateText("AndyName", "Super Charles!", 10, Color.green, Vector2.zero, 5, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
