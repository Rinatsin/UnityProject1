using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimensions : MonoBehaviour
{
    Vector3 point1 = Vector3.zero; //Начальная точка измерения
    Vector3 point2 = Vector3.zero; //Конечная точка измерения
    float distance = 0; // Расстояние от начальной до конечной точки

    ArrayList _dimensions = new ArrayList(); //список всех полученных измерений
    ArrayList _dimension = new ArrayList();//Список одного измерения[distance, point1, point2]

    Ray ray;
    Vector3 cursorPos;
    RaycastHit hit;
    LineRenderer lineRenderer;
    GameObject line;


    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            if (point1 == Vector3.zero)
            {
                point1 = hit.point;
                StartCoroutine(createLine(point1, hit.point));
            }
            else if (point1 != Vector3.zero && point2 == Vector3.zero)
            {
                point2 = hit.point;
                StartCoroutine(updateLine(point1, point2));
                saveDimensions();
                point1 = Vector3.zero;
                point2 = Vector3.zero;
            }
        }
        if (point1 != Vector3.zero && point2 == Vector3.zero && Physics.Raycast(ray, out hit))
        {
            StartCoroutine(updateLine(point1, hit.point));
        }
    }

    /*
     * Функция сохраняет полученные измерения
     */
    void saveDimensions()
    {
        distance = Vector3.Distance(point1, point2);
        _dimension.Add(distance);
        _dimension.Add(point1);
        _dimension.Add(point2);
        _dimensions.Add(_dimension);
        _dimension.Clear();
    }

    IEnumerator createLine(Vector3 p1, Vector3 p2)
    {
        line = new GameObject("Line ");
        lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.green;
        Vector3[] points = new Vector3[2] { p1, p2 };
        lineRenderer.SetPositions(points);
        yield return null;
    }

    IEnumerator updateLine(Vector3 pos1, Vector3 pos2)
    {
        Vector3[] points = new Vector3[2] { pos1, pos2 };
        lineRenderer.SetPositions(points);
        yield return null;

    }
}
