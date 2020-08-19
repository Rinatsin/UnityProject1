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

    //test----
    Material material;
    //test----
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        material = new Material(Shader.Find("Standard"));
        material.color = Color.green; 
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

            } else if (point2 == Vector3.zero)
            {
                point2 = hit.point;

            }
            Vector3[] points = new Vector3[2] { point1, point2 };
            lineRenderer.SetPositions(points);
            //OnLineRender(material, point1, point2);
            //point1 = Vector3.zero;
            //point2 = Vector3.zero;
        }
    }

    void OnLineRender(Material lineMat, Vector3 point1, Vector3 point2)
    {
        GL.Begin(GL.LINES);
        lineMat.SetPass(0);
        GL.Color(new Color(0f, 0f, 0f, 1f));
        GL.Vertex3(point1.x, 5, point1.z);
        GL.Vertex3(point2.x, 5, point2.z);
        GL.End();
    }
}
