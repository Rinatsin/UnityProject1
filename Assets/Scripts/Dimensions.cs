using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimensions : MonoBehaviour
{
    public float lineWidth = .1f; //ширина линии
    int n = 1;// номер линии

    Vector3 point1 = Vector3.zero; //Начальная точка измерения
    Vector3 point2 = Vector3.zero; //Конечная точка измерения
    float distance = 0; // Расстояние от начальной до конечной точки

    ArrayList _dimensions = new ArrayList(); //список всех полученных измерений
    ArrayList _dimension = new ArrayList();//Список одного измерения[distance, point1, point2]

    Ray ray;
    RaycastHit hit;
    LineRenderer lineRenderer;
    GameObject line;
    BoxCollider lineCollider;
    Ray raySelect;
    RaycastHit hitSelect;


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
                addCollider(lineRenderer, point1, point2);
                saveDimensions();
                point1 = Vector3.zero;
                point2 = Vector3.zero;
            }
        }
        if (point1 != Vector3.zero && point2 == Vector3.zero && Physics.Raycast(ray, out hit))
        {
            StartCoroutine(updateLine(point1, hit.point));
        }
        if (Input.GetMouseButtonDown(1))
        {
            raySelect = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raySelect, out hitSelect) && hitSelect.collider.tag == "Line")
            {
                lineRenderer.startColor = Color.blue;
                lineRenderer.endColor = Color.blue;
            }
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
        line = new GameObject("Line " + n);
        line.tag = "Line";
        lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.green;
        Vector3[] points = new Vector3[2] { p1, p2 };
        lineRenderer.SetPositions(points);
        n++;
        yield return null;
    }

    IEnumerator updateLine(Vector3 pos1, Vector3 pos2)
    {
        Vector3[] points = new Vector3[2] { pos1, pos2 };
        lineRenderer.SetPositions(points);
        yield return null;

    }

    void addCollider(LineRenderer lineRend, Vector3 p1, Vector3 p2)
    {
        if (line != null)
        {
            lineCollider = line.AddComponent<BoxCollider>();
            lineCollider.transform.parent = lineRend.transform;
            float lineWidth = lineRend.endWidth;
            float lineLength = Vector3.Distance(p1, p2);
            lineCollider.size = new Vector3(lineLength, lineWidth, .5f);
            lineCollider.transform.position = (p1 + p2) / 2;
            lineCollider.center = Vector3.zero;
            float angle = Mathf.Atan2((p2.z - p1.z), (p2.x - p1.x));
            angle *= Mathf.Rad2Deg;
            angle *= -1;
            lineCollider.transform.Rotate(0, angle, 0);
        }
    }
}
