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

        CanvasUIEffects.Instance.CreateEndScreen(Color.red, Color.gray, 15, playerInfo);
    }

      void Update()
    {

    }
}
