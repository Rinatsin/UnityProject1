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
    Ray ray;
    RaycastHit hit;

    private string x;
    private string z;

    void Start()
    {
        coordinate = GetComponent<Text>();
        textPos = GetComponent<RectTransform>();
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        textPos.position = Input.mousePosition;

        if (Physics.Raycast(ray, out hit))
        {
            x = hit.point.x.ToString("#.#");
            z = hit.point.z.ToString("#.#");
        } else
        {
            x = cursorPos.x.ToString("#.#");
            z = cursorPos.z.ToString("#.#");
        }
        coordinate.text = "X: " + x + " Z: " + z;
    }
}
