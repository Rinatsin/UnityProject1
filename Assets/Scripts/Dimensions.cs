using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimensions : MonoBehaviour
{
    Vector3 point1 = Vector3.zero;
    Vector3 point2 = Vector3.zero;
    float distance = 0;

    Ray ray;
    Vector3 cursorPos;
    RaycastHit hit;
    LineRenderer lineRenderer;
    GameObject line;

    void Start()
    {
       
    }

    
    void Update()
    {
        
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            if (point1 == Vector3.zero)
            {
                point1 = hit.point;

            } else if (point1 != Vector3.zero && point2 == Vector3.zero)
            {
                point2 = hit.point;
                OnLineRender(point1, point2);
                saveDimensions();
                point1 = Vector3.zero;
                point2 = Vector3.zero;
            }
        }
        //if (point1 != Vector3.zero && point2 == Vector3.zero && Physics.Raycast(ray, out hit))
        //{
        //    OnLineRender(point1, hit.point);
        //}
    }

    /*
     * Функция рисования линии по двум точкам
     */
    void OnLineRender(Vector3 point1, Vector3 point2)
    {
        line = new GameObject();
        lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        Vector3[] points = new Vector3[2] { point1, point2 };
        lineRenderer.SetPositions(points);
        lineRenderer.useWorldSpace = true;
        //if (point2 == Vector3.zero)
        //{
        //    Destroy(lineRenderer);
        //}
    }

    void saveDimensions()
    {
        distance = Vector3.Distance(point1, point2);
        Debug.Log(distance);
    }
}
