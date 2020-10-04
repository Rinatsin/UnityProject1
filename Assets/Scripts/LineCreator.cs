using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineCreator : MonoBehaviour
{
    [SerializeField] private float lineWidth = .05f;

    private Vector3 startPoint = Vector3.zero; 
    private Vector3 endPoint = Vector3.zero;
    private ArrayList _selectedList = new ArrayList();

    private Ray ray;
    private RaycastHit hit;
    private LineRenderer lineRenderer;
    private GameObject line;
    private BoxCollider lineCollider;

    private bool _isOnHightlight = false;
    public bool IsOnHightlight
    {
        get
        {
            return _isOnHightlight;
        }

        set
        {
            _isOnHightlight = value;
        }
    }

    private bool _isOnMeasure = false;
    public bool IsOnMeasure
    {
        get
        {
            return _isOnMeasure;
        }

        set
        {
            _isOnMeasure = value;
        }
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
        DeleteSelectedLines();
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.tag != "Line" && _isOnMeasure)
                {
                    //Снимаем выделение с объекта(ов) если кликнули не на линии 
                    ClearSelected();
                    if (startPoint == Vector3.zero)
                    {
                        startPoint = hit.point + new Vector3(0, lineWidth, 0);
                        DrawLine(startPoint, hit.point);
                    }
                    else if (startPoint != Vector3.zero && endPoint == Vector3.zero)
                    {
                        endPoint = hit.point + new Vector3(0, lineWidth, 0);
                        UpdateLine(startPoint, endPoint);
                        AddCollider(lineRenderer, startPoint, endPoint);

                        Dimensions.SaveDimensions(startPoint, endPoint);

                        startPoint = Vector3.zero;
                        endPoint = Vector3.zero;
                    }
                }
                else if (hit.collider.tag == "Line" && _isOnHightlight)
                {
                    SelectLine();
                }
            }
            if (startPoint != Vector3.zero && endPoint == Vector3.zero && Physics.Raycast(ray, out hit))
            {
                UpdateLine(startPoint, hit.point);
            }
        }
    }

    private void DrawLine(Vector3 p1, Vector3 p2)
    {
        line = new GameObject("Line");
        line.tag = "Line";
        lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.numCapVertices = 2;
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        Vector3[] points = new Vector3[2] { p1, p2 };
        lineRenderer.SetPositions(points);
    }

    private void UpdateLine(Vector3 pos1, Vector3 pos2)
    {
        Vector3[] points = new Vector3[2] { pos1, pos2 };
        lineRenderer.SetPositions(points);
    }

    private void AddCollider(LineRenderer lineRend, Vector3 p1, Vector3 p2)
    {
        if (line != null)
        {
            lineCollider = line.AddComponent<BoxCollider>();
            lineCollider.transform.parent = lineRend.transform;
            float lineWidth = lineRend.endWidth;
            float lineLength = Vector3.Distance(p1, p2);
            lineCollider.isTrigger = true;
            lineCollider.size = new Vector3(lineLength, lineWidth, .5f);
            lineCollider.transform.position = (p1 + p2) / 2;
            lineCollider.center = Vector3.zero;
            float angle = Mathf.Atan2((p2.z - p1.z), (p2.x - p1.x));
            angle *= Mathf.Rad2Deg;
            angle *= -1;
            lineCollider.transform.Rotate(0, angle, 0);
        }
    }

    private void SelectLine()
    {
        line = hit.collider.gameObject;
        _selectedList.Add(line);
        line.GetComponent<LineRenderer>().startColor = Color.blue;
        line.GetComponent<LineRenderer>().endColor = Color.blue;
    }

    private void ClearSelected()
    {
        if (_selectedList != null)
        {
            foreach (GameObject selectedLine in _selectedList)
            {
                selectedLine.GetComponent<LineRenderer>().startColor = Color.green;
                selectedLine.GetComponent<LineRenderer>().endColor = Color.green;
            }
            _selectedList.Clear();
        }
    }

    public void DeleteSelectedLines()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            foreach (GameObject selectedLine in _selectedList)
            {
                Destroy(selectedLine);
            }
            _selectedList.Clear();
        }
    }

}
