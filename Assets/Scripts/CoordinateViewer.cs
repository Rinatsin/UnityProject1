using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoordinateViewer : MonoBehaviour
{
    Vector3 cursorPos;
    Text coordinate;
    RectTransform textPos;

    void Start()
    {
        coordinate = GetComponent<Text>();
        textPos = GetComponent<RectTransform>();
    }

    void Update()
    {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        textPos.position = Input.mousePosition;

        string x = cursorPos.x.ToString("#.#");
        string z = cursorPos.z.ToString("#.#");
        coordinate.text = "X: " + x + " Z: " + z;
    }
}
