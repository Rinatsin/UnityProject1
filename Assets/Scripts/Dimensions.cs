using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dimensions : MonoBehaviour
{
    public float lineWidth = .1f; //ширина линии
    int n = 1;// номер линии

    [SerializeField] GameObject Panel;
    private bool _panelIsOpened = false;
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

    Vector3 point1 = Vector3.zero; //Начальная точка измерения
    Vector3 point2 = Vector3.zero; //Конечная точка измерения
    float distance = 0; // Расстояние от начальной до конечной точки

    ArrayList _dimensions = new ArrayList(); //список всех полученных измерений
    ArrayList _dimension = new ArrayList();//cписок одного измерения[distance, point1, point2]
    ArrayList _selectedList = new ArrayList();// список выделенных объектов

    Ray ray;
    RaycastHit hit;
    LineRenderer lineRenderer;
    GameObject line;
    BoxCollider lineCollider;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
        OpenClosePanel();
        DeleteSelectedLines();
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.tag != "Line" && _isOnMeasure)
            {
                //Снимаем выделение с объекта(ов) если кликнули не на линии 
                ClearSelected();
                //Рисуем линию
                if (point1 == Vector3.zero)
                {
                    point1 = hit.point;
                    StartCoroutine(CreateLine(point1, hit.point));
                }
                else if (point1 != Vector3.zero && point2 == Vector3.zero)
                {
                    point2 = hit.point;
                    StartCoroutine(UpdateLine(point1, point2));
                    AddCollider(lineRenderer, point1, point2);
                    SaveDimensions();
                    point1 = Vector3.zero;
                    point2 = Vector3.zero;
                }
            }
            else if (Input.GetMouseButtonDown(0) && hit.collider.tag == "Line" && _isOnHightlight)
            {
                SelectLine();
            }
            if (point1 != Vector3.zero && point2 == Vector3.zero && Physics.Raycast(ray, out hit))
            {
                StartCoroutine(UpdateLine(point1, hit.point));
            }
        }
    }

    /*
     * Функция сохраняет полученные измерения
     */
    private void SaveDimensions()
    {
        distance = Vector3.Distance(point1, point2);
        _dimension.Add(distance);
        _dimension.Add(point1);
        _dimension.Add(point2);
        _dimensions.Add(_dimension);
        _dimension.Clear();
    }

    /*
     * Функция рисует линию
     */
    IEnumerator CreateLine(Vector3 p1, Vector3 p2)
    {
        line = new GameObject("Line " + n);
        line.tag = "Line";
        lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        Vector3[] points = new Vector3[2] { p1, p2 };
        lineRenderer.SetPositions(points);
        n++;
        yield return null;
    }

    /*
     * Функция обновляет положение линии
     */
    IEnumerator UpdateLine(Vector3 pos1, Vector3 pos2)
    {
        Vector3[] points = new Vector3[2] { pos1, pos2 };
        lineRenderer.SetPositions(points);
        yield return null;

    }

    /*
     * Функция добавляет коллаидер к линии
     */
    private void AddCollider(LineRenderer lineRend, Vector3 p1, Vector3 p2)
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

    /*
     * Функция выделения линии
     */
    private void SelectLine()
    {
        line = hit.collider.gameObject;
        _selectedList.Add(line);
        line.GetComponent<LineRenderer>().startColor = Color.blue;
        line.GetComponent<LineRenderer>().endColor = Color.blue;
    }

    /*
     * Функция снимает выделение
     */
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

    /*
     * Функция удаляет выделенные линии если нажата клавиша Delete
     */
    private void DeleteSelectedLines()
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

    public void ChangePanelVisibility()
    {
        if (_panelIsOpened)
        {
            _panelIsOpened = false;
        } else
        {
            _panelIsOpened = true;
        }
    }

    private void OpenClosePanel()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangePanelVisibility();
            Panel.SetActive(_panelIsOpened);
        }
    }
}
