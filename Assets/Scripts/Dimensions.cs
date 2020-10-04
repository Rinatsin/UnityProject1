using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dimensions
{
    private static int _counter = 0;
    public static int Counter
    {
        get
        {
            return _counter;
        }
        set
        {
            _counter = value;
        }
    }

    private static string _startPoint;
    private static string _endPoint;
    private static float _distance = 0;
    private static string _distanceToString;

    public static void SaveDimensions(Vector3 startPoint, Vector3 endPoint)
    {
        _counter++;
        _startPoint = startPoint.ToString();
        _endPoint = endPoint.ToString();
        _distance = Vector3.Distance(startPoint, endPoint);
        _distanceToString = _distance.ToString("#.#");
        PlayerPrefs.SetString($"StartPoint{_counter}", _startPoint);
        PlayerPrefs.SetString($"EndPoint{_counter}", _endPoint);
        PlayerPrefs.SetString($"Distance{_counter}", _distanceToString);
        PlayerPrefs.Save();
    }


    public static void ResetDimensions()
    {
        PlayerPrefs.DeleteAll();
        _startPoint = "";
        _endPoint = "";
        _distance = 0.0f;
    }

    public static string LoadDimension(string dimensionName, int numberOfDimension)
    {
        switch (dimensionName)
        {
            case "StartPoint":
                return PlayerPrefs.GetString($"StartPoint{numberOfDimension}");
            case "EndPoint":
                return PlayerPrefs.GetString($"EndPoint{numberOfDimension}");
            case "Distance":
                return PlayerPrefs.GetString($"Distance{numberOfDimension}");
            default:
                return "Измерения нет";
        }
    }

    public static string LoadDimensionsList(int counter)
    {
        string result = "";
        for (int i = 1; i <= counter; i++)
        {
            string startPoint = LoadDimension("StartPoint", i);
            string endPoint = LoadDimension("EndPoint", i);
            string distance = LoadDimension("Distance", i);
            result += $"\nStartPoint{i}: {startPoint}, EndPoint{i}: {endPoint}, Distance{i}: {distance} \n";
        }
        return result;
    }
}
