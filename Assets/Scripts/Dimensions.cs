using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimensions
{
    private float distance = 0; // Расстояние от начальной до конечной точки
    private ArrayList _dimensions = new ArrayList(); //список всех полученных измерений
    private ArrayList _dimension = new ArrayList();//cписок одного измерения[point1, point2, distance]

    public void SaveDimensions(Vector3 startPoint, Vector3 endPoint)
    {
        distance = Vector3.Distance(startPoint, endPoint);
        _dimension.Add(startPoint);
        _dimension.Add(endPoint);
        _dimension.Add(distance);
        _dimensions.Add(_dimension);
        _dimension.Clear();
    }

    public string ViewAllDimensions()
    {
        if (_dimensions.Count > 0)
        {
            foreach (ArrayList dimension in _dimensions)
            {
                return $"point 1: {dimension[0]}, point 2: {dimension[1]}, distance: {dimension[2]} \n";
            }
        }
        return "Измерений пока нет";
        
    }
    
}
